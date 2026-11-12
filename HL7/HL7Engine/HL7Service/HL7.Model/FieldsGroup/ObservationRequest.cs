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
    /// This class represent Observation Request
    /// and will map data to nhapi OBR data type.
    /// </summary>
    public class ObservationRequest
    {
        public string SetID { get; set; } //Sequence ID

        public EntityIdentifier PlacerOrderNumber { get; set; }

        public EntityIdentifier FillerOrderNumber { get; set; }

        public AllFieldsCodedWithException UniversalServiceIdentifier { get; set; }

        public TimeStamp ObservationDateTime { get; set; }

        public TimeStamp ObservationEndDateTime { get; set; }

        public ExtCompIDAndNme CollectorIdentifier { get; set; }

        public Identity SpecimenActionCode { get; set; }

        public AllFieldsCodedWithException DangerCode { get; set; }

        public string RelevantClinicalInformation { get; set; }

        public ExtCompIDAndNme OrderingProvider { get; set; }

        public ExtendedTelecoNo OrderCallbackPhoneNumber { get; set; }

        public string PlacerField1 { get; set; }

        public string PlacerField2 { get; set; }

        public string FillerField1 { get; set; }

        public string FillerField2 { get; set; }

        public TimeStamp ResultsRptDateTime { get; set; }

        //public MOC ChargeToPractice{get;set;}

        public Identity DiagnosticServSectID { get; set; }

        public Identity ResultStatus { get; set; }

        public ParentResultLink ParentResult { get; set; }

        public List<ExtCompIDAndNme> ResultCopiesTo { get; set; }

        public EntityIdentifierPair Parent { get; set; }

        public AllFieldsCodedWithException ReasonForStudy { get; set; }

        public NameWithDateAndLocation PrincipalResultInterpreter { get; set; }

        public NameWithDateAndLocation AssistantResultInterpreter { get; set; }

        public NameWithDateAndLocation Technician { get; set; }

        public NameWithDateAndLocation Transcriptionist { get; set; }

        public TimeStamp ScheduledDateTime { get; set; }

        public AllFieldsCodedWithException CollectorSComment { get; set; }

        public AllFieldsCodedWithException ProcedureCode { get; set; }

        public AllFieldsCodedWithException ProcedureCodeModifier { get; set; }

        //public AllFieldsCodedWithException ProcedureCodeModifier { get; set; }

        public AllFieldsCodedWithException PlacerSupplementalServiceInfo { get; set; }

        public AllFieldsCodedWithException FillerSupplementalServiceInfo { get; set; }

        public AllFieldsCodedWithException MedicallyNecessaryDuplicateProcedureReason { get; set; }

        public UserDefinedCodedValue ResultHandling { get; set; }

        public AllFieldsCodedWithException ParentUniversalServiceIdentifier { get; set; }
    }
}
