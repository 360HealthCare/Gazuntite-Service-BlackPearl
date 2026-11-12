using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using LiveProvider.Model.Common;

namespace LiveProvider.Model.Response
{  
    [DataContract]
    [XmlRoot(ElementName = "StationDTO")]
    public class StationDTOResponse
    {
         [DataMember]
        [XmlArrayAttribute("Stations")]
        public List<StationDTO> StationsList { get; set; }

        public StationDTOResponse()
        {
            if (StationsList == null)
            {
                StationsList = new List<StationDTO>();
            }
        }
    }
}
