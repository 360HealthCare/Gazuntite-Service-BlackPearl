using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 19 March 2012
    /// This class represent Composite ID Number and Name Simplified
    /// and will map data to nhapi CNN data type.
    /// </summary>
    public class CompositeIDNoAndNameSimplified
    {
        public string IDNumber { get; set; }

        public string FamilyName { get; set; }

        public string GivenName { get; set; }

        public string SecondAndFutherName { get; set; }

        public string Suffix { get; set; }

        public string Prefix { get; set; }

        public UserDefinedCodedValue Degree { get; set; }

        public UserDefinedCodedValue AssigningAuthorityNamespaceID { get; set; }

        public string AssigningAuthorityUniversalID { get; set; }

        public Identity AssigningAuthorityUniversalIDType { get; set; }
    }
}
