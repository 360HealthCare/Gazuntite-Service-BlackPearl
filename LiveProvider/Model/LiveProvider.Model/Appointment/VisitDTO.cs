using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.Appointment
{
    [DataContract]
    [XmlRoot(ElementName = "Visit")]
    public class VisitDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "PrefLocation")]
        public string PreferredLocation { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "PrimProvider")]
        public string PrimaryProvider { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "RefProvider")]
        public string ReferringProvider { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "NextVisit")]
        public string NextVisit { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "LastVisit")]
        public string LastVisit { get; set; }
    }
}
