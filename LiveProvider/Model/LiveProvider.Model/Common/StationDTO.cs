using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.Common
{
    [DataContract]
    [XmlType(TypeName = "Station")]
    public class StationDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ID")]
        public int StationID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Name")]
        public string StationName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ClinicBranchID")]
        public int ClinicBranchID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "CreatedBy")]
        public int CreatedBy { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ModifiedAt")]
        public DateTime ModifiedAt { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ModifiedBy")]
        public int ModifiedBy { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "IsDefault")]
        public bool IsDefault { get; set; }

    }
}
