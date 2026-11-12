using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.SocialHistory
{
    [DataContract]
    public class SocialHistoryAddictionTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int SocialHistoryAddictionTypeID { get; set; }
        [DataMember]
        public string AddictionName { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
