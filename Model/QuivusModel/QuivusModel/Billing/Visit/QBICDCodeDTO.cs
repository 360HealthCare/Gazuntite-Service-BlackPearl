using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuivusModel.Billing.Visit
{
    [DataContract]
    [XmlRoot(ElementName = "DiseaseType"), XmlType(TypeName = "DiseaseType")]
    public class QBICDCodeDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ICDCodeID")]
        public int ICDCodeID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ICDCodeName")]
        public string ICDCodeName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ICDCodeDescription")]
        public string ICDCodeDescription { get; set; }
    }
}
