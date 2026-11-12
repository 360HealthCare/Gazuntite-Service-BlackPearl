using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    [XmlRoot(ElementName = "FrequencyCodeType"), XmlType(TypeName = "FrequencyCodeType")]
    public class ClaimFrequencyCodeTypeDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "FrequencyTypeID")]
        public int FrequencyTypeID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "FrequencyCode")]
        public string FrequencyCode { get; set; }

    }
}
