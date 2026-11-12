using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model;
using System.Runtime.Serialization;

namespace QuivusModel.Common
{
    [DataContract]
    public class StationDTO : QuivusBaseDTO
    {
        [DataMember]
        public int StationID { get; set; }

        [DataMember]
        public string StationName { get; set; }

        [DataMember]
        public int ClinicBranchID { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime ModifiedAt { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public bool IsDefault { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string FaxNumber { get; set; }

    }
}
