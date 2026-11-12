using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp;

namespace Quivus.Model.QuickInput.FollowUp
{
    [DataContract]
    public class PatientFollowupInstructionDTO : QuivusBaseDTO
    {
        [DataMember]
        public int PatientID { get; set; }

        [DataMember]
        public DateTime VisitDate { get; set; }

        [DataMember]
        public int FollowupInstructionID { get; set; }

        [DataMember]
        public ServiceTypeDTO objServiceTypeDTO { get; set; }

        [DataMember]
        public InstructionDurationTypeDTO AfterInstructionDurationTypeDTO { get; set; }

        [DataMember]
        public InstructionDurationTypeDTO ForInstructionDurationTypeDTO { get; set; }

        [DataMember]
        public InstructionAgeTypeDTO AgeTypeDTO  { get; set; }


        [DataMember]
        public InstructionAgeValueTypeDTO AgeValueTypeDTO { get; set; }

        [DataMember]
        public int AfterDurationValue { get; set; }

        [DataMember]
        public int ForDurationValue { get; set; }

        [DataMember]
        public bool IsAgeSelected { get; set; }

        [DataMember]
        public bool IsDurationSelected { get; set; }

        [DataMember]
        public string Notes { get; set; }
        [DataMember]
        public string NoteSubject { get; set; }
        [DataMember]
        public Guid DictationID { get; set; }
        [DataMember]
        public int VisitID { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }
        
        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public DateTime ModifiedAt { get; set; }

         [DataMember]
        public int ClinicBranchId { get; set; }
         [DataMember]
         public bool IsRecordEditable { get; set; } 
    }
}
