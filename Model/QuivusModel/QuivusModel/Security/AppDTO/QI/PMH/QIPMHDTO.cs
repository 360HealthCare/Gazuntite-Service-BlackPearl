using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using QuivusModel.Security.AppDTO.BaseDTO;

namespace QuivusModel.Security.AppDTO.QI.PMH
{
    
    [DataContract]
    [XmlType(TypeName = "QIPMH")]
    public class QIPMHDTO : ModuleARBaseDTO 
    {
        [DataMember]
        [XmlElement(ElementName = "QIAllergy")]
        public QIAllergyDTO Allergy { get; set; }
    }
}
