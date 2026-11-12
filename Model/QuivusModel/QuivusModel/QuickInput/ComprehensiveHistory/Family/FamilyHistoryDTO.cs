using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.FamilyHistory;

namespace Quivus.Model.QuickInput.ComprehensiveHistory.Family
{
    /// <summary>
    /// Author Muhammad Ismail
    /// Date 13 March 2011
    /// Class Used As a bean for Patient Family History Under Quick Input Section
    /// </summary>
    [DataContract]
    public class FamilyHistoryDTO : QuivusBaseDTO
    {
      
        [DataMember]
        public string DiseaseCode { get; set; }
        [DataMember]
        public string DiseaseName { get; set; }
        [DataMember]
        public bool IsSystemSpecified { get; set; }
        [DataMember]
        public int DiseaseID { get; set; }      

        [DataMember]
        public string FamilyHistoryTypeName { get; set; }
        [DataMember]
        public int PatientFamilyHistoryID { get; set; }
              
        [DataMember]
        public int FamilyHistoryTypeID { get; set; }
        [DataMember]
        public string FamilyHistoryNote { get; set; }
        [DataMember]
        public bool IsPresent { get; set; }
        [DataMember]
        public int ReletionshipTypeID { get; set; }
        [DataMember]
        public string ReletionshipTypeName { get; set; }
        [DataMember]
        public string IsYesSelected { get; set; }
       
        [DataMember]
        public string NoteSubject { get; set; }      
        [DataMember]
        public int VisitID { get; set; }
        [DataMember]
        public Guid DictationID { get; set; }
        [DataMember]
        public DateTime CreatedAt { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime ModifiedAt { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
       
        [DataMember]
        public DateTime VisitDate { get; set; }

        [DataMember]
        public bool IsRecordEditable { get; set; } 
    }
}
