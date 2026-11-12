using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Quivus.Utility.Fault
{
    [DataContract]
    public class QuivusFault
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