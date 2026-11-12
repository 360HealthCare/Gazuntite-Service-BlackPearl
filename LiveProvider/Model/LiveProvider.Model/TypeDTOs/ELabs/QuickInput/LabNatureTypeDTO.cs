using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LiveProvider.Model.TypeDTOs.ELabs.QuickInput
{
    [DataContract]
    public class LabNatureTypeDTO
    {
        [DataMember]
        public int LabTestNatureTypeID { get; set; }
        [DataMember]
        public string LabTestNatureName { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
