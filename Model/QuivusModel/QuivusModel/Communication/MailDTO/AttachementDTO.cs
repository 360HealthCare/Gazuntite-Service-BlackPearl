using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace QuivusModel.Communication.MailDTO
{
    /// <summary>
    /// Author: Zahid Mehmood
    /// Date: 7 December 2011
    /// Description: Used to get the Attachments List
    /// </summary>
    [DataContract]
   public class AttachementDTO
    {
        [DataMember]
        public byte[] Attachment;
        [DataMember]
        public string AttachedFileName;

        //public string AttachedFileName()
        //{
        //    return AttachedFileName;
        //}

        //[DataMember]
        //public byte[] Attachment { get; set; }

        //[DataMember]
        //public string AttachedFileName { get; set; }
    }
}
