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
    public class MethodTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int RecommendationMethodTypeID { get; set; }

        [DataMember]
        public string MethodName { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
