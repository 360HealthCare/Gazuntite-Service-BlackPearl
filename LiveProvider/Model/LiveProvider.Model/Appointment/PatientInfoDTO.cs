using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.Appointment
{
    [DataContract]
    [XmlRoot(ElementName = "Patient")]
    public class PatientInfoDTO
    {   
        [DataMember]
        [XmlAttribute(AttributeName = "ID")]
        public int PatientID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "FirstName")]
        public string FirstName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "LastName")]
        public string LastName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "MiddleName")]
        public string MiddleName { get; set; }

        //[DataMember]
        //[XmlAttribute(AttributeName = "UserID")]
        //public int PatientUserID { get; set; }
    }
}
