using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuivusModel.Billing.Visit
{
    [DataContract]
    [XmlRoot(ElementName = "TOSType"), XmlType(TypeName = "TOSType")]
    public class QBEncounterTypeDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "EncounterTypeID")]
        public int EncounterTypeID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "EncounterTypeName")]
        public string EncounterTypeName { get; set; }
    }
}
