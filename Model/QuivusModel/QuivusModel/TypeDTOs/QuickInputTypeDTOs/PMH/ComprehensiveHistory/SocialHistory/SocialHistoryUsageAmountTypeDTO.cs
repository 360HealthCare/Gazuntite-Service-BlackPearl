using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.SocialHistory
{
    [DataContract]
    public class SocialHistoryUsageAmountTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int SocialHistoryUsageAmountID { get; set; }
        [DataMember]
        public string UsageAmountName { get; set; }

         [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Description { get; set; }

         [DataMember]
        public int SocialHistoryAddictionTypeID { get; set; }

         [DataMember]
        public string AddictionName { get; set; }

          [DataMember]
        public bool IsPresentApplicable { get; set; }

          [DataMember]
        public bool IsPastApplicable { get; set; }
   
    }
}
