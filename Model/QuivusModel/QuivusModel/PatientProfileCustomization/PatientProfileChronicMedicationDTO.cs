using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model;
using System.Runtime.Serialization;
using Quivus.Model.PatientProfileCustomization;

namespace Quivus.Model.PatientProfileCustomization
{
     [DataContract]
    public class PatientProfileChronicMedicationDTO:QuivusBaseDTO
    {
        [DataMember]
        public int PatientId { get; set; }

        [DataMember]
        public List<PatientChronicMedicationInfoDTO> PatientChronicMedicationInfoData { get; set; }
    }
}
