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
    /// This class represent Clinical Trial Identification
    /// and will map data to nhapi CTI data type.
    /// </summary>
    public class ClinicalTrialIdentification
    {
        public EntityIdentifier SponsorStudyID { get; set; }

        public CodedElement StudyPhaseIdentifier { get; set; }

        public CodedElement StudyScheduledTimePoint { get; set; }

    }
}
