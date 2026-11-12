using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.MedicalHistory;

namespace Quivus.Model.QuickInput.ComprehensiveHistory.Medical
{
    /// <summary>
    /// Author Muhammad Ismail
    /// Date 11 March 2011
    /// Class Used As a bean for Patient Medical History Under Quick Input Section
    /// </summary>
    [DataContract]
    public class MedicalHistoryDTO : QuivusBaseDTO
    {
         [DataMember]
        public int PatientMedicalHistoryID {get;set;}

        [DataMember]
        public int MedicalHistoryTypeID { get; set; }

        [DataMember]
        public int VisitID { get; set; }

        [DataMember]
        public string MedicalHistoryNote { get; set; }

        [DataMember]
        public string NoteSubject { get; set; }

        [DataMember]
        public string PatientMedicalHistoryDescription { get; set; }

         [DataMember]
        public MedicalHistoryTypeDTO MedicalHistoryType { get; set; }

        
        [DataMember]
        public Guid DictationID { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

         [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public DateTime ModifiedAt { get; set; }


        [DataMember]
        public DateTime VisitDate { get; set; } 

        [DataMember]
        public bool IsRecordEditable { get; set; }

        [DataMember]
        public string IsNone { get; set; }
        
    }
}
