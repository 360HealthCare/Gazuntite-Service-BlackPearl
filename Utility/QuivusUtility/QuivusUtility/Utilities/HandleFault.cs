using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Utility.Fault;
using System.ServiceModel;

namespace QuivusUtility.Utilities
{
    public class HandleFault
    {
       
        public static void HandleFautException(Exception exp)
        {
            QuivusFault userFault = new QuivusFault();
            userFault.FaultMessage = exp.Message;
            userFault.Reason = exp.Message;
            throw new FaultException<QuivusFault>(userFault,userFault.Reason);
        }
        

        public static void HandleFautException(Exception exp,String detail)
        {
            QuivusFault userFault = new QuivusFault();
            userFault.FaultMessage = exp.Message + detail;
            userFault.FaultDetail = detail;
            //M detail is also added

            userFault.Reason = exp.Message + detail; 
            throw new FaultException<QuivusFault>(userFault, userFault.Reason);
        }

        public static void HandleFautException(Exception exp, String detail,String Code)
        {
            QuivusFault userFault = new QuivusFault();
            userFault.FaultMessage = exp.Message + detail;
            userFault.FaultDetail = detail;
            userFault.Code = Code;
            //M detail is also added

            userFault.Reason = exp.Message + detail;
            throw new FaultException<QuivusFault>(userFault, userFault.Reason);
        }
    }
}
