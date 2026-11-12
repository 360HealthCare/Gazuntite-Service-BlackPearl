using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Quivus.Model.AddressBook;

namespace Quivus.Model.User.Patient
{
    /// <summary>
    /// Author Mansoor
    /// Date 02 Feb 2011
    /// Contains the Patient profile info data
    /// </summary>
    
    [DataContract]
    public class PatientProfileDTO : QuivusBaseDTO
    {
        [DataMember]
        public int PatientId { get; set; }

        [DataMember]
        public BaseUserDTO UserDTO { get; set; }

        [DataMember]
        public PatientDTO PatientDTO { get; set; }

        [DataMember]
        public int InsuranceBalance { get; set; }

        [DataMember]
        public int RejectedClaims { get; set; }     

        [DataMember]
        public int PatientBalance { get; set; }

        [DataMember]
        public int ClaimsOverDue { get; set; }

        [DataMember]
        public bool IsChronicDiagnosis { get; set; }

        [DataMember]
        public List<PatientLanguageDTO> PatientLanguageList { get; set; }

        [DataMember]
        public List<PatientEmergencyContactDTO> PatientEmergencyContactList { get; set; }

        [DataMember]
        public PatientGuarantorDTO PatientGurantor { get; set; }

        [DataMember]
        public List<PatientAllergyDTO> PatientAllergyList { get; set; }

        [DataMember]
        public List<PatientMedicalHistoryDTO> PatientMedicalHistoryList { get; set; }

        [DataMember]
        public List<PatientFamilyHistoryDTO> PatientFamilyHistoryList { get; set; }

        [DataMember]
        public List<PatientInsuranceDTO> PatientInsuranceList { get; set; }

        [DataMember]
        public AddressBookDTO PatientAddressBook { get; set; }     
    }
}
