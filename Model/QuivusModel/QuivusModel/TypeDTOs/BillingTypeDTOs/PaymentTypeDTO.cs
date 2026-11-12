using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public class PaymentTypeDTO
    {
        [DataMember]
        public int PaymentTypeID { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
