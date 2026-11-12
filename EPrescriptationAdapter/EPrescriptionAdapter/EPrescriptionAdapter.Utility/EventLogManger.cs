using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;

namespace EprescriptionService.Utility
{
    // <summary>
    /// Takes care of logging errors and warnings.
    /// It is important that these classes do not throw an exception
    /// (because most likely, they are already logging an exception)
    /// </summary>
    public class EventLogManager
    {
        public EventLogManager() { }



        /// <summary>
        /// Logs and exception to the database
        /// The exception logger cannot fail!!
        /// Therefore swallow exceptions
        /// </summary>
        public static void LogEvent(SharedTypes.SeverityTypeEnum severity, string customMessage, Exception exc)
        {
            try
            {
                string innerExc = "";
                if (exc.InnerException != null)
                    innerExc = exc.InnerException.Message;

                LogEvent(severity, customMessage, exc.Message, innerExc, exc.StackTrace, DateTime.Now,null,null);
            }
            catch
            { // do nothing 
            }
        }

        /// <summary>
        /// Short form of LogEvent message
        /// </summary>
        public static void LogEvent(SharedTypes.SeverityTypeEnum severity, string customMessage)
        {
            try
            {
                LogEvent(severity, customMessage, null, null, null, DateTime.Now,null,null);
            }
            catch
            { // do nothing 
            }
        }


        /// <summary>
        /// Logs and exception to the database
        /// Written for the front end which cannot pass an 
        /// exception object due to the multitude of exceptions
        /// that can be passed.
        /// </summary>
        public static void LogEvent(SharedTypes.SeverityTypeEnum severity, string customMessage, string excMessage, string innerExcMessage, string stackTrace, DateTime? dt, String UserID,String Command)
        {
            try
            {
                Dictionary<string, string> args = new Dictionary<string, string>();
                if (dt == null)
                    dt = DateTime.Now;
                args.Add("@LogDtTm", dt.ToString());
              
              
                args.Add("@SeverityTypeId", severity.ToString());
                if (!String.IsNullOrEmpty(customMessage))
                    args.Add("@CustomMessage", customMessage);
                else
                    args.Add("@CustomMessage", null);


                if (!String.IsNullOrEmpty(excMessage))
                    args.Add("@ExceptionMessage", excMessage);
                else
                    args.Add("@ExceptionMessage", null);
                if (!String.IsNullOrEmpty(innerExcMessage))
                    args.Add("@InnerExceptionMessage", innerExcMessage);
                else
                    args.Add("@InnerExceptionMessage", null);
                if (!String.IsNullOrEmpty(stackTrace))
                {
                    if (stackTrace.Length > 2048)
                        args.Add("@StackTrace", stackTrace.Substring(0, 2047));
                    else
                        args.Add("@StackTrace", stackTrace);
                }
                else
                {
                    args.Add("@StackTrace", null);
                }

                if(!String.IsNullOrEmpty(UserID))
                    args.Add("@UserID", UserID);
                else
                    args.Add("@UserID", null);

                if(!String.IsNullOrEmpty(Command))
                    args.Add("@Command", UserID);
                else
                    args.Add("@Command", null);

                DBAccess db = new DBAccess();
              //  db.RunStoredProc(Db args);

            }
            catch (Exception exc)
            {
                // do nothing
            }
        }
    }
}
