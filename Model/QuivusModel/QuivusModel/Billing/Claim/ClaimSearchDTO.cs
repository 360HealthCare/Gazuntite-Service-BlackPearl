using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuivusModel.Billing.Claim
{
    [DataContract]
    //[XmlRoot(ElementName = "Claim")]
    [XmlRoot(ElementName = "Claim"), XmlType(TypeName = "Claim")]
    public class ClaimSearchDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ClaimID")]
        public int ClaimID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "PatientID")]
        public int PatientID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "VisitID")]
        public int VisitID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "PatientName")]
        public string PatientName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "QuivusID")]
        public string QuivusID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Carrier1")]
        public string Carrier1 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Status1")]
        public string Status1 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Media1")]
        public string Media1 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Batch1")]
        public int Batch1 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "BillDate1")]
        public string BillDate1 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "EDIReceiver1")]
        public string EDIReceiver1 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Carrier2")]
        public string Carrier2 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Status2")]
        public string Status2 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Media2")]
        public string Media2 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Batch2")]
        public int Batch2 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "BillDate2")]
        public string BillDate2 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "EDIReceiver2")]
        public string EDIReceiver2 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Carrier3")]
        public string Carrier3 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Status3")]
        public string Status3 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Media3")]
        public string Media3 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Batch3")]
        public int Batch3 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "BillDate3")]
        public string BillDate3 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "EDIReceiver3")]
        public string EDIReceiver3 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ProviderId")]
        public int ProviderId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ProviderName")]
        public string ProviderName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "DateCreated")]
        public string DateCreated { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "PatientUserId")]
        public int PatientUserId { get; set; }

        //[DataMember]       
        //[XmlArrayAttribute("ClaimTransactions")]
        //public List<ClaimTransactionDTO> ClaimTransactionList { get; set; }
    }
}
