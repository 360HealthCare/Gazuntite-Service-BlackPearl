using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 14 March 2012
    /// This class represent Extended Composite ID Number and Name for Persons
    /// and will map data to nhapi XCN data type.
    /// </summary>
    public class ExtCompIDAndNme
    {
        public string IDNumber { get; set; }

        public FamilyName FamilyName { get; set; }

        public string GivenName { get; set; }

        public string InitialsThereof { get; set; }

        public string Suffix { get; set; }

        public string Prefix { get; set; }

        public UserDefinedCodedValue Degree { get; set; }

        public UserDefinedCodedValue SourceTable { get; set; }

        public HierarchicDesignator AssigningAuthority { get; set; }

        public Identity NameTypeCode { get; set; }

        public string IdentifierCheckDigit { get; set; }

        public Identity CheckDigitScheme { get; set; }

        public Identity IdentifierTypeCode { get; set; }

        public HierarchicDesignator AssigningFacility { get; set; }

        public Identity NameRepresentationCode { get; set; }

        public CodedElement NameContext { get; set; }

        public Identity AssemblyOrderName { get; set; }

        public TimeStamp EffectiveDate { get; set; }

        public string ProfessionalSuffix { get; set; }

        public TimeStamp ExpirationDate { get; set; }

        public AllFieldsCodedWithException AssigningJurisdiction { get; set; }

        public AllFieldsCodedWithException AssigningAgencyDepartment { get; set; }

    }
}
