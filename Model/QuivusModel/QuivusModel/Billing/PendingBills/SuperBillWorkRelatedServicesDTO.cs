using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuivusModel.Billing.PendingBills
{
    [DataContract]
    [XmlRoot(ElementName = "Service"), XmlType(TypeName = "Service")]
    public class SuperBillWorkRelatedServicesDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "WRSTypeId")]
        public Int32 WorkServiceTypeId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ServiceDescription")]
        public String ServiceDescription { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Fee")]
        public Decimal Fee { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "IsSelected")]
        public Boolean IsSelected { get; set; }
    }
}
