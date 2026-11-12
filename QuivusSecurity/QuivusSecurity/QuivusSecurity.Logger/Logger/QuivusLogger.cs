using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using MicrosoftLogger = Microsoft.Practices.EnterpriseLibrary.Logging.Logger;
using System.Configuration;
using System.Threading;


namespace QuivusSecurity.Logger.Logger
{
    public enum Category
    {

        Error,
        Info,
        Debug,

    }

    public static class QuivusLogger
    {
        private static readonly string UserIdKey = "ID";

        #region "Debug (Verbose)"

        public static void Debug(object message)
        {
            WriteMessage(message, TraceEventType.Verbose, null, null);
        }

        public static void Debug(object message, Category category)
        {
            WriteMessage(message, TraceEventType.Verbose, category, null);
        }

        public static void Debug(object message, object category)
        {
            WriteMessage(message, TraceEventType.Verbose, category, null);
        }

        public static void Debug(object message, Exception ex)
        {
            WriteMessage(message, TraceEventType.Verbose, null, ex);
        }

        public static void Debug(object message, object category, Exception ex)
        {
            WriteMessage(message, TraceEventType.Verbose, category, ex);
        }

        #endregion

        #region "Info"

        public static void Info(object message)
        {
            WriteMessage(message, TraceEventType.Information, null, null);
        }

        public static void Info(object message, Category category)
        {
            WriteMessage(message, TraceEventType.Information, category, null);
        }

        public static void Info(object message, object category)
        {
            WriteMessage(message, TraceEventType.Information, category, null);
        }

        public static void Info(object message, Exception ex)
        {
            WriteMessage(message, TraceEventType.Information, null, ex);
        }

        public static void Info(object message, object category, Exception ex)
        {
            WriteMessage(message, TraceEventType.Information, null, ex);
        }

        #endregion

        #region "Warning"

        public static void Warn(object message)
        {
            WriteMessage(message, TraceEventType.Warning, null, null);
        }

        public static void Warn(object message, Category category)
        {
            WriteMessage(message, TraceEventType.Warning, category, null);
        }

        public static void Warn(object message, object category)
        {
            WriteMessage(message, TraceEventType.Warning, category, null);
        }

        public static void Warn(object message, Exception ex)
        {
            WriteMessage(message, TraceEventType.Warning, null, ex);
        }

        public static void Warn(object message, object category, Exception ex)
        {
            WriteMessage(message, TraceEventType.Warning, category, ex);
        }

        #endregion

        #region "Error"

        public static void Error(Exception ex)
        {
            WriteMessage(ex.Message, TraceEventType.Error, null, ex);
        }

        public static void Error(object message)
        {
            WriteMessage(message, TraceEventType.Error, Category.Error, null);
        }

        public static void Error(object message, Category category)
        {
            WriteMessage(message, TraceEventType.Error, category, null);
        }

        public static void Error(object message, object category)
        {
            WriteMessage(message, TraceEventType.Error, category, null);
        }

        public static void Error(object message, Exception ex)
        {
            WriteMessage(message, TraceEventType.Error, null, ex);
        }

        public static void Error(object message, object category, Exception ex)
        {
            WriteMessage(message, TraceEventType.Error, category, ex);
        }

        #endregion

        #region "Critical"

        public static void Critical(object message)
        {
            WriteMessage(message, TraceEventType.Critical, null, null);
        }

        public static void Critical(object message, Category category)
        {
            WriteMessage(message, TraceEventType.Critical, category, null);
        }


        public static void Critical(object message, object category)
        {
            WriteMessage(message, TraceEventType.Critical, category, null);
        }

        public static void Critical(object message, Exception ex)
        {
            WriteMessage(message, TraceEventType.Critical, null, ex);
        }

        public static void Critical(object message, object category, Exception ex)
        {
            WriteMessage(message, TraceEventType.Critical, category, ex);
        }

        #endregion

        #region "Private Helper Methods"

        private static void WriteMessage(object message, TraceEventType eventType, object category, Exception exception)
        {

            try
            {
                LogEntry entry = new LogEntry();
                entry.Severity = eventType;
                //Add Message 
                if (((message != null)))
                {
                    entry.Message = message.ToString();
                }
                //Add category 
                if (((category != null)))
                {
                    entry.Categories.Add(category.ToString());
                }
                //Add exception if needed 
                AddExtendedProperties(entry, exception);
                entry.TimeStamp = DateTime.Now;

                //Only log the entry if we have a valid user name 
                if ((IsValidUser(entry)))
                {
                    try
                    {
                        // MicrosoftLogger.Write(entry);

                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(entry);
                    }
                    catch (Exception)
                    {
                        //Something wrong with the Logger ..make sure we dispose it properly 
                        if (((MicrosoftLogger.Writer != null)))
                        {
                            MicrosoftLogger.Writer.Dispose();
                        }
                    }

                }
            }

            catch (Exception)
            {
                //Logger exception we dont do anything about it as it wil lead to recursive call 
                //Write to the file system or event log ? 
            }


        }

        private static bool LoggingIsEnabled()
        {
            bool loggingEnabled;
            try
            {
                loggingEnabled = MicrosoftLogger.IsLoggingEnabled();
            }
            catch (ConfigurationException)
            {
                loggingEnabled = true;
            }
            catch (Exception)
            {
                loggingEnabled = true;
            }
            return loggingEnabled;
        }

        private static void AddExtendedProperties(LogEntry entry, Exception exception)
        {
            if ((entry.ExtendedProperties != null))
            {
                AddUserName(entry);
                AddExceptionDetails(entry, exception);
                AddDetailedTime(entry);
            }

        }

        private static bool IsValidUser(LogEntry entry)
        {
            bool validUser = true;
            if ((entry.ExtendedProperties != null))
            {
                object user = entry.ExtendedProperties[UserIdKey];
                if ((user != null))
                {
                    if (user.ToString() == " ")
                    {
                        validUser = false;
                    }
                }
                else
                {
                    validUser = false;
                }
            }
            return validUser;
        }

        private static void AddUserName(LogEntry entry)
        {
            string userName = GetUserName();
            entry.ExtendedProperties.Add(UserIdKey, userName);
        }

        private static void AddExceptionDetails(LogEntry entry, Exception exception)
        {
            const string stackTraceKey = "Exception";
            string exceptionDetails = "";
            if (((exception != null)))
            {
                string message = ConstructMessage(exception, "Exception Details");
                string stackTrace = ConstructStackTrace(exception);
                exceptionDetails = message + stackTrace;
                //Check for Inner Exception and add details 
                if (((exception.InnerException != null)))
                {
                    string innerExceptionMessage = ConstructMessage(exception.InnerException, " Inner Exception Details");
                    string innerExceptionStackTrace = ConstructStackTrace(exception.InnerException);
                    exceptionDetails += innerExceptionMessage;
                    exceptionDetails += innerExceptionStackTrace;
                }
            }
            entry.ExtendedProperties.Add(stackTraceKey, exceptionDetails);
        }

        private static string ConstructStackTrace(Exception exception)
        {

            return Environment.NewLine + "\t" + "Stack Trace: " + exception.StackTrace + Environment.NewLine + "\t" + " ---------------------------------------------------------------------------------------------" + Environment.NewLine;
        }

        private static string ConstructMessage(Exception exception, string exceptionHeader)
        {

            return Environment.NewLine + " ----------------------- " + exceptionHeader + " ----------------------------------------------------------------------" + Environment.NewLine + "\t" + "Message: " + exception.Message;
        }

        private static void AddDetailedTime(LogEntry entry)
        {
            //Add the time 
            //select convert(datetime, '2008-02-12 05:37:46.330', 121) 
            string detailedTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
            entry.ExtendedProperties.Add("Time", detailedTime);
        }

        private static string GetUserName()
        {
            string userName = " ";
            //Default it to unknown 
            //Try to get Name from Http Context 
            try
            {
                HttpContext currentContext = HttpContext.Current;
                if (((currentContext != null)))
                {
                    userName = currentContext.User.Identity.Name;
                }
                else
                {
                    //HttpContext doe snot exixt So get it from Current Thread 
                    if (((Thread.CurrentPrincipal != null)))
                    {
                        userName = Thread.CurrentPrincipal.Identity.Name;
                    }
                }
            }
            catch (Exception)
            {
                // Some exception has occured 
                //Need to log it to Event log or file system
            }
            return userName;
        }

        #endregion
    }

}
