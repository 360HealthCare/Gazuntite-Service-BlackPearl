using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using LiveProvider.Model.Appointment;

namespace LiveProvider.Model.Response
{
    [DataContract]
    [XmlRoot(ElementName = "WaitingRoom")]
    public class WaitingRoomResponse
    {
        [DataMember]
        [XmlArrayAttribute("Appointments")]
        public List<AppointmentDTO> AppointmentsList { get; set; }

        public WaitingRoomResponse()
        {
            if (AppointmentsList == null)
            {
                AppointmentsList = new List<AppointmentDTO>();
            }
        }
    }
}
