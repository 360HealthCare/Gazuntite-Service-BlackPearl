using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuivusModel.Billing.Claim
{
    [DataContract]
    [XmlRoot(ElementName = "Transaction"), XmlType(TypeName = "Transaction")]
    public class ClaimTransactionDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ID")]
        public int ID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ClaimID")]
        public int ClaimID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ProcedureID")]
        public int ProcedureID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ProcedureCode")]
        public string ProcedureCode { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "DateFrom")]
        public DateTime DateFrom { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "DateTo")]
        public DateTime DateTo { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Amount")]
        public decimal Amount { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Document")]
        public string Document { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Ins1Resp")]
        public bool Ins1Resp { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Ins2Resp")]
        public bool Ins2Resp { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Ins3Resp")]
        public bool Ins3Resp { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "CreatedBy")]
        public int CreatedBy { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ModifiedBy")]
        public int ModifiedBy { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ModifiedAt")]
        public DateTime ModifiedAt { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "IsDeleted")]
        public Boolean IsDeleted { get; set; }
              
    }
}
