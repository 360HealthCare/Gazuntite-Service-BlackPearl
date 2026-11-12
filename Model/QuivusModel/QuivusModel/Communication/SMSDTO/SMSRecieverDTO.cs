using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace QuivusModel.Communication.SMSDTO
{
    /// <summary>
    /// Author: Zahid Mehmood
    /// Date: 2 January 2012
    /// Description: This DTO represents list of SMS reciever
    /// </summary>
    [DataContract]
   public class SMSRecieverDTO
    {
        [DataMember]
        [XmlAttribute("UserId")]
        public int UserId { get; set; }

        [DataMember]
        [XmlAttribute("To")]
        public string To { get; set; }

        [DataMember]
        [XmlAttribute("PhoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
