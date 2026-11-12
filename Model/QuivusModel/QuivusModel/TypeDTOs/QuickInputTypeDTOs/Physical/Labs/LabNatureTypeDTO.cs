using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Labs
{
    [DataContract]
    public class LabNatureTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int LabTestNatureTypeID { get; set; }
        [DataMember]
        public string LabTestNatureName { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
