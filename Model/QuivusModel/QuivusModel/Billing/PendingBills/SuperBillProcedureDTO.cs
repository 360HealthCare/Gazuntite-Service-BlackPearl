using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs;
using System.Xml.Serialization;

namespace QuivusModel.Billing.PendingBills
{
    [DataContract]
    [XmlRoot(ElementName = "Procedure"), XmlType(TypeName = "Procedure")]
    public class SuperBillProcedureDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ProcedureID")]
        public int PatientProcedureID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "VisitID")]
        public int VisitID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "PatientID")]
        public int PatientID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "CPTMedicalTypeID")]
        public int CPTMedicalTypeID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ProcedureCode")]
        public string ProcedureCode { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ProcedureDescription")]
        public string ProcedureDescription { get; set; }
    
        [DataMember]
        [XmlAttribute(AttributeName = "CreatedBy")]
        public int CreatedBy { get; set; }  
      
        [DataMember]
        [XmlAttribute(AttributeName = "ModifiedBy")]
        public int ModifiedBy { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "VisitDate")]
        public String VisitDate { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "IncludeInBilling")]
        public Boolean IncludeInBilling { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "IsCreatedFromSuperBill")]
        public Boolean IsCreatedFromSuperBill { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Modifier1")]
        public String Modifier1 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Modifier2")]
        public String Modifier2 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Modifier3")]
        public String Modifier3 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Modifier4")]
        public String Modifier4 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ModifierTypeId1")]
        public int ModifierTypeId1 { get; set; }
        
        [DataMember]
        [XmlAttribute(AttributeName = "ModifierTypeId2")]
        public int ModifierTypeId2 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ModifierTypeId3")]
        public int ModifierTypeId3 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ModifierTypeId4")]
        public int ModifierTypeId4 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Fee")]
        public Decimal Fee { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Unit")]
        public int Unit { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "IsDeleted")]
        public Boolean IsDeleted { get; set; }
    }
}
