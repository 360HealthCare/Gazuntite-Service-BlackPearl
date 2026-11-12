using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp
{
    //Author Mansoor
    //Date 25 Mar 2011

    [DataContract]
    public class FollowUpMethodTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int FollowUpMethodTypeID { get; set; }

        [DataMember]
        public string MethodName { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
