using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Social
{
    [DataContract]
    public class SocialHistoryMediumTypeResponse : ServiceResponse
    {
        [DataMember]
        public List<SocialHistoryMediumTypeDTO> AlcoholList { get; set; }

        [DataMember]
        public List<SocialHistoryMediumTypeDTO> TobaccoList { get; set; }

        [DataMember]
        public List<SocialHistoryMediumTypeDTO> DrugsList { get; set; }

         [DataMember]
        public List<SocialHistoryMediumTypeDTO> ResponseList { get; set; }
    }
}
