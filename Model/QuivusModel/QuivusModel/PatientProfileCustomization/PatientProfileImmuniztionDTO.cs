using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model.PatientProfileCustomization;
using Quivus.Model;

namespace Quivus.Model.PatientProfileCustomization
{
      [DataContract]
    public class PatientProfileImmuniztionDTO : QuivusBaseDTO
    {
        [DataMember]
        public int PatientId { get; set; }

        [DataMember]
        public List<PatientImmunizationInfoDTO> PatientImmunizationInfoData { get; set; }
    }
}
