using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.Billing.Claim
{
    [DataContract]
    public class ClaimAdjustmentDTO
    {
        [DataMember]
        public int ClaimAdjustmentID { get; set; }

        [DataMember]
        public int ClaimPaymentID { get; set; }        

        [DataMember]
        public int AdjustmentReasonCodeID { get; set; }

        [DataMember]
        public int AdjustmentGroupCodeTypeID { get; set; }

        [DataMember]
        public Decimal AdjustmentAmount { get; set; }

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
