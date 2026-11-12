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
    /// This class represent Patient idenetification
    /// and will map data to nhapi PID1 data type.
    /// </summary>
    public class PatientIdentification
    {
        public string ID { get; set; } // Sequence ID represent SI

        public ExtendedCompositeID PatientID { get; set; }

        public List<ExtendedCompositeID> PatientIdentifierList { get; set; }

        public List<ExtendedPersonName> PatientName { get; set; }

        public List<ExtendedPersonName> MotherSMaidenName { get; set; }

        public TimeStamp DateTimeOfBirth { get; set; }

        public UserDefinedCodedValue AdministrativeSex { get; set; }

        public List<CodedElement> Race { get; set; }

        public List<ExtendedAddress> PatientAddress { get; set; }

        public List<ExtendedTelecoNo> PhoneNumberHome { get; set; }

        public ExtendedTelecoNo PhoneNumberBusiness { get; set; }

        public AllFieldsCodedWithException PrimaryLanguage { get; set; }

        public CodedElement MaritalStatus { get; set; }

        public AllFieldsCodedWithException Religion { get; set; }

        public ExtendedCompositeID PatientAccountNumber { get; set; }

        public ExtendedCompositeID MotherSIdentifier { get; set; }

        public List<CodedElement> EthnicGroup { get; set; }

        public string BirthPlace { get; set; }

        public Identity MultipleBirthIndicator { get; set; }

        public Numeric BirthOrder { get; set; }

        public AllFieldsCodedWithException Citizenship { get; set; }

        public AllFieldsCodedWithException VeteransMilitaryStatus { get; set; }

        public TimeStamp PatientDeathDateTime { get; set; }

        public Identity PatientDeathIndicator { get; set; }

        public Identity IdentityUnknownIndicator { get; set; }

        public UserDefinedCodedValue IdentityReliabilityCode { get; set; }

        public TimeStamp LastUpdateDateTime { get; set; }

        public HierarchicDesignator LastUpdateFacility { get; set; }

        public AllFieldsCodedWithException SpeciesCode { get; set; }

        public AllFieldsCodedWithException BreedCode { get; set; }

        public string Strain { get; set; }

        public AllFieldsCodedWithException ProductionClassCode { get; set; }

        public AllFieldsCodedWithException TribalCitizenship { get; set; }
    }
}
