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
    [XmlRoot(ElementName = "Schedule")]
    public class AppointmentDTOResponse
    {
        [DataMember]
        [XmlArrayAttribute("Locations")]
        public List<LocationDTO> LocationList { get; set; }

        public AppointmentDTOResponse()
        {
            if (LocationList == null)
            {
                LocationList = new List<LocationDTO>();
            }
        }
    }
}
