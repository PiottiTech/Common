using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PiottiTech.Common
{
    public class Result : IResult
    {
        #region IResult Implementation

        public bool Success { get; set; }

        private List<string> _messageList = new List<string>();

        public List<string> MessageList
        {
            get { return _messageList; }
            set { _messageList = value; }
        }

        #endregion IResult Implementation

        #region Constructors

        public Result()
        {
        }

        public Result(bool success)
        {
            Success = success;
        }

        public Result(bool success, string message)
        {
            Success = success;
            AddMessage(message);
        }

        public Result(bool success, List<string> messageList)
        {
            Success = success;
            foreach (string message in messageList)
            {
                AddMessage(message);
            }
        }

        public Result(IResult iResult)
        {
            Success = iResult.Success;
            MessageList = iResult.MessageList;
        }

        public Result(Exception ex)
        {
            this.Exception = ex;
        }

        public Result(string message, Exception ex)
        {
            this.AddMessage(message);
            this.Exception = ex;
        }

        #endregion Constructors

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        #region Exceptions and Exception Methods

        private Exception _exception;

        public Exception Exception
        {
            set
            {
                Guid logGuid = Logger.Log(value);
                AddMessage("An error has occurred.");
                AddMessage("Log Id: " + logGuid.ToString());
                if (ReturnExceptions())
                {
                    this.AddMessage(value.ToString());
                }
                _exception = value;
            }
        }

        private static bool ReturnExceptions()
        {
            bool returnApiExceptions;
            string configReturnApiExceptions = Config.AppSetting("ReturnApiExceptions");
            Boolean.TryParse(configReturnApiExceptions, out returnApiExceptions);
            return returnApiExceptions;
        }

        #endregion Exceptions and Exception Methods

        #region Message Methods

        public void AddMessage(string message)
        {
            message = message.Trim();
            if (!String.IsNullOrEmpty(message))
            {
                this.MessageList.Add(message);
            }
        }

        public void PrependMessage(string message)
        {
            this.MessageList.Insert(0, message);
        }

        #endregion Message Methods

        #region Operators

        public static Result operator +(Result r1, Result r2)
        {
            //DEVNOTE: Set the success bool
            Result newResult = new Result(r1.Success & r2.Success);

            //DEVNOTE: Set the messages
            newResult.MessageList.AddRange(r1.MessageList);
            newResult.MessageList.AddRange(r2.MessageList);

            return newResult;
        }

        #endregion Operators
    }
}