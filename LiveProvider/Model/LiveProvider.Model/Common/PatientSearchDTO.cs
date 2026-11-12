using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.Common
{
    [DataContract]
    [XmlRoot(ElementName = "PatientSearch"),XmlType(TypeName="Patient")]
    public class PatientSearchDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "UserID")]
        public int UserId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ID")]
        public int PatientId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "FirstName")]
        public string FirstName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "LastName")]
        public string LastName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "MiddleName")]
        public string MiddleName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "QuivusID")]
        public string QuivusID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "DOB")]
        public DateTime DOB { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Authorized")]
        public bool IsAuthorized { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Email")]
        public bool IsEmailIdAvailable { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "SnailID")]
        public bool IsSnailIdAvailable { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "SMS")]
        public bool IsSMSNoAvalable { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Chat")]
        public bool IsChatIdAvailable { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Phone")]
        public bool IsPhoneNoAvailable { get; set; }

        [DataMember]        
        [XmlAttribute(AttributeName = "SSN")]
        public string SSN { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "GenderId")]
        public int GenderId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "GenderName")]
        public string GenderName { get; set; }
    }
}
