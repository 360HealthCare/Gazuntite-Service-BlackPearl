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
    [XmlRoot(ElementName = "AppointmentResponse")]
    public class AppointmentSearchResponse
    {
        [DataMember]
        [XmlArrayAttribute("Appointments")]
        public List<AppointmentDTO> AppointmentList { get; set; }

        public AppointmentSearchResponse()
        {
            if (AppointmentList == null)
            {
                AppointmentList = new List<AppointmentDTO>();
            }
        }
    }
}
