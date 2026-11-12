using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.ELab
{
    [DataContract]
    [XmlType(TypeName = "Result")]
    public class ResultManagementDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "Status")]
        public string Status { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Comments")]
        public string Comments { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "File")]
        public string ResultFile { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Ranage")]
        public string Ranage { get; set; }
    }
}
