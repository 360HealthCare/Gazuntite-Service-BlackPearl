using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace QuivusModel.Communication.MailDTO
{
    [DataContract]
    public class MailDTO
    {
        [DataMember]
        [XmlElement("Email")]
        public List<EmailDTO> Emails {get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "TotalRecords")]
        public int TotalRecords { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "FirstRecord")]
        public int FirstRecord { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "LastRecord")]
        public int LastRecord { get; set; }
    }
}
