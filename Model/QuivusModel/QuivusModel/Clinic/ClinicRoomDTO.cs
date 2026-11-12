using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.Clinic
{
    [DataContract]
    public class ClinicRoomDTO
    {
        [DataMember]
        public int ClinicRoomId { get; set; }

        [DataMember]
        public int StationId { get; set; }
        
        [DataMember]
        public string RoomTitle { get; set; }

        [DataMember]
        public string Roomtype { get; set; }

        [DataMember]
        public string RoomStatus { get; set; }

        [DataMember]
        public string RoomCode { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public DateTime ModifiedAt { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }
    }
}
