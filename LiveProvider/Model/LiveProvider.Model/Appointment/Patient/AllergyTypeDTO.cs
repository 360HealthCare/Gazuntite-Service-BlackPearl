using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.Appointment.Patient
{
    [DataContract]
    [XmlType(TypeName = " AllergyType")]
    public class AllergyTypeDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "AllergyTypeIdSeq")]
        public int AllergyTypeIdSeq { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "AllergyName")]
        public string AllergyName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "UniiCode")]
        public string UniiCode { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "AllergyAgentId")]
        public int AllergyAgentId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "AllergyAgentName")]
        public string AllergyAgentName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ProblemType")]
        public string ProblemType { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "SnomedCode")]
        public string SnomedCode { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "SnomedDescription")]
        public string SnomedDescription { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "NKDA")]
        public string NKDA { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Severity")]
        public int Severity { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "NdfRt")]
        public string NdfRt { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "RxNormCode")]
        public string RxNormCode { get; set; }
    }
}
