using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace QuivusModel.Communication.SMSDTO
{
    [DataContract]
   public class SMSResponseDetailDTO
    {
        [DataMember]
        [XmlAttribute("SMSID")]
        public int SMSID { get; set; }

        [DataMember]
        [XmlAttribute("Message")]
        public string Message { get; set; }

        [DataMember]
        [XmlAttribute("SendAt")]
        public DateTime SendAt { get; set; }

        [DataMember]
        [XmlArray("RecieverList")]
        [XmlArrayItem("Reciever")]
        public List<SMSRecieverDTO> smsRecieverList { get; set; }
    }
}
