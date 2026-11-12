using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.Billing.EDICommon
{
    [DataContract]
    public class GSControlNumberDTO
    {
        [DataMember]
        public int GSControlNumberID { get; set; }

        [DataMember]
        public string FormatType { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }
    }
}
