using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public class ClaimStatusCodeTypeDTO
    {
        [DataMember]
        public int ClaimStatusCodeTypeID { get; set; }

        [DataMember]
        public string ClaimStatusCode { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
