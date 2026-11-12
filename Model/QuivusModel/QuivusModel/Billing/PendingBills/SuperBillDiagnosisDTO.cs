using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuivusModel.Billing.PendingBills
{
    [DataContract]
    [XmlRoot(ElementName = "Diagnosis"), XmlType(TypeName = "Diagnosis")]
    public class SuperBillDiagnosisDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "DiagnosisID")]
        public int PatientDiagnosisID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "PatientID")]
        public int PatientID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "VisitID")]
        public int VisitID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "VisitDate")]
        public DateTime VisitDate { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "CreatedBy")]
        public int CreatedBy { get; set; }

        //[DataMember]
        //[XmlAttribute(AttributeName = "CreatedAt")]
        //public DateTime CreatedAt { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ModifiedBy")]
        public int ModifiedBy { get; set; }

        //[DataMember]
        //[XmlAttribute(AttributeName = "ModifiedAt")]
        //public DateTime ModifiedAt { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ICDCodeID")]
        public int ICDCodeID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ICDCodeName")]
        public string ICDCodeName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ICDCodeDescription")]
        public string ICDCodeDescription { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "IncludeInBilling")]
        public Boolean IncludeInBilling { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "IsCreatedFromSuperBill")]
        public Boolean IsCreatedFromSuperBill { get; set; }
        
    }
}
