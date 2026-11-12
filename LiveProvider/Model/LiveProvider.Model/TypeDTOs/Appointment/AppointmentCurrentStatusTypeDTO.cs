using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.TypeDTOs.Appointment
{
    [DataContract]
    [XmlRoot(ElementName = "Action")]
    [XmlType(TypeName="Action")]
    public class AppointmentCurrentStatusTypeDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ID")]
        public int AppointmentCurrentStatusId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Name")]
        public string AppointmentCurrentStatusName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "NextAction")]
        public string NextAction { get; set; }

    }
}
