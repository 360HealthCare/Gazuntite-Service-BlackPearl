using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model.User.Patient;
using Quivus.Model.PatientProfileCustomization;

namespace Quivus.Model.User.Patient
{
    [DataContract]
    public class ProfileSettingsCollectionDTO
    {
        [DataMember]
        public int UserUserProfileCategoryID { get; set; } //M

        [DataMember]
        public List<PatientAllergyInfoDTO> AllergyList { get; set; }
        [DataMember]
        public List<PatientDiagnosisInfoDTO> DiagnosisList { get; set; }
        [DataMember]
        public List<PatientImmunizationInfoDTO> ImmunizationList { get; set; }
        [DataMember]
        public List<PatientChronicMedicationInfoDTO> ChronicMedicationList { get; set; }
        [DataMember]
        public List<PatientInsuracneInfoDTO> InsuranceList { get; set; }
        [DataMember]
        public String CollectionType { get; set; }
    }
}
