using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public class PaymentProcdureQualifierTypeDTO
    {
        [DataMember]
        public int PaymentProcdureQualifierTypeID { get; set; }

        [DataMember]
        public string PaymentProcdureQualifierTypeCode { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
