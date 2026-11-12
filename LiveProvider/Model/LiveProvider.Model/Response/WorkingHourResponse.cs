using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using LiveProvider.Model.Appointment.Provider;

namespace LiveProvider.Model.Response
{
    [DataContract]
    [XmlRoot(ElementName = "ProviderWorkingHours")]
    public class WorkingHourResponse
    {
        [DataMember]
        [XmlArrayAttribute("WorkingHours")]
        public List<WorkingHourDTO> WorkingHoursList { get; set; }

        public WorkingHourResponse()
        {
            if (WorkingHoursList == null)
            {
                WorkingHoursList = new List<WorkingHourDTO>();
            }
        }    
    }
}
