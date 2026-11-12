using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public class PaymentProcedureAmountQualifierTypeDTO
    {
        [DataMember]
        public int PaymentProcedureAmountQualifierTypeID { get; set; }

        [DataMember]
        public string PaymentProcedureAmountQualifierCode { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
