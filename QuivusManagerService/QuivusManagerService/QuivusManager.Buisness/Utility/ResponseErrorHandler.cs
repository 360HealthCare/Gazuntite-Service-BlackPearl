using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quivus.Utility.Logger;
using System.Text;
using Quivus.Utility;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomSession;
using QuivusManager.Buisness.ApplicationSession;



namespace QuivusManager.Buisness.Utility
{
    public class ResponseErrorHandler
    {
        /// <summary>
        /// Author Muhammad Ismail
        /// created Date 14 jan 2011
        /// This method Update the Response In Case Of Exception
        /// </summary>
        /// <param name="response"></param>
        /// <param name="txp"></param>
        /// <returns></returns>

        public static void HandleException(QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse serviceResponse, Exception txp)
        {
            QuivusLogger.Info("Handling Exception :: serviceResponse:: " + serviceResponse + "txp:: " + txp.ToString());
            if (serviceResponse == null)
            {
                QuivusLogger.Info("Handling Exception Service Response Is Null ");
                if (serviceResponse.ResponseMessage == null)
                {
                    serviceResponse.ResponseMessage = "Handling Exp Object Ref Set To Null";
                    return;
                }

                // throw new Exception("Object Ref Set To Null");
            }

            QuivusLogger.Info("ResponseMessage::" + txp.Message);
            serviceResponse.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;

            // check added by mansoor            
            if (serviceResponse.ResponseMessage == null)
            {
                serviceResponse.ResponseMessage = "";
            }
            serviceResponse.ResponseMessage += txp.Message;

            QuivusLogger.Info("ErrorDetails::" + txp.ToString());
            //return serviceResponse;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// created Date 19 jan 2011
        /// This method Update the Response In Case Of Exception
        /// </summary>
        /// <param name="response"></param>
        /// <param name="txp"></param>
        /// <returns></returns>
        public static void HandleException(QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse serviceResponse, Exception txp, String ErrorDetails)
        {
            QuivusLogger.Info("Handling Exception :: serviceResponse:: " + serviceResponse + "txp:: " + txp.ToString() + "ErrorDetails::" + ErrorDetails);
            if (serviceResponse == null)
            {
                if (serviceResponse.ResponseMessage == null)
                {
                    serviceResponse.ResponseMessage = "Handling Exp Object Ref Set To Null";
                    return;
                }

               // throw new Exception("Object Ref Set To Null");
                // serviceResponse = new ServiceResponse();
            }
            QuivusLogger.Info("ResponseMessage::" + txp.Message);
            serviceResponse.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;

            // check added by mansoor            
            if (serviceResponse.ResponseMessage == null)
            {
                serviceResponse.ResponseMessage = "";
            }

            serviceResponse.ResponseMessage += txp.Message;

            serviceResponse.ResponseMessage += ErrorDetails;

            //M serviceResponse.ErrorDetails = sb.Append(ErrorDetails);

            QuivusLogger.Info("ErrorDetails::" + ErrorDetails);
            //return serviceResponse;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// created Date 19 jan 2011
        /// This method Update the Response In Case Of Exception
        /// </summary>
        /// <param name="response"></param>
        /// <param name="txp"></param>
        /// <returns></returns>
        public static void HandleException(QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse serviceResponse, Exception txp, String ErrorDetails, String ErrorCode)
        {
            QuivusLogger.Info("Handling Exception :: serviceResponse:: " + serviceResponse + "txp:: " + txp.ToString() + "ErrorDetails::" + ErrorDetails);
            if (serviceResponse == null)
            {
                if (serviceResponse.ResponseMessage == null)
                {
                    serviceResponse.ResponseMessage = "Handling Exp Object Ref Set To Null";
                    return;
                }

              //  throw new Exception("Object Ref Set To Null");
                // serviceResponse = new ServiceResponse();
            }
            QuivusLogger.Info("ResponseMessage::" + txp.Message);
            serviceResponse.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;

            // check added by mansoor            
            if (serviceResponse.ResponseMessage == null)
            {
                serviceResponse.ResponseMessage = "";
            }

            serviceResponse.ResponseMessage += txp.Message;

            serviceResponse.ResponseMessage += ErrorDetails;

            serviceResponse.ErrorCode = ErrorCode; // Error Details Are Added 

            //M serviceResponse.ErrorDetails = sb.Append(ErrorDetails);

            QuivusLogger.Info("ErrorDetails::" + ErrorDetails);
            //return serviceResponse;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// created Date 19 jan 2011
        /// This method Update the Response In Case Of Exception
        /// </summary>
        /// <param name="response"></param>
        /// <param name="txp"></param>
        /// <returns></returns>
        public static void HandleException(QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse serviceResponse, Exception txp, String ErrorDetails, String ErrorCode, UserSessionDTO userSession)
        {

            QuivusLogger.Info("Handling Exception :: serviceResponse:: " + serviceResponse + "txp:: " + txp.ToString() + "ErrorDetails::" + ErrorDetails);
            if (serviceResponse == null || txp == null)
            {
                if (serviceResponse.ResponseMessage == null)
                {
                    serviceResponse.ResponseMessage = "Handling Exp Object Ref Set To Null";
                    return;
                }

                // throw new Exception("Handling Exp Object Ref Set To Null");
                // serviceResponse = new ServiceResponse();
            }
            QuivusLogger.Info("ResponseMessage::" + txp.Message);
            serviceResponse.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;

            // check added by mansoor            
            if (serviceResponse.ResponseMessage == null)
            {
                serviceResponse.ResponseMessage = "";
            }

            serviceResponse.ResponseMessage += txp.Message;

            serviceResponse.ResponseMessage += ErrorDetails;

            serviceResponse.ErrorCode = ErrorCode; // Error Details Are Added 

            //M serviceResponse.ErrorDetails = sb.Append(ErrorDetails);

            QuivusLogger.Info("ErrorDetails::" + ErrorDetails);

            LogDebuggingDetails(serviceResponse, txp, ErrorDetails, ErrorCode, userSession);

            //return serviceResponse;
        }

        /// <summary>
        /// Autor Ismail
        /// Date 08 June 2011
        /// Method Log error details for Debugging
        /// </summary>
        /// <param name="serviceResponse"></param>
        /// <param name="txp"></param>
        /// <param name="ErrorDetails"></param>
        /// <param name="ErrorCode"></param>
        /// <param name="userSession"></param>
        private static void LogDebuggingDetails(QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse serviceResponse, Exception txp, String ErrorDetails, String ErrorCode, UserSessionDTO userSession)
        {
            if (userSession != null)
            {
                UserSessionDTO obj = QApplicationSession.GetUserSession(userSession.SessionID);
                if (obj != null)
                {
                    if (serviceResponse.DebuggingDetails == null)
                        serviceResponse.DebuggingDetails = new StringBuilder();

                    serviceResponse.DebuggingDetails.Append("UserId::");
                    serviceResponse.DebuggingDetails.Append(obj.UserID);
                    serviceResponse.DebuggingDetails.Append("-UserTypeID::");
                    serviceResponse.DebuggingDetails.Append(obj.UserTypeID);
                    serviceResponse.DebuggingDetails.Append("-ClinicBranchID::");
                    serviceResponse.DebuggingDetails.Append(obj.ClinicBranchID);
                    serviceResponse.DebuggingDetails.Append("-Error Message::");
                    serviceResponse.DebuggingDetails.Append(txp.Message);
                    serviceResponse.DebuggingDetails.Append("-Error Details::");
                    serviceResponse.DebuggingDetails.Append(ErrorDetails);
                    serviceResponse.DebuggingDetails.Append("-Error Code::");
                    serviceResponse.DebuggingDetails.Append(ErrorCode);


                }
            }
        }

        /// <summary>
        /// Author Mansoor
        /// Date 12 Mar 2011
        /// This method set the error code in the response also, used for session time out in client side
        /// </summary>
        /// <param name="serviceResponse"></param>
        /// <param name="txp"></param>
        /// <param name="errorCode"></param>
        public static void HandleException(QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse serviceResponse, Exception txp, int errorCode)
        {
            QuivusLogger.Info("Handling Exception :: serviceResponse:: " + serviceResponse + "txp:: " + txp.ToString());
            if (serviceResponse == null)
            {
                throw new Exception("Object Ref Set To Null");
                // serviceResponse = new ServiceResponse();
            }
            QuivusLogger.Info("ResponseMessage::" + txp.Message);
            serviceResponse.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;

            // check added by mansoor            
            if (serviceResponse.ResponseMessage == null)
            {
                serviceResponse.ResponseMessage = "";
            }

            serviceResponse.ResponseMessage += txp.Message;

            serviceResponse.ResponseCode = errorCode;
        }

        ///// <summary>
        ///// Author Mansoor
        ///// created Date 14 jan 2011
        ///// This method Update the Response In Case Of Exception 
        ///// </summary>
        ///// <param name="response"></param>
        ///// <param name="txp"></param>
        ///// <returns></returns>

        //public static void HandleLoginException(ServiceResponse response, Exception exp)
        //{
        //    if (response == null)
        //    {
        //        response = new ServiceResponse();
        //        response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
        //    }
        //    response.ResponseMessage = exp.Message;
        //    //return response;
        //}   


    }
}