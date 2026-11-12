using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Social
{
    [DataContract]
    public class SocialHistoryUsageAmountTypeResponse : ServiceResponse
    {

        [DataMember]
        public List<SocialHistoryUsageAmountTypeDTO> TobacooList { get; set; }

        [DataMember]
        public List<SocialHistoryUsageAmountTypeDTO> DrugsList { get; set; }

        [DataMember]
        public List<SocialHistoryUsageAmountTypeDTO> AlcoholList { get; set; }


        [DataMember]
        public List<SocialHistoryUsageAmountTypeDTO> ResponseList { get; set; }
    }
}
