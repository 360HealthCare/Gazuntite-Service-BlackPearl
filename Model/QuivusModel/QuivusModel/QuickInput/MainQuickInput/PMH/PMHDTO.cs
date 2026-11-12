using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;
using Quivus.Model.QuickInput.ComprehensiveHistory;
using Quivus.Model.QuickInput;

namespace Quivus.Model.QuickInput.MainQuickInput.PMH
{
    [DataContract]
    public class PMHDTO : QuivusBaseDTO
    {
        [DataMember]
        public List<AllergyDTO> Allergy { get; set; }

        [DataMember]
        public List<ChiefComplaintDTO> ChiefComplaint { get; set; }

        [DataMember]
        public List<ChronicDiagnosisDTO> ChronicDiagnosis { get; set; }

        [DataMember]
        public List<MedicationPrescribedDTO> ChronicMedication { get; set; }

        [DataMember]
        public ComprehensiveHistoryDTO ComprehensiveHistory { get; set; }

        [DataMember]
        public List<ImmunizationDTO> Immunization { get; set; }

    }
}
