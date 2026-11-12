using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace QuivusModel.Communication.SMSDTO
{
    [DataContract]
    [XmlRoot(ElementName = "SMSLog"), XmlType(TypeName = "SMSLog")]
   public class SMSResponseDTO
    {


        [DataMember]
        [XmlAttribute("TotalPages")]
        public int TotalPages { get; set; }

        [DataMember]
        [XmlAttribute("CurrentPage")]
        public int CurrentPage { get; set; }

        
        /// /////////////////////////////////

        [DataMember]
        [XmlArray("SMSDetails")]
        [XmlArrayItem("SMS")]
        public List<SMSResponseDetailDTO> smsResponseDetail { get; set; }
       

    }
}
