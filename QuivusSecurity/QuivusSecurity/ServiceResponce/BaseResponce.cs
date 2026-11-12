using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ServiceResponce
{
     [DataContract]
    public class BaseResponce
    {
        [DataMember]
        public bool ResponseStatus { get; set; }

        [DataMember]
        public int ResponseCode { get; set; }

        [DataMember]
        public string ResponseMessage { get; set; }

        [DataMember]
        public string ResponseWarning { get; set; }

      

        [DataMember]
        public string ErrorCode { get; set; }

        [DataMember]
        public string ErrorDetails { get; set; }


        public StringBuilder DebuggingDetails { get; set; } // Will Used in fututre for debugginh purpose :)
       
    }
}
