using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using LiveProvider.Model.Appointment.Patient;
using System.Xml.Serialization;

namespace LiveProvider.Model.Appointment
{
    [DataContract]
    [XmlType(TypeName = "PatientInfo")]
    public class PatientCompleteInfoDTO
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
        public string QuivusId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "SSN")]
        public string SSN { get; set; }

        //[DataMember]
        //[XmlAttribute(AttributeName = "RecGuid")]
        //public Guid RecGuid { get; set; } by sami on front end request

        [DataMember]
        [XmlAttribute(AttributeName = "DOB")]
        public DateTime DateOfBirth { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "GenderID")]
        public int GenderId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "GenderName")]
        public string GenderName { get; set; }

        [DataMember]
        [XmlElement(ElementName = "Billing")]
        public BillingDTO Billing { get; set; }

        //[DataMember]
        //[XmlAttribute(AttributeName = "InsBalance")]
        //public int InsuranceBalance { get; set; }

        //[DataMember]
        //[XmlAttribute(AttributeName = "RejClaims")]
        //public int RejectedClaims { get; set; }

        //[DataMember]
        //[XmlAttribute(AttributeName = "PatBalance")]
        //public int PatientBalance { get; set; }

        //[DataMember]
        //[XmlAttribute(AttributeName = "OverDue")]
        //public int ClaimsOverDue { get; set; }

        //[DataMember]
        //[XmlAttribute(AttributeName = "IsChronic")]
        //public bool IsChronicDiagnosis { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [DataMember]
        [XmlArrayAttribute("Insurances")]
        public List<PatientInsuranceDTO> PatientInsuranceList { get; set; }

        [DataMember]
        [XmlArrayAttribute("PatientAllergies")]
        public List<PatientAllergyDTO> PatientAllergyList { get; set; }

        //Data Not Avaliable by Mansoor

        [DataMember]
        [XmlElement(ElementName = "Visit")]
        public VisitDTO Visit { get; set; }

        //[DataMember]
        //[XmlAttribute(AttributeName = "PrefLocation")]
        //public string PreferredLocation { get; set; }

        //[DataMember]
        //[XmlAttribute(AttributeName = "PrimProvider")]
        //public string PrimaryProvider { get; set; }

        //[DataMember]
        //[XmlAttribute(AttributeName = "RefProvider")]
        //public string ReferringProvider { get; set; }

        //[DataMember]
        //[XmlAttribute(AttributeName = "NextVisit")]
        //public string NextVisit { get; set; }

        //[DataMember]
        //[XmlAttribute(AttributeName = "LastVisit")]
        //public string LastVisit { get; set; }

    }
}
