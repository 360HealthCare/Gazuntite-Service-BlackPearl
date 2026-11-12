using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using LiveProvider.Model.TypeDTOs.Appointment;
using System.Xml.Serialization;

namespace LiveProvider.Model.Appointment
{
    [XmlRoot(ElementName = "Request")]
    [XmlType(TypeName = "Request")]
    [DataContract]
    public class AppointmentRequestDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ID")]
        public int RequestID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ProviderID")]
        public int ProviderID { get; set; }

        [DataMember]//M
        [XmlAttribute(AttributeName = "ProviderUserID")]
        public int ProviderUserID { get; set; }

        [DataMember]//M
        [XmlAttribute(AttributeName = "ProviderTypeID")]
        public int ProviderTypeID { get; set; }

        [DataMember]//M
        [XmlAttribute(AttributeName = "ProviderTypeName")]
        public string ProviderTypeName { get; set; }

        [DataMember]//M
        [XmlAttribute(AttributeName = "ProviderName")]
        public string ProviderName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "PatientID")]
        public int PatientID { get; set; }

        //[DataMember]
        //public string PatientName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Station")]
        public int StationID { get; set; }

        [DataMember]//M
        [XmlAttribute(AttributeName = "LocationName")]
        public string LocationName { get; set; }

        [DataMember]//M
        [XmlAttribute(AttributeName = "LocationAddress")]
        public string LocationAddress { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Date")]
        public DateTime AppRequestDate { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "StartTime")]
        public DateTime AppointmentStartTime { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "EndTime")]
        public DateTime AppointmentEndTime { get; set; }

        [DataMember]
        [XmlElement(ElementName = "Type")]
        public AppointmentRequestStatusTypeDTO AppRequestStatus { get; set; }

        //[DataMember]
        //public string GeneratedON { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Email")]
        public bool IsEmailSent { get; set; }

        //[DataMember]
        //public bool IsRejected { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "CreatedBy")]
        public int CreatedBy { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ModifiedBy")]
        public int ModifiedBy { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ModifiedAt")]
        public DateTime ModifiedAt { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "IsDeleted")]
        public bool IsDeleted { get; set; }

        [DataMember] //M
        [XmlAttribute(AttributeName = "SpecialityId")]
        public int ProviderSpecialityId { get; set; }

        [DataMember] //M
        [XmlAttribute(AttributeName = "SpecialityName")]
        public string ProviderSpecialityName { get; set; }

        [DataMember]
        [XmlElement(ElementName = "Patient")]
        public PatientInfoDTO Patient { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "AppointmentTypeID")]
        public int AppointmentTypeId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "QuivusID")]
        public string QuivusId { get; set; }
    }
}
