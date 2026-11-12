using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Model.Appointment;
using System.Xml.Serialization;

namespace LiveProvider.Model.Request.Scheduler
{
    [System.Xml.Serialization.XmlRoot("Appointments",Namespace="")]
    public class AppointmentRequestDTO
    {
        [XmlArray("Appointments")]
        public List<AppointmentDTO> Appointments { get; set; }
    }
}
