using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.Billing.Claim 
{
    [DataContract]
    public class ClaimInsuranceDTO
    {
        [DataMember]
        public int ClaimInsuranceID { get; set; }

        [DataMember]
        public int ClaimID { get; set; }

        [DataMember]
        public int InsuranceCarrierID { get; set; }

        [DataMember]
        public int InsuranceTypeId { get; set; }

        [DataMember]
        public int MediaTypeID { get; set; }

        [DataMember]
        public int RelationShipID { get; set; }

        [DataMember]
        public int PatientGuarantorID { get; set; }

        [DataMember]
        public int ClaimFrequencyCodeTypeID { get; set; }

        [DataMember]
        public int DelayReasonCodeTypeID { get; set; }

        [DataMember]
        public decimal TertiaryInsurancePayment { get; set; }

        [DataMember]
        public string PolicyNumber { get; set; }

        [DataMember]
        public string GroupName { get; set; }

        [DataMember]
        public string GroupNumber { get; set; }

        [DataMember]
        public int CMSSecondaryTypeID { get; set; }

        [DataMember]
        public string ICN { get; set; }

        [DataMember]
        public string ClaimNote { get; set; }

        [DataMember]
        public int EDIReceiverTypeID { get; set; }

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
