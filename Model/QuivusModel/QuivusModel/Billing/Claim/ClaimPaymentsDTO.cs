using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.Billing.Claim
{
    [DataContract]
    public class ClaimPaymentsDTO
    {
        [DataMember]
        public int ClaimPaymentID { get; set; }

        [DataMember]
        public int ClaimID { get; set; }

        [DataMember]
        public int InsuranceCarrierId { get; set; }

        [DataMember]
        public DateTime EntryDate { get; set; }

        [DataMember]
        public int PaymentSourceTypeID { get; set; }

        [DataMember]
        public int PaymentTypeID { get; set; }

        [DataMember]
        public string ChargedProcedure { get; set; }

        [DataMember]
        public string PaidProcedure { get; set; }

        [DataMember]
        public string Modifier1 { get; set; }

        [DataMember]
        public string Modifier2 { get; set; }

        [DataMember]
        public string Modifier3 { get; set; }

        [DataMember]
        public string Modifier4 { get; set; }

        [DataMember]
        public DateTime ServiceDateFrom { get; set; }

        [DataMember]
        public DateTime ServiceDateTo { get; set; }

        [DataMember]
        public string CheckNumber { get; set; }

        [DataMember]
        public DateTime FilingDate { get; set; }

        [DataMember]
        public Decimal AllowedAmount { get; set; }

        [DataMember]
        public Decimal PaidAmount { get; set; }

        [DataMember]
        public Decimal AdjustedAmount { get; set; }

        [DataMember]
        public Decimal RejectedAmount { get; set; }

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
