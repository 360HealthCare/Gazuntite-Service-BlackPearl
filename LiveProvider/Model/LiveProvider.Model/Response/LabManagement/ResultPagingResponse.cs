using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using LiveProvider.Model.ELab;

namespace LiveProvider.Model.Response.LabManagement
{
    [DataContract]
    [XmlRoot(ElementName = "Results")]
    public class ResultPagingResponse
    {
        [XmlArrayAttribute("Labs")]
        public List<OrderManagementDTO> LabList { get; set; }

        [XmlElement(ElementName = "Status")]
        public ServiceResponse Response { get; set; }

        [XmlAttribute(AttributeName = "FirstRecord")]
        public int FirstRecord { get; set; }

        [XmlAttribute(AttributeName = "LastRecord")]
        public int LastRecord { get; set; }

        [XmlAttribute(AttributeName = "TotalRecords")]
        public int TotalRecords { get; set; }
    }
}
