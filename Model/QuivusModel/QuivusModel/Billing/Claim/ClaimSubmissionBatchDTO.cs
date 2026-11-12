using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.Billing.Claim 
{
    /// <summary>
    /// Author: Shahid Munir
    /// Date: 12/28/2011
    /// </summary>
    [DataContract]
    public class ClaimSubmissionBatchDTO
    {
        [DataMember]
        public int BatchID { get; set; }

        [DataMember]
        public int ClaimID { get; set; }

        [DataMember]
        public DateTime BillingDate { get; set; }

        [DataMember]
        public int BillingModeTypeID { get; set; }

        [DataMember]
        public int BillingStatusTypeID { get; set; }

        [DataMember]
        public decimal TotalCharges { get; set; }

        [DataMember]
        public decimal DueAmount { get; set; }

        [DataMember]
        public int MediaTypeID { get; set; }

        [DataMember]
        public int EDIReceiverTypeID { get; set; }

        [DataMember]
        public int BillingPhysicianID { get; set; }

        [DataMember]
        public int InsuranceCarrierID { get; set; }

        [DataMember]
        public int ISAControlNumberID { get; set; }

        [DataMember]
        public int GSControlNumberID { get; set; }

        [DataMember]
        public int STControlNnumberID { get; set; }

        [DataMember]
        public int BHTControlNumberID { get; set; }

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
