using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;
using Quivus.Model.QuickInput.ComprehensiveHistory.Medical;
using Quivus.Model.QuickInput.ComprehensiveHistory.Family;
using Quivus.Model.QuickInput.ComprehensiveHistory.Social;

namespace Quivus.Model.QuickInput.ComprehensiveHistory
{
    [DataContract]
    public class ComprehensiveHistoryDTO : QuivusBaseDTO
    {
        [DataMember]
        public List<MedicalHistoryDTO> MedicalHistory { get; set; }

        [DataMember]
        public List<FamilyHistoryDTO> FamilyHistory { get; set; }

        [DataMember]
        public List<SocialHistoryDTO> SocialHistory { get; set; }
    }
}
