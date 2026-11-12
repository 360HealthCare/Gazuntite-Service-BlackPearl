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
    public class DurationTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int RecommendationDurationTypeID { get; set; }

        [DataMember]
        public string DurationName { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
