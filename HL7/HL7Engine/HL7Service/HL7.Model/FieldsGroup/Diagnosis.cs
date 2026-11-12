using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HL7.Model.Fields;

namespace HL7.Model.FieldsGroup
{
    public class Diagnosis
    {
        public string SetID { get; set; }

        public CodedElement DiagnosisCode { get; set; }

        public UserDefinedCodedValue DiagnosisType { get; set; }

        public Identity DiagnosisPriority { get; set; }

    }
}
