using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;
using Quivus.Model.QuickInput.Recommendation;
using Quivus.Model.QuickInput.Diagnosis;
using Quivus.Model.QuickInput.FollowUp;
using Quivus.Model.QuickInput.Procedure;
using Quivus.Model.QuickInput.FollowUp;

namespace Quivus.Model.QuickInput.MainQuickInput.Assessment
{
    [DataContract]
    public class AssessmentDTO : QuivusBaseDTO
    {
        [DataMember]
        public List<PatientRecommendationDTO> Recommendation { get; set; }

        [DataMember]
        public List<PatientDiagnosisDTO> Diagnosis { get; set; }

        [DataMember]
        public PatientFollowUpDTO PatientFollowUp { get; set; }

        [DataMember]
        public List<PatientFollowupInstructionDTO> PatientFollowupInstruction { get; set; }

        [DataMember]
        public List<ProcedureDTO> Procedure { get; set; }       
    }
}
