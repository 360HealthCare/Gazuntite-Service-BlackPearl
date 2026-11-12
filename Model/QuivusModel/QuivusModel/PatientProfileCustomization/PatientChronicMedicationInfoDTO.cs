using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.PatientProfileCustomization
{
    /// <summary>
    /// This is a light weight DTO which contains the patient allergies name and status to be shown to the provider, after loading patient
    /// </summary>

    [DataContract]
    public class PatientChronicMedicationInfoDTO : QuivusBaseDTO
    {
        //[DataMember]
        //public string BrandName { get; set; }

        //[DataMember]
        //public string Status { get; set; }

        [DataMember]
        public int PatientMedicationId { get; set; }

        [DataMember]
        public int Index { get; set; }

        [DataMember]
        public string MedicationName { get; set; }

        [DataMember]
        public string MedicationStrength { get; set; }

        [DataMember]
        public string Dose { get; set; }

        [DataMember]
        public bool IsActive { get; set; }
    }
}
