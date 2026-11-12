using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model;
using System.Runtime.Serialization;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.Followup
{
    [DataContract]
    public class FollowupDataControlTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int FollowupDataControlTypeID { get; set; }

        [DataMember]
        public string DataControlName { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
