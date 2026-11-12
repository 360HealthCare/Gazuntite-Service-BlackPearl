using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.TypeDTOs.ELabs.QuickInput
{
    [DataContract]
    [XmlType(TypeName = "Laboratory")]
    public class LabTypeDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ID")]
        public int LabTypeID { get; set; }
        [DataMember]
        [XmlAttribute(AttributeName = "Name")]
        public string LabName { get; set; }
    }
}
