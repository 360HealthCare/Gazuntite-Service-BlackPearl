using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public class PaymentClaimAmountQualifierTypeDTO
    {
        
        [DataMember]
        public int PaymentClaimAmountQualifierTypeID { get; set; }

        [DataMember]
        public string PaymentClaimAmountQualifierCode { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
