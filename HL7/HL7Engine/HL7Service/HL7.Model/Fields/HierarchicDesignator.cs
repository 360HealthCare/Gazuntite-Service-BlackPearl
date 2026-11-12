using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 14 March 2012
    /// This class represent Hierarchic Designator
    /// and will map data to nhapi HD data type.
    /// </summary>
    public class HierarchicDesignator
    {
        public UserDefinedCodedValue NamespaceID { get; set; }

        public string UniversalID { get; set; }

        public Identity UniversalIDType { get; set; }
    }
}
