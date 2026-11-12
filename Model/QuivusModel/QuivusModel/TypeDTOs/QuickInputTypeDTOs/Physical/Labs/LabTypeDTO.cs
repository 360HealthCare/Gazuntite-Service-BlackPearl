using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Labs
{

    [DataContract]
    public class LabTypeDTO : QuivusBaseTypeDTO
    {

        [DataMember]
        public int LabTypeID { get; set; }
        [DataMember]
        public string LabName { get; set; }
    }
}
