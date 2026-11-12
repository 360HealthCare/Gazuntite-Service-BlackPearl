using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.Response.LabManagement
{
    [DataContract]
    [XmlType(TypeName = "Status")]
    public class ServiceResponse
    {
        [DataMember]
        [XmlAttribute(AttributeName = "Type")]
        public string ResponseType { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Message")]
        public string ResponseMessage { get; set; }
    }
}
