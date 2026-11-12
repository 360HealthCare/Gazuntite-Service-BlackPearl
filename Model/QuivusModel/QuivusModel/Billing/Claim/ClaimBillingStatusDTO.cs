using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.Billing.Claim
{
    [DataContract]
    public class ClaimBillingStatusDTO
    {        
        [DataMember]
        public int ClaimBillingStatusID { get; set; }

        [DataMember]
        public int ClaimID { get; set; }

        [DataMember]
        public int BillingModeTypeID { get; set; }

        [DataMember]
        public int BillingStatusTypeID { get; set; }
        
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
