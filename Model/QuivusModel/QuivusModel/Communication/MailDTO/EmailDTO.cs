using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuivusModel.Communication.MailDTO
{
    /// <summary>
    /// Author: Zahid Mehmood
    /// Date: 30 Novmeber 2001
    /// Description: DTO of email
    /// </summary>
    [DataContract(Namespace = "EmailCommunication/")]
    //[DataContract]
    [XmlRoot(ElementName="Email"), XmlType(TypeName="Email")]
    public class EmailDTO 
    {
        //[DataMember(Order=0)]
        ////[XmlElement("Template")]
        //[XmlAttribute(AttributeName="TemplateID")]
        //public int EmailTemplateID;

        //[DataMember]
        //[XmlAttribute(AttributeName = "UserID")]
        //public int UserId;

        [DataMember]
        [XmlAttribute(AttributeName="TemplateID")]
        public int EmailTemplateID { get; set; }

        // remove userid later on
        
        [DataMember]
        [XmlAttribute(AttributeName = "EmailID")]
        public int UserEmailID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "RecieverID")]
        public int RECIEVERID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ClinicBranchID")]
        public int ClinicBranchID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Subject")]
        public string Subject { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Message")]
        public string Message { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Attachment")]
        public byte[] Attachment { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "AttachedFileName")]
        public string AttachedFileName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "HasAttachment")]
        public bool HasAttachment { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "RecipientEmail")]
        public string RecipientEmail { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "SenderID")]
        public int SENDERID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "EmailStateID")]
        public int EmailStateID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "EmailTypeID")]
        public int EmailTypeID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "EmailTokenID")]
        public int Token_ID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "MailSentAt")]
        public string MailSentAt { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "From")]
        public string From { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Sendto")]
        public string Sendto { get; set; }


        [DataMember]
        [XmlAttribute(AttributeName = "IsSelected")]
        public Boolean IsSelected { get; set; }


        [DataMember]
        [XmlAttribute(AttributeName = "TotalRecords")]
        public int TotalRecords { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "FirstRecord")]
        public int FirstRecord { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "UserId")]
        public int UserId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "LastRecord")]
        public int LastRecord { get; set; }




        [DataMember]
        //[XmlElement(ElementName="Attachments")]
        [XmlArray("Attachments")]
        [XmlArrayItem("Attachment")]
        public List<AttachementDTO> Attachemnts{get; set;}
    }
}
