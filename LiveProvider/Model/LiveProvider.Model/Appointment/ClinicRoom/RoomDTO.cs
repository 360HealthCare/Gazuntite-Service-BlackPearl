using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.Appointment.ClinicRoom
{

    [DataContract]
    [XmlRoot(ElementName = "Room")]
    [XmlType(TypeName = "Room")]
    public class RoomDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ID")]
        public int ClinicRoomId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Station")]
        public int StationId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Title")]
        public string RoomTitle { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Type")]
        public string Roomtype { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Status")]
        public string RoomStatus { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Code")]
        public string RoomCode { get; set; }

    }
}
