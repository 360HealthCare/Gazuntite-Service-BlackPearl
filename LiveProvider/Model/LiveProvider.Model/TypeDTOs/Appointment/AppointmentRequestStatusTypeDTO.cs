using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.TypeDTOs.Appointment
{
    [DataContract]
    [XmlRoot(ElementName="Status"),XmlType(TypeName = "Status")]
    public class AppointmentRequestStatusTypeDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ID")]
        //[XmlElement(ElementName="ID")]
        public int AppointmentRequestStatusId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Name")]
        //[XmlElement(ElementName = "Name")]
        public string AppointmentRequestStatusName { get; set; }
    }
}
