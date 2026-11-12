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
    /// Date: 17 November 2011
    /// Description: SMS data saving and retrieval
    /// </summary>
    [DataContract]
    public class SMSLogDTO
    {
        [DataMember]
        [XmlAttribute("ClinicBranchID")]
        public int ClinicBranchID { get; set; }

        [DataMember]
        [XmlAttribute("UserId")]
        public int UserId { get; set; }

        [DataMember]
        [XmlAttribute("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [DataMember]
        [XmlAttribute("Message")]
        public string Text { get; set; }

        [DataMember]
        [XmlAttribute("SendAt")]
        public string SendAt { get; set; }

        [DataMember]
        [XmlAttribute("SMSTemplateID")]
        public int SMSTemplateID { get; set; }

        [DataMember]
        [XmlAttribute("CreatedBy")]
        public int CreatedBy { get; set; }

        [DataMember]
        [XmlAttribute("IsDeleted")]
        public bool IsDeleted { get; set; }

        [DataMember]
        [XmlArray("RecieverList")]
        [XmlArrayItem("Reciever")]
        public List<SMSRecieverDTO> smsRecieverList { get; set; }

        //[DataMember]
        //public int ModifiedBy { get; set; }

        //[DataMember]
        //public int ModifiedAt { get; set; }
    }
}
