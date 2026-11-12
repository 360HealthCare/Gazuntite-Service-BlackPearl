using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using QuivusModel.User.Patient;

namespace QuivusModel.Billing.PendingBills
{
    /// <summary>
    /// Author: Shahid Munir
    /// Date:   01/25/2012
    /// DTO for signed visits for which claim not created for billing 
    /// </summary>
    [DataContract]
    [XmlRoot(ElementName = "PendingBill"), XmlType(TypeName = "PendingBill")]
    public class PendingBillsDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "VisitId")]
        public Int32 VisitId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "PatientId")]
        public int PatientId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "VisitDate")]
        public DateTime VisitDate { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ProviderId")]
        public int ProviderId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "QuivusId")]
        public String QuivusId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Patient")]
        public String Patient { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Provider")]
        public String Provider { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "BillPayment")]
        public String BillPayment { get; set; }

        [DataMember]
        [XmlElement(ElementName = "PatientInfo")]
        public SuperBillPatientInfoDTO PatientInfo { get; set; }

        //________________ Encounter Info______________________________________________\\

        [DataMember]
        [XmlAttribute(AttributeName = "EncounterTypeID")]
        public int EncounterTypeID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ServiceTypeID")]
        public int ServiceTypeID { get; set; }

        //__________________________________________________________________________________\\

        [DataMember]       
        [XmlArrayAttribute("ProcedureInfo")]
        public List<SuperBillProcedureDTO> ProcedureInfo { get; set; }

        [DataMember]
        [XmlArrayAttribute("DiagnosisInfo")]
        public List<SuperBillDiagnosisDTO> DiagnosisInfo { get; set; }

        [DataMember]
        [XmlArrayAttribute("WRSInfo")]
        public List<SuperBillWorkRelatedServicesDTO> WRSInfo { get; set; }

        //________________ E/M Service Charges______________________________________________\\

        [DataMember]
        [XmlAttribute(AttributeName = "HistoryLevelScale")]
        public int HistoryLevelScale { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ExaminationLevelScale")]
        public int ExaminationLevelScale { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "DecisionLevelScale")]
        public int DecisionLevelScale { get; set; }

        [DataMember]
        [XmlArrayAttribute("ENMServiceInfo")]        
        public List<SuperBillENMServiceDTO> ENMServiceInfo { get; set; }

        //__________________________________________________________________________________\\

        [DataMember]
        [XmlAttribute(AttributeName = "BillingStatusTypeID")]
        public int BillingStatusTypeID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "PaymentTypeID")]
        public int PaymentTypeID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "PriInsOrganizationID")]
        public int PriInsOrganizationID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "SecInsOrganizationID")]
        public int SecInsOrganizationID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "TerInsOrganizationID")]
        public int TerInsOrganizationID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "HCFA")]
        public CMS1500QuestionsDTO HCFA { get; set; }


        [DataMember]
        [XmlAttribute(AttributeName = "ManagedByUserID")]
        public int ManagedByUserID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "PatientUserId")]
        public Int32 PatientUserId { get; set; }
                      
    }
}
