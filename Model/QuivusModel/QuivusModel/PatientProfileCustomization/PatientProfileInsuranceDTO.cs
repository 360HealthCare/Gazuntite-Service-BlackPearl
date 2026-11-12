using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;
using Quivus.Model.PatientProfileCustomization;

namespace Quivus.Model.PatientProfileCustomization
{
     [DataContract]
    public class PatientProfileInsuranceDTO : QuivusBaseDTO
    {
        [DataMember]
        public int PatientId { get; set; }

        [DataMember]
        public List<PatientInsuracneInfoDTO> PatientInsuranceInfoData { get; set; }
    }
}
