using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.QuickInput
{
    [DataContract]
    public class SoapTemplateTypeDTO
    {
        [DataMember]
        public int SoapTemplateID { get; set; }

        [DataMember]
        public int ClinicBranchID { get; set; }

        [DataMember]
        public string TemplateName { get; set; }

        [DataMember]
        public string TemplateURI { get; set; }
    }
}
