using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.Appointment
{
    [DataContract]
    [XmlType(TypeName = "Day")]
    public class ScheduleDayDTO
    {
        [DataMember]
        [XmlAttribute("DayName")]
        public string DayName { get; set; }

        [DataMember]
        [XmlAttribute("Date")]
        public string Date { get; set; }

        [DataMember]
        [XmlAttribute("Total")]
        public int Total { get; set; }

        //[DataMember]
        //[XmlArrayAttribute("Appointments")]
        //public List<AppointmentDTO> AppointmentList { get; set; }

        public ScheduleDayDTO()
        {
            //if (AppointmentList == null)
            //{
            //    AppointmentList = new List<AppointmentDTO>();
            //}
        }
    }
}
