using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.Billing.EDIPaymentTransaction
{
    [DataContract]
    public class AdjustmentMappingDTO
    {
        [DataMember]
        public int AdjustmentMappingID { get; set; }

        [DataMember]
        public int AdjustmentReasonCodeID { get; set; }

        [DataMember]
        public int AdjustmentGroupCodeTypeID { get; set; }

        [DataMember]
        public int AdjustmentMappingTypeID { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public DateTime ModifiedAt { get; set; }

        [DataMember]
        public Boolean IsDeleted { get; set; }
    }
}
