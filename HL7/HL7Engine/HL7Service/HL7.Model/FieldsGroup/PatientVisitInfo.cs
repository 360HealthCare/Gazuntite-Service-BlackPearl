using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HL7.Model.Fields;

namespace HL7.Model.FieldsGroup
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 19 March 2012
    /// This class represent PATIENT VISIT INFORMATION
    /// and will map data to nhapi PV1 data type.
    /// </summary>
    public class PatientVisitInfo
    {
        public string SetID { get; set; } //Sequence ID

        public UserDefinedCodedValue PatientClass { get; set; }

        public PersonLocation AssignedPatientLocation { get; set; }

        public UserDefinedCodedValue AdmissionType { get; set; }

        public ExtendedCompositeID PreAdmitNumber { get; set; }

        public PersonLocation PriorPatientLocation { get; set; }

        public ExtCompIDAndNme AttendingDoctor { get; set; }

        public ExtCompIDAndNme ReferringDoctor { get; set; }

        public ExtCompIDAndNme ConsultingDoctor { get; set; }

        public UserDefinedCodedValue HospitalService { get; set; }

        public PersonLocation TemporaryLocation { get; set; }

        public UserDefinedCodedValue PreadmitTestIndicator { get; set; }

        public UserDefinedCodedValue AdmitSource { get; set; }

        public ExtCompIDAndNme AdmittingDoctor { get; set; }

        public UserDefinedCodedValue PatientType { get; set; }

        public ExtendedCompositeID VisitNumber { get; set; }

        public FinancialClass FinancialClass { get; set; }

        public HL7DateTime TransferToBadDebtDate { get; set; }

        public UserDefinedCodedValue BadDebtAgencyCode { get; set; }

        public Numeric BadDebtTransferAmount { get; set; }

        public Numeric BadDebtRecoveryAmount { get; set; }

        public UserDefinedCodedValue DeleteAccountIndicator { get; set; }

        public HL7DateTime DeleteAccountDate { get; set; }

        public UserDefinedCodedValue DischargeDisposition { get; set; }

        //public DLD DischargedToLocation { get; set; }

        public AllFieldsCodedWithException DietType { get; set; }

        public UserDefinedCodedValue ServicingFacility { get; set; }

        public UserDefinedCodedValue AccountStatus { get; set; }

        public PersonLocation PendingLocation { get; set; }

        public PersonLocation PriorTemporaryLocation { get; set; }

        public TimeStamp AdmitDateTime { get; set; }

        public TimeStamp DischargeDateTime { get; set; }

        public Numeric CurrentPatientBalance { get; set; }

        public Numeric TotalCharges { get; set; }

        public Numeric TotalAdjustments { get; set; }

        public Numeric TotalPayments { get; set; }

        public ExtendedCompositeID AlternateVisitID { get; set; }

        public UserDefinedCodedValue VisitIndicator { get; set; }

        public ExtCompIDAndNme OtherHealthcareProvider { get; set; }
        
    }
}
