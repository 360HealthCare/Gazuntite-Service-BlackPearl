using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.Appointment
{
    [DataContract]
    [XmlType(TypeName = "Location")]
    public class LocationDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ID")]
        public int StationID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Name")]
        public string StationName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ClinicID")]
        public int ClinicBranchID { get; set; }

        [DataMember]
        [XmlArrayAttribute("Providers")]
        public List<ProviderDTO> ProviderList { get; set; }

        public LocationDTO()
        {
            if (ProviderList == null)
            {
                ProviderList = new List<ProviderDTO>();
            }
        }
    }
}
