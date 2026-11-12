using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public class PaymentMethodCodeTypeDTO
    {
        [DataMember]
        public int PaymentMethodCodeTypeID { get; set; }

        [DataMember]
        public string PaymentMethodCode { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
