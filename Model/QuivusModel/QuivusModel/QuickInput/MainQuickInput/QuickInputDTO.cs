using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;
using Quivus.Model.QuickInput.MainQuickInput.PMH;
using Quivus.Model.QuickInput.MainQuickInput.Physical;
using Quivus.Model.QuickInput.MainQuickInput.Assessment;
using Quivus.Model.QuickInput.Notes;
using Quivus.Model.QuickInput.Dictation;
using Quivus.Model.Visit;

namespace Quivus.Model.QuickInput.MainQuickInput
{
    [DataContract]
    public class QuickInputDTO : QuivusBaseDTO
    {
        [DataMember]
        public VisitDTO Visit { get; set; }

        [DataMember]
        public PMHDTO PMH { get; set; }

        [DataMember]
        public PhysicalDTO Physical { get; set; }

        [DataMember]
        public AssessmentDTO Assessment { get; set; }

        [DataMember]
        public List<ModuleNoteDTO> ModuleNotes { get; set; }

        [DataMember]
        public List<ModuleDictationDTO> ModuleDictations { get; set; }

        [DataMember]
        public int PatientId { get; set; }

        [DataMember]
        public int VisitId { get; set; }

        [DataMember]
        public bool IsSigned { get; set; }

        [DataMember]
        public int SignedByUserId { get; set; }

        [DataMember]
        public int SignedByUserTypeId { get; set; }

        [DataMember]
        public DateTime SignedDate { get; set; }

        [DataMember]
        
        public bool isReviewed { get; set; }

        [DataMember]
        public DateTime ActionDate { get; set; }

        [DataMember]
        public int ClinicBranchId { get; set; }

    }
}
