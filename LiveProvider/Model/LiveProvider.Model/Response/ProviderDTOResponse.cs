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
    [XmlRoot(ElementName = "ProviderDTO")]
    public class ProviderDTOResponse
    {
        [XmlArrayAttribute("Providers")]
        public List<ProviderDTO> ProviderList { get; set; }

        public ProviderDTOResponse()
        {
            if (ProviderList == null)
            {
                ProviderList = new List<ProviderDTO>();
            }
        }
    }
}
