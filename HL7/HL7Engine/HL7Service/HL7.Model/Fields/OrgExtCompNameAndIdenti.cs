using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 14 March 2012
    /// This class represent Extended Composite Name and Identification Number for Organizations
    /// and will map data to nhapi XON data type.
    /// </summary>
    public class OrgExtCompNameAndIdentity
    {
        public string OrganizationName { get; set; }

        public UserDefinedCodedValue OrganizationNameTypeCode { get; set; }

        public Numeric CheckDigit { get; set; }

        public Identity CheckDigitScheme { get; set; }

        public HierarchicDesignator AssigningAuthority { get; set; }

        public Identity IdentifierTypeCode { get; set; }

        public HierarchicDesignator AssigningFacility { get; set; }

        public Identity NameRepresentationCode { get; set; }

        public string OrganizationIdentifier { get; set; }
    }
}
