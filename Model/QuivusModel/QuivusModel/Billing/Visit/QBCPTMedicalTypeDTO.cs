using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuivusModel.Billing.Visit
{
    [DataContract]
    [XmlRoot(ElementName = "CPTMedicalType"), XmlType(TypeName = "CPTMedicalType")]
    public class QBCPTMedicalTypeDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "CPTMedicalTypeID")]
        public int CPTMedicalTypeID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ProcedureCode")]
        public string ProcedureCode { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ProcedureDescription")]
        public string ProcedureDescription { get; set; }
    }
}
