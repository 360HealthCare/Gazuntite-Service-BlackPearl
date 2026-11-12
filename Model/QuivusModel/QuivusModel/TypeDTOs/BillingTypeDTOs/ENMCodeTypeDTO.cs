using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    [XmlRoot(ElementName = "ENMCodeType"), XmlType(TypeName = "ENMCodeType")]
    public class ENMCodeTypeDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ENMCodeTypeID")]
        public int ENMCodeTypeID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ENMProcCode")]
        public String ENMProcCode { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ENMProcDescription")]
        public String ENMProcDescription { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Price")]
        public Decimal Price { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "EncounterType")]
        public int EncounterType { get; set; }  
    }
}
