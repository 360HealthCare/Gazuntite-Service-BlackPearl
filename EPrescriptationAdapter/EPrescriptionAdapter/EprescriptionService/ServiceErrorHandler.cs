using System;
using System.Collections.Generic;
using System.Web;
using System.ServiceModel.Dispatcher;


namespace EprescriptionService
{
/// <summary>
/// Services can intercept errors, perform processing, and affect how errors are reported using the 
/// IErrorHandler interface. The interface has two methods that can be implemented: ProvideFault and
/// HandleError. The ProvideFault method allows you to add, modify, or suppress a fault message that 
/// is generated in response to an exception. The HandleError method allows error processing to take 
/// place in the event of an error and controls whether additional error handling can run.
/// 
/// To use this class, specify it as the type in the ErrorBehavior attribute constructor.
/// </summary>
    public class ServiceErrorHandler : IErrorHandler
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ServiceErrorHandler()
        {
        }

        /// <summary>
        /// Specifies a url of the service
        /// </summary>
        /// <param name="strUrl"></param>
        public ServiceErrorHandler(string strUrl, bool bHandled)
        {
            this.strUrl_m = strUrl;
            this.bHandled_m = bHandled;
        }

        /// <summary>
        ///HandleError. Log an error, then allow the error to be handled as usual. 
        ///Return true if the error is considered as already handled
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public virtual bool HandleError(Exception exError)
        {
         
            //TODO: log error
            //EventLogManager.LogEvent(SharedTypes.SeverityTypeEnum.E, "WCF Service Error", null);

            return this.bHandled_m;
        }

        /// <summary>
        ///Provide a fault. The Message fault parameter can be replaced, or set to
        ///null to suppress reporting a fault.
        /// </summary>
        /// <param name="error"></param>
        /// <param name="version"></param>
        /// <param name="msg"></param>
        public virtual void ProvideFault(Exception exError,
            System.ServiceModel.Channels.MessageVersion version,
            ref System.ServiceModel.Channels.Message msg)
        {
            //Any custom message here

            //TODO: create quivus fault contract 
            //QuivusFault dafc = new QuivusFaultContract(exError.Message);
          

            //System.ServiceModel.FaultException fe = new System.ServiceModel.FaultException<QuivusFaultContract>(dafc);
            //System.ServiceModel.Channels.MessageFault fault = fe.CreateMessageFault();

            string ns = "QuivusEprescriptionService";
          //  msg = System.ServiceModel.Channels.Message.CreateMessage(version, fault, ns);
            
        }

        private string strUrl_m;
        /// <summary>
        /// Specifies a url of the service, displayed in the error log
        /// </summary>
        public string Url
        {
            get
            {
                return this.strUrl_m;
            }
        }

        private bool bHandled_m;
        /// <summary>
        /// Determines if the exception should be considered handled
        /// </summary>
        public bool Handled
        {
            get
            {
                return this.bHandled_m;
            }
        }
    }
}

