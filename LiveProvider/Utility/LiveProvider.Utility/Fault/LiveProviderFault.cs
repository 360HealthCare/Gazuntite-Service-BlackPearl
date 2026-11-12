using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LiveProvider.Utility.Fault
{
    /// <summary>
    /// Author Muhammad Ismail
    /// Date 04 July 2011
    /// This is the fault thrown from service in case of error
    /// 
    /// </summary>
      [DataContract]
   public class LiveProviderFault
  
    {
        [DataMember]
        public Guid FaultID { get; set; }

        [DataMember]
        public string FaultMessage { get; set; }

        [DataMember]
        public string FaultDetail { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Reason
        {
            get;
            set; 
        }
    }
}
