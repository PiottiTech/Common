using System;
using System.Collections.Generic;

namespace PiottiTech.Common
{
    public class ApiResult : Result
    {
        #region Properties

        public string TimeStamp
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        private IEnumerable<dynamic> _data;

        public IEnumerable<dynamic> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                Success = true;
            }
        }

        #endregion Properties

        #region Constructors

        public ApiResult(IEnumerable<dynamic> data)
        {
            Data = data;
        }

        public ApiResult() : base()
        {
        }

        public ApiResult(bool success)
            : base(success) { }

        public ApiResult(bool success, string message)
            : base(success, message) { }

        public ApiResult(IResult iResult)
            : base(iResult) { }

        public ApiResult(Exception ex)
            : base(ex) { }

        public ApiResult(string message, Exception ex)
            : base(message, ex) { }

        #endregion Constructors
    }
}

