using System;
using System.Collections.Generic;
using TrialIQ.Common.Contracts.DTOs;
using TrialIQ.Common.Contracts.DTOs.Users;

namespace TrialIQ.Common
{
    /// <summary>
    /// Takes care of logging errors and warnings.
    /// It is important that these classes do not throw an exception
    /// (because most likely, they are already logging an exception)
    /// </summary>
    public class EventLogManager
    {
        public EventLogManager() {}



       /// <summary>
        /// Logs and exception to the database
        /// The exception logger cannot fail!!
        /// Therefore swallow exceptions
        /// </summary>
        public static void LogEvent(SharedTypes.SeverityTypeEnum severity, string customMessage,  Exception exc, UserSession session)
        {
            try
            {
                string innerExc = "";
                if (exc.InnerException != null)
                    innerExc = exc.InnerException.Message;

                LogEvent(severity, customMessage, exc.Message, innerExc, exc.StackTrace, DateTime.Now, session);
            }
            catch 
            { // do nothing 
            }
        }

        /// <summary>
        /// Short form of LogEvent message
        /// </summary>
        public static void LogEvent(SharedTypes.SeverityTypeEnum severity, string customMessage, UserSession session)
        {
            try
            {
                LogEvent(severity, customMessage, null, null, null, DateTime.Now, session);
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
       public static void LogEvent(SharedTypes.SeverityTypeEnum severity, string customMessage, string excMessage, string innerExcMessage, string stackTrace, DateTime? dt, UserSession session)
        {
            try
            {
                Dictionary<string, string> args = new Dictionary<string, string>();
                if (dt == null)
                    dt = DateTime.Now;
                args.Add("@LogDtTm", dt.ToString());
                if (session != null)
                {
                    // add the userid
                    if (session.UserId != null)
                        args.Add("@UserId", session.UserId);
                    else
                        args.Add("@UserId", null);
                    // add the username
                    if (session.Username != null)
                        args.Add("@UserName", session.Username);
                    else
                        args.Add("@UserName", null);
                    // add the subject id
                    if (session.SubjectId != null)
                        args.Add("@SubjectId", session.SubjectId);
                    else
                        args.Add("@SubjectId", null);
                    // add the assigned subject id
                    if (session.AssignedSubjectId != null)
                        args.Add("@SubjectAssignedId", session.AssignedSubjectId);
                    else
                        args.Add("@SubjectAssignedId", null);
                    // add the subject protocol friendly name
                    if (session.SubjectProtocolFriendlyName != null)
                        args.Add("@ProtocolFriendlyName", session.SubjectProtocolFriendlyName);
                    else
                        args.Add("@ProtocolFriendlyName", null);
                }
                else
                {
                    args.Add("@UserId", null);
                    args.Add("@UserName", null);
                    args.Add("@SubjectId", null);
                    args.Add("@SubjectAssignedId", null);
                    args.Add("@ProtocolFriendlyName", null);
                }
                args.Add("@SeverityTypeId", severity.ToString());
                if (!String.IsNullOrEmpty(customMessage))
                    args.Add("@CustomMessage", customMessage);
                else
                    args.Add("@CustomMessage", null);


                if(!String.IsNullOrEmpty(excMessage))
                    args.Add("@ExceptionMessage", excMessage);
                else
                    args.Add("@ExceptionMessage", null);
                if(!String.IsNullOrEmpty(innerExcMessage))
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

 
                DBAccess db = new DBAccess();
                db.RunStoredProc("iq_logAddEvent", args);

            }
            catch(Exception exc)
            {
                // do nothing
            }
        }
    }
}
