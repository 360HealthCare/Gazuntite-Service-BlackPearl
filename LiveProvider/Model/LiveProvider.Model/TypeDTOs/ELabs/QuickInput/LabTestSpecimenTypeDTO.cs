using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LiveProvider.Model.TypeDTOs.ELabs.QuickInput
{
    [DataContract]
    public class LabTestSpecimenTypeDTO
    {
        [DataMember]
        public int LabTestSpecimenTypeID { get; set; }

        [DataMember]
        public string SpecimenName { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
