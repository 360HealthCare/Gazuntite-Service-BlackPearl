using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public class PaymentLevelTypeDTO
    {        
        [DataMember]
        public int PaymentLevelTypeID { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
