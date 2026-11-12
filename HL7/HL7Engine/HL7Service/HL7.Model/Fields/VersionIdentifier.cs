using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 20 March 2012
    /// This class represent Version Identifier
    /// and will map data to nhapi VID data type.
    /// </summary>
    public class VersionIdentifier
    {
        public Identity VersionID { get; set; }

        public AllFieldsCodedWithException InternationalizationCode { get; set; }

        public AllFieldsCodedWithException InternationalVersionID { get; set; }
    }
}
