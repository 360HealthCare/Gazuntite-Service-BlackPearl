using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.TypeDTOs.ELabs.LiveProvider
{
    [DataContract]
    [XmlType(TypeName = "Field")]
    public class SearchFieldsDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ID")]
        public int FieldID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Name")]
        public string FieldName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Order")]
        public bool IsOrder { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Result")]
        public bool IsResult { get; set; }
    }
}
