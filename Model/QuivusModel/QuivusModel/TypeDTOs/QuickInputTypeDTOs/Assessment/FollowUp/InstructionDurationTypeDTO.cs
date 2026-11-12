using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp
{
    public class InstructionDurationTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int DurationTypeID { get; set; }

        [DataMember]
        public string DurationTypeName { get; set; }
    }
}
