using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.SocialHistory
{
    [DataContract]
    public class SocialHistoryMediumTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int SocialHistoryMediumID { get; set; }
        [DataMember]
        public string MediumName { get; set; }
        [DataMember]
        public string Description { get; set; }

         [DataMember]
        public int SocialHistoryAddictionTypeID { get; set; }
        [DataMember]
        public string AddictionName { get; set; }

       
    }
}
