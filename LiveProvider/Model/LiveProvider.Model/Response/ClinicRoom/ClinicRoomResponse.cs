using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Model.Appointment.ClinicRoom;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.Response.ClinicRoom
{
    [DataContract]
    [XmlRoot(ElementName = "ClinicRooms")]
    public class ClinicRoomResponse
    {
        [DataMember]
        [XmlArrayAttribute("Rooms")]
        public List<RoomDTO> ClinicRoomList { get; set; }
    }
}
