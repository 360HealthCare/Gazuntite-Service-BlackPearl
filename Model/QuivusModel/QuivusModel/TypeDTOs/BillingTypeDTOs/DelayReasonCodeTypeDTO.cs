using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public class DelayReasonCodeTypeDTO
    {
        [DataMember]
        public int DelayReasonCodeTypeID { get; set; }

        [DataMember]
        public string DelayReasonCode { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Note { get; set; }
    }
}
