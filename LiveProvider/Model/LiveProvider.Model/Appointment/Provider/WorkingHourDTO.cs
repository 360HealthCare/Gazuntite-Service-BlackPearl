



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.Appointment.Provider
{
    [DataContract]
    [XmlType(TypeName = "WorkingHour")]
    public class WorkingHourDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "StartTime")]
        public string StartTime { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "EndTime")]
        public string EndTime { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "WeekDay")]
        public string WeekDay { get; set; }

    }
}
