using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public class PaymentSourceTypeDTO
    {
        [DataMember]
        public int PaymentSourceTypeID { get; set; }

        [DataMember]
        public string Description { get; set; }
       
    }
}
