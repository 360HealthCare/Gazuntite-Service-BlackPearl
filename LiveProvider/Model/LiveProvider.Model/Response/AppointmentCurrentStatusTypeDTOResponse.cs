using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using LiveProvider.Model.TypeDTOs.Appointment;

namespace LiveProvider.Model.Response
{
    [DataContract]
    [XmlRoot(ElementName = "AppointmentActions")]
    public class AppointmentCurrentStatusTypeDTOResponse
    {
        [DataMember]
        [XmlArrayAttribute("Actions")]
        public List<AppointmentCurrentStatusTypeDTO> AppointmentCurrentSatatusList { get; set; }

        public AppointmentCurrentStatusTypeDTOResponse()
        {
            if (AppointmentCurrentSatatusList == null)
            {
                AppointmentCurrentSatatusList = new List<AppointmentCurrentStatusTypeDTO>();
            }
        }
    }
}
