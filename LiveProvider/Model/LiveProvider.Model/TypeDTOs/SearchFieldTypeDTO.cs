using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.TypeDTOs
{
    [DataContract]
    [XmlType(TypeName = "Search")]
    public class SearchFieldTypeDTO : LiveProviderBaseTypeDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ID")]
        public int FieldID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Name")]
        public string FieldName { get; set; }
    }
}
