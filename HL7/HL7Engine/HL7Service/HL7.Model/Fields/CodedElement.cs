using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 14 March 2012
    /// This class represent Coded Element
    /// and will map data to nhapi CE data type.
    /// </summary>
    public class CodedElement
    {
        public string Identifier { get; set; }

        public string Text { get; set; }

        public Identity CodingSystemName { get; set; }

        public string AlternateIdentifier { get; set; }

        public string AlternateText { get; set; }

        public Identity AlternateCodingSystemName { get; set; }
    }
}
