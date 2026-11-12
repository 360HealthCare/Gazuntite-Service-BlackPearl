using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 14 March 2012
    /// This class represent Coded With No Exceptions
    /// and will map data to nhapi CNE data type.
    /// </summary>
    public class CodedWithNoExceptions
    {
        public string Identifier { get; set; }

        public string Text { get; set; }

        public Identity CodingSystemName { get; set; }

        public string AlternateIdentifier { get; set; }

        public string AlternateText { get; set; }

        public Identity AlternateCodingSystemName { get; set; }

        public string CodingSystemVersionID { get; set; }

        public string AltCodingSysVersionID { get; set; }

        public string OriginalText { get; set; }
    }
}
