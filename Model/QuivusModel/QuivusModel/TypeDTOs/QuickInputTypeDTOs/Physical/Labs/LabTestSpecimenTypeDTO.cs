using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Labs
{
    [DataContract]
    public class LabTestSpecimenTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int LabTestSpecimenTypeID { get; set; }

        [DataMember]
        public string SpecimenName { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
