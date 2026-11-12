using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Labs
{
    [DataContract]
    public class LabTestPriorityTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int LabTestPriorityTypeID { get; set; }

        [DataMember]
        public string PriorityName { get; set; }

        [DataMember]
        public string Description { get; set; }

    }
}
