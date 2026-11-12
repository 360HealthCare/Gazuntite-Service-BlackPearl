using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using LiveProvider.Model.TypeDTOs.ELabs.QuickInput;

namespace LiveProvider.Model.Response.LabManagement
{
    [DataContract]
    [XmlType(TypeName = "TestType")]
    public class TestTypeResponse
    {
        [XmlElement(ElementName = "Status")]
        public ServiceResponse Response { get; set; }

        [XmlElement(ElementName = "Tests")]
        public List<LIONICLabTestTypeDTO> TestList { get; set; }

    }
}
