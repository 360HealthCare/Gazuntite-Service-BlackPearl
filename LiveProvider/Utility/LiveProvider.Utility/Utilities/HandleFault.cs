using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Utility.Fault;
using System.ServiceModel;
using System.Net;

namespace LiveProvider.Utility.Utilities
{
    /// <summary>
    /// Author Muhammad Ismail
    /// Date 04 July 2011
    /// This class will be served as utility for handling exceptions & generating Faults
    /// </summary>
    public class HandleFault
    {
        /// <summary>
        /// Author Muhammad Ismail
        /// Date 04 July 2011
        /// This Method will handle the exception & generate the faults
        /// </summary>
        /// <param name="exp"></param>
        public static void HandleFautException(Exception exp)
        {
            LiveProviderFault userFault = new LiveProviderFault();
            userFault.FaultMessage = exp.Message;
            userFault.Reason = exp.Message;
            throw new FaultException<LiveProviderFault>(userFault, userFault.Reason);
            //throw new WebFaultException<LiveProviderFault>(userFault, HttpStatusCode.BadRequest);
            
        }

        /// <summary>
       /// Author Muhammad Ismail
        /// Date 04 July 2011
        /// This Method will handle the exception & generate the faults with details 
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="detail"></param>
        public static void HandleFautException(Exception exp, String detail)
        {
            LiveProviderFault userFault = new LiveProviderFault();
            userFault.FaultMessage = exp.Message + detail;
            userFault.FaultDetail = detail;
            //M detail is also added

            userFault.Reason = exp.Message + detail;
            throw new FaultException<LiveProviderFault>(userFault, userFault.Reason);
            //throw new WebFaultException<LiveProviderFault>(userFault, HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// <summary>
        /// Author Muhammad Ismail
        /// Date 04 July 2011
        /// This Method will handle the exception & generate the faults with details & code
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="detail"></param>
        /// <param name="Code"></param>
        public static void HandleFautException(Exception exp, String detail, String Code)
        {
            LiveProviderFault userFault = new LiveProviderFault();
            userFault.FaultMessage = exp.Message + detail;
            userFault.FaultDetail = detail;
            userFault.Code = Code;
            //M detail is also added

            userFault.Reason = exp.Message + detail;
            throw new FaultException<LiveProviderFault>(userFault, userFault.Reason);
            //throw new WebFaultException<LiveProviderFault>(userFault, HttpStatusCode.BadRequest);
            
        }
    }
}