using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp
{
    public class InstructionAgeTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int InstructionAgeTypeID { get; set; }

        [DataMember]
        public string AgeTypeName { get; set; }
    }
}
