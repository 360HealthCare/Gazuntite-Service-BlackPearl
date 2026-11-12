using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quivus.Utility.Logger;
using System.Text;
 
using Quivus.Utility;



namespace UserManager.Business.Utility
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

      /*  public static void HandleException(ServiceResponse serviceResponse, Exception txp)
        {
            QuivusLogger.Info("Handling Exception :: serviceResponse:: " + serviceResponse + "txp:: " + txp.ToString());
            if (serviceResponse == null)
            {
                QuivusLogger.Info("Handling Exception Service Response Is Null ");
                throw new Exception("Object Ref Set To Null");
               // serviceResponse = new ServiceResponse();
            }
            QuivusLogger.Info("ResponseMessage::" + txp.Message);
            serviceResponse.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
            serviceResponse.ResponseMessage.Append(txp.Message);
            //return serviceResponse;
        }*/

        /// <summary>
        /// Author Muhammad Ismail
        /// created Date 19 jan 2011
        /// This method Update the Response In Case Of Exception
        /// </summary>
        /// <param name="response"></param>
        /// <param name="txp"></param>
        /// <returns></returns>
      /*    public static void HandleException(ServiceResponse serviceResponse, Exception txp,String ErrorDetails)
        {
            QuivusLogger.Info("Handling Exception :: serviceResponse:: " + serviceResponse + "txp:: " + txp.ToString() + "ErrorDetails::" + ErrorDetails);
            if (serviceResponse == null)
            {
                throw new Exception("Object Ref Set To Null");
                // serviceResponse = new ServiceResponse();
            }
            QuivusLogger.Info("ResponseMessage::" + txp.Message);
            serviceResponse.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
            serviceResponse.ResponseMessage.Append(txp.Message);
            serviceResponse.ResponseMessage.Append(ErrorDetails);
            serviceResponse.ErrorDetails.Append(ErrorDetails);
            QuivusLogger.Info("ErrorDetails::" + ErrorDetails);
            //return serviceResponse;
        } */
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