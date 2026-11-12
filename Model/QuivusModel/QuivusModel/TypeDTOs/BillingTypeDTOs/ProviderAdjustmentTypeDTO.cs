using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public class ProviderAdjustmentTypeDTO
    {
        
        [DataMember]
        public int ProviderAdjustmentReasonCodeTypeID { get; set; }

        [DataMember]
        public string ProviderAdjustmentReasonCode { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
