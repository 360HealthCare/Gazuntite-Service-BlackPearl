using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp
{
    [DataContract]
    public class InstructionAgeValueTypeDTO
    {
        [DataMember]
        public int InstructionAgeValueTypeID { get; set; }

        [DataMember]
        public string AgeValueTypeName { get; set; }
    }
}