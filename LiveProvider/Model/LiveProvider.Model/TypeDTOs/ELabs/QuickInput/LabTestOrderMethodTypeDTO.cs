using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LiveProvider.Model.TypeDTOs.ELabs.QuickInput
{
    [DataContract]
    public class LabTestOrderMethodTypeDTO
    {
        [DataMember]
        public int LabTestOrderMethodTypeID { get; set; }
        [DataMember]
        public string OrderMethodTypeName { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
