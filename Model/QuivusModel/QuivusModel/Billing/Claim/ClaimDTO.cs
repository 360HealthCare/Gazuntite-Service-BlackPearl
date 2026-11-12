using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.Billing.Claim
{
    [DataContract]
    public class ClaimDTO
    {

        [DataMember]
        public List<ClaimTransactionDTO> Transaction { get; set; }

        [DataMember]
        public List<ClaimBillingStatusDTO> BillingStatus { get; set; }

        [DataMember]
        public List<ClaimInsuranceDTO> Insurance { get; set; }

        [DataMember]
        public int ClaimID { get; set; }

        [DataMember]
        public int ProviderID { get; set; }

        [DataMember]
        public string QuivusID { get; set; }

        [DataMember]
        public int PatientID { get; set; }

        [DataMember]
        public int VisitID { get; set; }

        [DataMember]
        public decimal TotalCharges { get; set; }        

        //[DataMember]
        //public decimal DueAmount { get; set; }       

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

        [DataMember]
        public int PatientUserID { get; set; }
    }
}
