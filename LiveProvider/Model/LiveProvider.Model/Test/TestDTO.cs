using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.Test
{

    [DataContract]
    [XmlRoot(ElementName = "Test")]
    public class TestDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Code")]
        public string Code { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ID")]
        public int TestId { get; set; }
    }
}
