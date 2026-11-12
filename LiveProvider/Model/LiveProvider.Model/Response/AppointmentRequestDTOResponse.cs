using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Model.Appointment;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.Response
{
    [XmlRoot(ElementName = "AppointmentRequest")]
    [DataContract]
    public class AppointmentRequestDTOResponse
    {
        [DataMember]
        [XmlArrayAttribute("Requests")]
        public List<LocationDTO> LocationList { get; set; }
        public List<AppointmentRequestDTO> AppointmentRequestList { get; set; }
    }
}
