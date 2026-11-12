using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    [XmlRoot(ElementName = "POSType"), XmlType(TypeName = "POSType")]
    public class PlaceOfServiceTypeDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ServiceTypeID")]
        public int ServiceTypeID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ServiceTypeCode")]
        public int ServiceTypeCode { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "POSDescription")]
        public string POSDescription { get; set; }
    }
}
