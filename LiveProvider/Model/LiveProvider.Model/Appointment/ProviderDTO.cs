using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.Appointment
{
    [DataContract]
    [XmlType(TypeName = "Provider")]
    public class ProviderDTO
    {
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
        [XmlAttribute(AttributeName = "UserID")]
        public int UserId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "StationID")]
        public int StationId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ID")]
        public int ProviderId { get; set; }

        //[DataMember]
        //[XmlArrayAttribute("Days")]
        //public List<ScheduleDayDTO> DayList { get; set; }

        [DataMember]
        [XmlArrayAttribute("Appointments")]
        public List<AppointmentDTO> AppointmentList { get; set; }

        public ProviderDTO()
        {
            //if (DayList == null)
            //{
            //    DayList = new List<ScheduleDayDTO>();
            //}
            if (AppointmentList == null)
            {
                AppointmentList = new List<AppointmentDTO>();
            }
        }

    }
}
