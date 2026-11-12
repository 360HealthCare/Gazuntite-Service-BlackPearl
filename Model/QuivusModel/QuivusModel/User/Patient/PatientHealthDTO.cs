using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.User.Patient
{
    /// <summary>
    /// Author Mansoor
    /// Date 24 Jan 2011
    /// this will contains the patient health info 
    /// </summary>

    [DataContract]
    public class PatientHealthDTO :QuivusBaseDTO
    {
        [DataMember]
        public List<PatientAllergyDTO> PatientAllergyDTOList { get; set; }

        [DataMember]
        public List<PatientFamilyHistoryDTO> PatientFamilyHistoryDTOList { get; set; }

        [DataMember]
        public List<PatientMedicalHistoryDTO> PatientMedicalHistoryDTOList { get; set; }
    }
}
