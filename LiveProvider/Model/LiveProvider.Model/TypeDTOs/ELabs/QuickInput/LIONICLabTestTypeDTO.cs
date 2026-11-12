using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.TypeDTOs.ELabs.QuickInput
{
    [DataContract]
    [XmlType(TypeName = "Test")]
    public class LIONICLabTestTypeDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ID")]
        public int LionicLabTestTypeID { get; set; }
        [DataMember]
        [XmlAttribute(AttributeName = "Num")]
        public string LOINC_NUM { get; set; }
        [DataMember]
        [XmlAttribute(AttributeName = "Name")]
        public string SHORTNAME { get; set; }
    }
}
