using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using LiveProvider.Model.TypeDTOs.ELabs.LiveProvider;
using LiveProvider.Model.TypeDTOs.ELabs.QuickInput;

namespace LiveProvider.Model.Response.LabManagement
{
    [DataContract]
    [XmlRoot(ElementName = "StaticData")]
    public class StaticDataResponse
    {
        [XmlElement(ElementName = "Status")]
        public ServiceResponse Response { get; set; }

        //[XmlArrayAttribute("Fields")]
        //public List<SearchFieldsDTO> Feilds { get; set; }
        [XmlElement(ElementName = "Fields")]
        public List<SearchFieldsResponse> Fields { get; set; }


        [XmlArrayAttribute("Labs")]
        public List<LabTypeDTO> Labs { get; set; }

        [XmlArrayAttribute("Tests")]
        public List<LIONICLabTestTypeDTO> Tests { get; set; }
    }
}
