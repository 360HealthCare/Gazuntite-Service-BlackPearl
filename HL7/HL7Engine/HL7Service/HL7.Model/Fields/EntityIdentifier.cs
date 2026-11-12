using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 14 March 2012
    /// This class represent Entity Identifier coded value
    /// and will map data to nhapi EI data type.
    /// </summary>
    public class EntityIdentifier
    {
        public string Identifier { get; set; }

        public UserDefinedCodedValue NamespaceID { get; set; }

        public string UniversalID { get; set; }

        public Identity UniversalIDType { get; set; }
    }
}
