using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.TypeDTOs.Appointment
{
    [DataContract]
    [XmlRoot(ElementName = "Type"),XmlType(TypeName="Type")]
    public class AppointmentTypeDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ID")]
        public int AppointmentTypeId { get; set; }
        
        [DataMember]
        [XmlAttribute(AttributeName = "Name")]
        public string AppointmentTypeName { get; set; }
    }
}
