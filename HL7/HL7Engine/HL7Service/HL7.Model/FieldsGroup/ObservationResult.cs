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
    /// This class represent Observation/Result
    /// and will map data to nhapi OBX data type.
    /// </summary>
    public class ObservationResult
    {
        public string SetID { get; set; } //sequence number

        public Identity ValueType { get; set; }

        public AllFieldsCodedWithException ObservationIdentifier { get; set; }

        public string ObservationSubID { get; set; }

        public List<string> ObservationValue { get; set; }

        public AllFieldsCodedWithException Units { get; set; }

        public string ReferencesRange { get; set; }

        public AllFieldsCodedWithException AbnormalFlags { get; set; }

        public Numeric Probability { get; set; }

        public Identity NatureOfAbnormalTest { get; set; }

        public Identity ObservationResultStatus { get; set; }

        public TimeStamp EffectiveDateOfReferenceRange { get; set; }

        public string UserDefinedAccessChecks { get; set; }

        public TimeStamp ObservationDateTime { get; set; }

        public AllFieldsCodedWithException ProducerSReference { get; set; }

        public ExtCompIDAndNme ResponsibleObserver { get; set; }

        public AllFieldsCodedWithException ObservationMethod { get; set; }

        public EntityIdentifier EquipmentInstanceIdentifier { get; set; }

        public TimeStamp AnalysisDateTime { get; set; }

        public OrgExtCompNameAndIdentity PerformingOrganizationName { get; set; }

        public ExtendedAddress PerformingOrganizationAddress { get; set; }

        public ExtCompIDAndNme PerformingOrganizationMedicalDirector { get; set; }


    }
}
