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
    public class PatientAllergyInfoDTO : QuivusBaseDTO
    {
        [DataMember]
        public int PatientAllergyId { get; set; }        

        [DataMember]
        public int Index { get; set; }

        [DataMember]
        public string AllergyName { get; set; }

        [DataMember]
        public bool IsActive { get; set; }
    }
}
