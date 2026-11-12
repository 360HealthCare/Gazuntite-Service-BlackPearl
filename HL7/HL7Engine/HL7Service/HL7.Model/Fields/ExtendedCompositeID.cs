using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 19 March 2012
    /// This class represent Extended Composite ID with Check Digit
    /// and will map data to nhapi CX data type.
    /// </summary>
    public class ExtendedCompositeID
    {
        public string IDNumber { get; set; }

        public string CheckDigit { get; set; }

        public Identity CheckDigitScheme { get; set; }

        public HierarchicDesignator AssigningAuthority { get; set; }

        public Identity IdentifierTypeCode { get; set; }

        public HierarchicDesignator AssigningFacility { get; set; }

        public HL7DateTime EffectiveDate { get; set; }

        public HL7DateTime ExpirationDate { get; set; }

        public AllFieldsCodedWithException AssigningJurisdiction { get; set; }

        public AllFieldsCodedWithException AssigningAgencyorDepartment { get; set; }
    }
}
