using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using LiveProvider.Model.TypeDTOs.Appointment;
using System.Xml.Serialization;
using LiveProvider.Model.Appointment.ClinicRoom;

namespace LiveProvider.Model.Appointment
{
    [DataContract]
    [XmlRoot(ElementName = "Appointment")]
    [XmlType(TypeName="Appointment")]
    public class AppointmentDTO
    {

        [DataMember]
        [XmlAttribute(AttributeName = "ID")]
        public int AppointmentId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "StationID")]
        public int StationId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "StationName")]
        public string StationName { get; set; }

        //[DataMember]
        //public string ClinicBranchAddress { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "RequestID")]
        public int AppointmentRequestId { get; set; }

        //[DataMember(EmitDefaultValue = false)]
        [XmlAttribute(AttributeName = "UserID")]
        public string ProviderUserId { get; set; }

        //[DataMember]
        //public int ProviderTypeId { get; set; }

        //[DataMember]
        //public int ProviderSpecialityId { get; set; }

        //[DataMember]
        //public string ProviderSpecialityName { get; set; }

        //[DataMember]
        //public string ProviderTypeName { get; set; }

        //[DataMember]
        //public string ProviderName { get; set; }

        //[DataMember]
        //public string PatientQuivusID { get; set; }

        //[DataMember]
        //public string DoctorSSN { get; set; }

        //[DataMember]
        //[XmlAttribute(AttributeName = "PatientID")]
        //public int PatientId { get; set; }

        //[DataMember]
        //public string PatientName { get; set; }

        //[DataMember]
        //public string PatientFirstName { get; set; }

        //[DataMember]
        //public string PatientLastName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Date")]
        //public DateTime AppointmentDate { get; set; } by Sami
        public string AppointmentDate { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "StartTime")]
        //public DateTime AppointmentStartTime { get; set; } by sami
        public string AppointmentStartTime { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "EndTime")]
        //public DateTime AppointmentEndTime { get; set; }
        public string AppointmentEndTime { get; set; }

        [DataMember]
        [XmlElement(ElementName = "Type")]
        public AppointmentTypeDTO AppointmentType { get; set; }

        [DataMember]
        [XmlElement(ElementName = "AppointmentStatus")]
        public AppointmentRequestStatusTypeDTO AppointmentStatus { get; set; }

        [DataMember]
        [XmlElement(ElementName = "Action")]
        public AppointmentCurrentStatusTypeDTO AppointmentCurrentStatus { get; set; }

        //[DataMember]
        //public string Location { get; set; }

        //[DataMember]
        //public bool IsAllDay { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ClinicID")]
        public int ClinicRoomId { get; set; }

        //[DataMember]
        //public string ClinicRoomTitle { get; set; }

        //[DataMember]
        //public bool IsRescheduled { get; set; }       

        //[DataMember]
        //public string PatientLastVisitDate { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ChiefComplaint")]
        public string ChiefComplaint { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "CreatedBy")]
        public int CreatedBy { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ModifiedBy")]
        public int ModifiedBy { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ModifiedAt")]
        public DateTime ModifiedAt { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "IsDeleted")]
        public bool IsDeleted { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Alert")]
        //[XmlElement(ElementName = "Alert")]
        public string InformationAlert { get; set; }

        [DataMember]
        [XmlElement(ElementName = "Patient")]
        public PatientInfoDTO Patient { get; set; }

        [DataMember]
        [XmlElement(ElementName = "Room")]
        public RoomDTO Room { get; set; }

        //[DataMember]
        [XmlAttribute(AttributeName = "FirstName")]
        public string ProviderFirstName { get; set; }

        //[DataMember]
        [XmlAttribute(AttributeName = "LastName")]
        public string ProviderLastName { get; set; }

        //[DataMember]
        [XmlAttribute(AttributeName = "MiddleName")]
        public string ProviderMiddleName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "QuivusID")]
        public string QuivusId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "LastVisit")]
        public string LastVisit { get; set; }
    }


    //[System.Xml.Serialization.xmlroot="Appointments"]
    //public class Appointments
    //{
    //    [XmlArray("Appointments")]
    //    public AppointmentDTO appointments[];
    //}
}
