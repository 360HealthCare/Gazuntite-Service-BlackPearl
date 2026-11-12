using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Labs
{
    [DataContract]
    public class LabTestOrderMethodTypeDTO : QuivusBaseTypeDTO
    {

        [DataMember]
        public int LabTestOrderMethodTypeID { get; set; }
        [DataMember]
        public string OrderMethodTypeName { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
