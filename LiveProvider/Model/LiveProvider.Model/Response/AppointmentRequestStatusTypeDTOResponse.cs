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
    [XmlRoot(ElementName = "AppointmentStatus")]
    public class AppointmentRequestStatusTypeDTOResponse
    {
        [DataMember]
        [XmlArrayAttribute("Statuses")]
        public List<AppointmentRequestStatusTypeDTO> RequestStatusTypeList { get; set; }

        public AppointmentRequestStatusTypeDTOResponse()
        {
            if (RequestStatusTypeList == null)
            {
                RequestStatusTypeList = new List<AppointmentRequestStatusTypeDTO>();
            }
        }

    }
}
