using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HL7.Model.Fields;

namespace HL7.Model.FieldsGroup
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 20 March 2012
    /// This class represent Insurance
    /// and will map data to nhapi IN1 data type.
    /// </summary>
    public class Insurance
    {
        public string SetID { get; set; } //Sequence ID

        public CodedElement InsurancePlanID { get; set; }

        public ExtendedCompositeID InsuranceCompanyID { get; set; }

        public OrgExtCompNameAndIdentity InsuranceCompanyName { get; set; }

        public ExtendedAddress InsuranceCompanyAddress { get; set; }

        public ExtendedPersonName InsuranceCoContactPerson { get; set; }

        public ExtendedTelecoNo InsuranceCoPhoneNumber { get; set; }

        public string GroupNumber { get; set; }

        public OrgExtCompNameAndIdentity GroupName { get; set; }

        public ExtendedCompositeID InsuredSGroupEmpID { get; set; }

        public OrgExtCompNameAndIdentity InsuredSGroupEmpName { get; set; }

        public HL7DateTime PlanEffectiveDate { get; set; }

        public HL7DateTime PlanExpirationDate { get; set; }

        //public AUI AuthorizationInformation { get; set; }

        public UserDefinedCodedValue PlanType { get; set; }

        public ExtendedPersonName NameOfInsured { get; set; }

        public CodedElement InsuredSRelationshipToPatient { get; set; }

        public TimeStamp InsuredSDateOfBirth { get; set; }

        public ExtendedAddress InsuredSAddress { get; set; }

        public UserDefinedCodedValue AssignmentOfBenefits { get; set; }

        public UserDefinedCodedValue CoordinationOfBenefits { get; set; }

        public string CoordinationOfBenPriority { get; set; }

        public Identity NoticeOfAdmissionFlag { get; set; }

        public HL7DateTime NoticeOfAdmissionDate { get; set; }

        public Identity ReportOfEligibilityFlag { get; set; }

        public HL7DateTime ReportOfEligibilityDate { get; set; }

        public UserDefinedCodedValue ReleaseInformationCode { get; set; }

        public string PreAdmitCert { get; set; }

        public TimeStamp VerificationDateTime { get; set; }

        public ExtCompIDAndNme VerificationBy { get; set; }

        public UserDefinedCodedValue TypeOfAgreementCode { get; set; }

        public UserDefinedCodedValue BillingStatus { get; set; }

        public Numeric LifetimeReserveDays { get; set; }

        public Numeric DelayBeforeLRDay { get; set; }

        public UserDefinedCodedValue CompanyPlanCode { get; set; }

        public string PolicyNumber { get; set; }



    }
}
