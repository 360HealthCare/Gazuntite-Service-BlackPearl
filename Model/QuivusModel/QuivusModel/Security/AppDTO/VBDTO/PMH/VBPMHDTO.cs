using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using QuivusModel.Security.AppDTO.BaseDTO;

namespace QuivusModel.Security.AppDTO.VBDTO.PMH
{
    
    [DataContract]
    [XmlType(TypeName = "VBPMH")]
    public class VBPMHDTO : ModuleARBaseDTO 
    {
        [DataMember]
        [XmlElement(ElementName = "VBAllergy")]
        public VBAllergyDTO VBAllergy { get; set; }
    }
}
