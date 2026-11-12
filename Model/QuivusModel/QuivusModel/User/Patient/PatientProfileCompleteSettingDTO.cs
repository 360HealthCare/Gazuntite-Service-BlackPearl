using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model.User.Patient;

namespace Quivus.Model.User.Patient
{
    [DataContract]
    public class PatientProfileCompleteSettingDTO
    {
        [DataMember]
        public List<PatientProfileSettingDTO> ResponseData { get; set; }  // This Will ReponseData

        [DataMember]
        public PatientProfileSettingDTO SectionFirstData { get; set; }

        [DataMember]
        public PatientProfileSettingDTO SectionSecondData { get; set; }

        [DataMember]
        public ProfileSettingsCollectionDTO SectionThirdData { get; set; }
    }
}
