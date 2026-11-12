using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.PatientProfileCustomization
{
    /// <summary>
    /// This is a light weight DTO which contains the patient diagnosis info to be shown to the provider, after loading patient,  and tagging
    /// </summary>

    [DataContract]
    public class PatientDiagnosisInfoDTO : QuivusBaseDTO
    {
        [DataMember]
        public float PatientHistoryMedicalID { get; set; }

        [DataMember]
        public int Index { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string ICDCodeId { get; set; }

        [DataMember]
        public string Nature { get; set; }

        [DataMember]
        public bool IsActive { get; set; }
    }
}
