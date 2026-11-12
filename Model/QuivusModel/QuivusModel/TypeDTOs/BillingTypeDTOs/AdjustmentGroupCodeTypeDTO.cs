using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public class AdjustmentGroupCodeTypeDTO
    {
        [DataMember]
        public int AdjustmentGroupCodeTypeID { get; set; }

        [DataMember]
        public string AdjustmentGroupCode { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
