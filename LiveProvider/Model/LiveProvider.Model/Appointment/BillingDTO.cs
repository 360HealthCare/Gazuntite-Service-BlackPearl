using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.Appointment
{
    [DataContract]
    [XmlRoot(ElementName = "Billing")]
    public class BillingDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "InsBalance")]
        public int InsuranceBalance { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "RejClaims")]
        public int RejectedClaims { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "PatBalance")]
        public int PatientBalance { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "OverDue")]
        public int ClaimsOverDue { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "IsChronic")]
        public bool IsChronicDiagnosis { get; set; }
    }
}
