using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.Recommendation;


namespace Quivus.Model.QuickInput.Recommendation
{
    //Author Mansoor
    //Date 22 Mar 2011
    [DataContract]
    public class PatientRecommendationDTO : QuivusBaseDTO
    {
         [DataMember]
        public string IsDiscussedWithPatient { get; set; }

         [DataMember]
         public bool IsActive { get; set; }

        [DataMember]
        public int PatientRecommendationID { get; set; }

        [DataMember]
        public int PatientID { get; set; }

        [DataMember]
        public int VisitID { get; set; }

        [DataMember]
        public PlanTypeDTO Plan { get; set; }

        [DataMember]
        public MethodTypeDTO Method { get; set; }

        [DataMember]
        public DurationTypeDTO Duration { get; set; }

        [DataMember]
        public string IsDiscussed { get; set; }

        [DataMember]
        public string Links { get; set; }

        [DataMember]
        public string Note { get; set; }

        [DataMember]
        public string NoteSubject { get; set; }

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
        public int ClinicBranchID { get; set; }

        [DataMember]
        public int CreatedByUserTypeID { get; set; }
        [DataMember]
        public int ModifiedByUserTypeID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string DiagnosisInfo { get; set; }

        [DataMember]
        public int PatientDiagnosisID { get; set; }

         }
}
