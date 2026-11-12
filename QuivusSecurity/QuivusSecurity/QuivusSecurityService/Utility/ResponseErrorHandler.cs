using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceResponce;
using QuivusSecurity.Logger.Logger;

namespace QuivusSecurityService.Utility
{
    public class ResponseErrorHandler
    {
        public static void HandleException(BaseResponce serviceResponse, Exception txp)
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
            serviceResponse.ResponseStatus = false;

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
        public static void HandleException(BaseResponce serviceResponse, Exception txp, String ErrorDetails)
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
            serviceResponse.ResponseStatus = false;

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
        public static void HandleException(BaseResponce serviceResponse, Exception txp, String ErrorDetails, String ErrorCode)
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
            serviceResponse.ResponseStatus = false;

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
        /// Author Mansoor
        /// Date 12 Mar 2011
        /// This method set the error code in the response also, used for session time out in client side
        /// </summary>
        /// <param name="serviceResponse"></param>
        /// <param name="txp"></param>
        /// <param name="errorCode"></param>
        public static void HandleException(BaseResponce serviceResponse, Exception txp, int errorCode)
        {
            QuivusLogger.Info("Handling Exception :: serviceResponse:: " + serviceResponse + "txp:: " + txp.ToString());
            if (serviceResponse == null)
            {
                throw new Exception("Object Ref Set To Null");
                // serviceResponse = new ServiceResponse();
            }
            QuivusLogger.Info("ResponseMessage::" + txp.Message);
            serviceResponse.ResponseStatus = false;

            // check added by mansoor            
            if (serviceResponse.ResponseMessage == null)
            {
                serviceResponse.ResponseMessage = "";
            }

            serviceResponse.ResponseMessage += txp.Message;

            serviceResponse.ResponseCode = errorCode;
        }

      

    }
}