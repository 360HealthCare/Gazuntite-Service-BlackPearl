using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HL7.Model.Fields;

namespace HL7.Model.FieldsGroup
{
    /// <summary>
    /// Author Sahibzada Sami ud din
    /// Date 29 July 2012
    /// This clasa represent Specimen segment of HL7
    /// </summary>
    public class Specimen
    {
        public string SetID { get; set; }

        public AllFieldsCodedWithException SpecimenType { get; set; }
    }
}
