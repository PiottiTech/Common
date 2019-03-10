using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace PiottiTech.Common
{
    public class Logger
    {
        public static Guid Log(string message)
        {
            List<string> listOStrings = new List<string>() { message };
            return Log(listOStrings);
        }

        public static Guid Log(string message, Exception exception)
        {
            List<string> listOStrings = new List<string>() { message, exception.ToString() };
            return Log(listOStrings);
        }

        public static Guid Log(string message, Exception exception, Object obj)
        {
            List<string> listOStrings = new List<string>() { message, exception.ToString(), obj.ToStringJson() };
            return Log(listOStrings);
        }

        public static Guid Log(Exception exception)
        {
            List<string> listOStrings = new List<string>() { exception.ToString() };
            return Log(listOStrings);
        }

        public static Guid Log(Exception exception, Object obj)
        {
            List<string> listOStrings = new List<string>() { exception.ToString(), obj.ToStringJson() };
            return Log(listOStrings);
        }

        public static Guid Log(Result result)
        {
            List<string> listOStrings = new List<string>() { result.ToString() };
            return Log(listOStrings);
        }

        #region Methods that do the actual logging work

        //DEVNOTE: This method is the ONLY place where Guid is generated and ONLY method that calls WriteToLog.
        public static Guid Log(List<string> messages)
        {
            Guid logGuid = Guid.NewGuid();
            StringBuilder sb = new StringBuilder(2 + (2 * messages.Count));

            sb.Append(logGuid.ToString());
            sb.Append(Environment.NewLine);

            foreach (string message in messages)
            {
                sb.Append(message);
                sb.Append(Environment.NewLine);
            }

            WriteToLog(sb.ToString(), logGuid);
            return logGuid;
        }

        private static void WriteToLog(string logMessage, Guid logGuid)
        {
            //DEVNOTE: Get Logger_ApplicationName from AppSettings. If not there, default to "Unknown"
            string dbException = String.Empty;
            string applicationName = Config.AppSetting("Logger_ApplicationName");
            if (String.IsNullOrEmpty(applicationName)) { applicationName = "Unknown"; }

            try
            {
                using (var connection = new SqlConnection(Config.ConnectionString("Log")))
                {
                    connection.Query("Logging.ApplicationLogInsert",
                        new
                        {
                            applicationName,
                            logGuid,
                            logMessage
                        },
                        commandType: CommandType.StoredProcedure);
                }
                return;
            }
            catch (Exception ex)
            {
                //DEVNOTE: Instead of nesting try catch, will run them stacked with hard return in initial try.
                dbException = ex.ToString();
            }

            try
            {
                string sSource = applicationName;
                string sLog = "Application";

                if (!EventLog.SourceExists(sSource))
                {
                    EventLog.CreateEventSource(sSource, sLog);
                }

                EventLog.WriteEntry(sSource, "Cannot log to database server. Logging to EventLog as failover. " + dbException, EventLogEntryType.Error);
                EventLog.WriteEntry(sSource, logMessage, EventLogEntryType.Error);
                return;
            }
            catch
            {
                //DEVNOTE: No further processing.
            }
        }

        #endregion Methods that do the actual logging work
    }
}