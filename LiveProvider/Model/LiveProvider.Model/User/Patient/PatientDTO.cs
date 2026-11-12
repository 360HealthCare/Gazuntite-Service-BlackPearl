using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace LiveProvider.Model.User.Patient
{
    [DataContract]
    [XmlRoot(ElementName = "Patient")]
    public class PatientDTO
    {

        [DataMember]
        [XmlAttribute(AttributeName = "ID")]
        public int PatientId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "FirstName")]
        public string PatientFirstName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "LastName")]
        public string PatientLastName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "MiddleName")]
        public string PatientMiddleName { get; set; }
    }
}
