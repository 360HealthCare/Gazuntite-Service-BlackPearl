using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 14 March 2012
    /// This class represent All Fields Except OBX-5
    /// and will map data to nhapi CWE data type.
    /// </summary>
    public class AllFieldsCodedWithException
    {
        public string Identifier { get; set; }

        public string Text { get; set; }

        public Identity CodeSystemName { get; set; }

        public string AlternateIdentifier { get; set; }

        public string AlternateText { get; set; }

        public Identity AlternateCodingSystemName { get; set; }

        public string CodingSystemVersionID { get; set; }

        public string AlternateCodingSystemVersionID { get; set; }

        public string OriginalText { get; set; }

        public string SecondAlternateIdentifier { get; set; }

        public string SecondAlternateText { get; set; }

        public string SecondAlternateCodingSystemName { get; set; }

        public string SecondAlternateCodingSystemVersionID { get; set; }

        public string CodingSystemOID { get; set; }
    }
}
