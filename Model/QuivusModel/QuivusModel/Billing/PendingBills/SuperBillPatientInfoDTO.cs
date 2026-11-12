using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Quivus.Model.User.Patient;

namespace QuivusModel.Billing.PendingBills
{
    /// <summary>
    /// Author: Shahid Munir
    /// Date:   01/26/2012
    /// DTO for patient info required in super bill 
    /// </summary>
    [DataContract]
    [XmlRoot(ElementName = "Patient"), XmlType(TypeName = "Patient")]
    public class SuperBillPatientInfoDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "PatientId")]
        public Int32 PatientId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "UserId")]
        public Int32 UserId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Patient")]
        public string Patient { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "QuivusID")]
        public String QuivusID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "SSN")]
        public String SSN { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Gender")]
        public String Gender { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Phone")]
        public String Phone { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "PreferredLocation")]
        public String PreferredLocation { get; set; }

        [DataMember]
        [XmlArrayAttribute("InsuranceInfo")]
        public List<SuperBillPatientInsuranceDTO> InsuranceInfo { get; set; }
        
    }
}
