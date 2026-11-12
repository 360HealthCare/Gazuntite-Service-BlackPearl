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
    /// This class represent PATIENT VISIT – ADDITIONAL INFORMATION SEGMENT
    /// and will map data to nhapi PV2 data type.
    /// </summary>
    public class PatientVisitInfo2
    {
        public AllFieldsCodedWithException AdmitReason { get; set; }

        public Identity EmploymentIllnessRelatedIndicator { get; set; }

        public OrgExtCompNameAndIdentity ClinicOrganizationName { get; set; }

        public HL7DateTime PreviousTreatmentDate { get; set; }

        public HL7DateTime FirstSimilarIllnessDate { get; set; }

        public AllFieldsCodedWithException AdmissionLevelCareCode { get; set; }

    }
}
