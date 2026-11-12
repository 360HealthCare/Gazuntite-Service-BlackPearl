using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusModel.TypeDTOs.BillingTypeDTOs;
using System.Xml.Serialization;

namespace QuivusModel.Billing.PendingBills
{
    [DataContract]
    [XmlRoot(ElementName = "ENMService"), XmlType(TypeName = "ENMService")]
    public class SuperBillENMServiceDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ENMServiceID")]
        public int ENMServiceID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ClaimID")]
        public int ClaimID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ENMCodeTypeID")]
        public int ENMCodeTypeID { get; set; }        

        [DataMember]
        [XmlAttribute(AttributeName = "Fee")]
        public Decimal Fee { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "EncounterType")]
        public int EncounterType { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "CodeType")]
        public int CodeType { get; set; } 
        
    }
}
