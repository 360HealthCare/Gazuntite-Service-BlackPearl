using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 19 March 2012
    /// This class represent Extended Person Name
    /// and will map data to nhapi XPN data type.
    /// </summary>
    public class ExtendedPersonName
    {
        public FamilyName FamName { get; set; }

        public string GivenName { get; set; }

        public string SecondAndFutherName { get; set; }

        public string Suffix { get; set; }

        public string Prefix { get; set; }

        public UserDefinedCodedValue Degree { get; set; }

        public Identity NameTypeCode { get; set; }

        public Identity NameRepresentationCode { get; set; }

        public AllFieldsCodedWithException NameContext { get; set; }

        public Identity NameAssemblyOrder { get; set; }

        public TimeStamp EffectiveDate { get; set; }

        public TimeStamp ExpirationDate { get; set; }

        public string ProfessionalSuffix { get; set; }
    }
}
