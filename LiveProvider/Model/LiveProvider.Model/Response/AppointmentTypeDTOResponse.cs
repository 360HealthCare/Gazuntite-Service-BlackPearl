using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using LiveProvider.Model.TypeDTOs.Appointment;

namespace LiveProvider.Model.Response
{
    [DataContract]
    [XmlRoot(ElementName = "AppointmentType")]
    public class AppointmentTypeDTOResponse
    {
        [DataMember]
        [XmlArrayAttribute("Types")]
        public List<AppointmentTypeDTO> AppointmentTypeList { get; set; }

        public AppointmentTypeDTOResponse()
        {
            if (AppointmentTypeList == null)
            {
                AppointmentTypeList = new List<AppointmentTypeDTO>();
            }
        }

    }
}