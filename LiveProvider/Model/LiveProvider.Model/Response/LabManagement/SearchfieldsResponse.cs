using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using LiveProvider.Model.TypeDTOs.ELabs.QuickInput;
using LiveProvider.Model.TypeDTOs.ELabs.LiveProvider;

namespace LiveProvider.Model.Response.LabManagement
{
    [DataContract]
    [XmlType(TypeName = "Fields")]
    public class SearchFieldsResponse
    {
        [DataMember]
        [XmlAttribute("Type")]
        public string Type { get; set; }

        [DataMember]
        [XmlArrayAttribute("List")]
        public List<SearchFieldsDTO> Feilds { get; set; }
    }
}
