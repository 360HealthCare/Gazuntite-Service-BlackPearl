using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.Appointment.Patient
{
    [DataContract]
    [XmlType(TypeName = "PatientAllergy")]
    public class PatientAllergyDTO
    {
        public PatientAllergyDTO()
        {
        }

        [DataMember]
        [XmlElement(ElementName = "AllergyType")]
        public AllergyTypeDTO AllergyType { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "PatientHistoryAllergyId")]
        public int PatientHistoryAllergyId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "PatientId")]
        public int PatientId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "IsActive")]
        public string IsActive { get; set; } // handled as bool

        //[DataMember]
        //public String AllergyTypeName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Reaction")]
        public string Reaction { get; set; }


        [DataMember]
        [XmlAttribute(AttributeName = "OnSetDate")]
        public String OnSetDate { get; set; }
    }
}
