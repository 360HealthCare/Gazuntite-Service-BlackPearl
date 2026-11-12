using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuivusModel.Communication.VideoChatDTO
{
    [DataContract]
    [XmlRoot(ElementName = "VideoChat"), XmlType(TypeName = "VideoChat")]
   public class VideoChatDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "VideoChatID")]
        public int VideoChatID {get; set;}

        [DataMember]
        [XmlAttribute(AttributeName = "OpenTokSessionID")]
        public String OpenTokSessionID {get; set;}

        [DataMember]
        [XmlAttribute(AttributeName = "OpenTokTokenID")]
        public String OpenTokTokenID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ClinicBranchID")]
        public String ClinicBranchID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "CallInitiator")]
        public String CallInitiator {get; set;}

        [DataMember]
        [XmlAttribute(AttributeName = "CallReciever")]
        public String CallReciever {get; set;}

        [DataMember]
        [XmlAttribute(AttributeName = "CallStartTime")]
        public DateTime CallStartTime {get; set;}

        [DataMember]
        [XmlAttribute(AttributeName = "CallEndTime")]
        public DateTime CallEndTime {get; set;}

        //[DataMember]
        //public String CallDuration {get; set;}

        [DataMember]
        [XmlAttribute(AttributeName = "CreatedDate")]
        public DateTime CreatedDate {get; set;}

        [DataMember]
        [XmlAttribute(AttributeName = "ModifiedBy")]
        public int ModifiedBy {get; set;}

        [DataMember]
        [XmlAttribute(AttributeName = "ModifiedDate")]
        public DateTime ModifiedDate { get; set; }


    }
}
