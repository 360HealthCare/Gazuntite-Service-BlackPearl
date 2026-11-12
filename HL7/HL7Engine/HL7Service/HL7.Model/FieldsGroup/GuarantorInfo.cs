using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HL7.Model.Fields;

namespace HL7.Model.FieldsGroup
{
    /// <summary>
    /// Author Sahibzada Sami ud din
    /// Date 17 July 2012
    /// This class will represent Guarantor and map to GTI segment of HL7
    /// </summary>
    public class GuarantorInfo
    {
        public string SetID { get; set; }

        public ExtendedPersonName GuarantorName { get; set; }

        public ExtendedAddress GuarantorAddress { get; set; }

        public UserDefinedCodedValue GuarantorType { get; set; }

        public OrgExtCompNameAndIdentity GuarantorOrgNme { get; set; }

    }
}
