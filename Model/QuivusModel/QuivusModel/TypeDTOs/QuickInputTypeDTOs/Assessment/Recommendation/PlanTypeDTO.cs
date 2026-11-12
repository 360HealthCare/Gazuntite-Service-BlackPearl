using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.Recommendation
{
    //Author Mansoor
    //Date 22 Mar 2011

    [DataContract]
    public class PlanTypeDTO : QuivusBaseTypeDTO
    {

        [DataMember]
        public int RecommendationPlanTypeID { get; set; }

        [DataMember]
        public string PlanName { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
