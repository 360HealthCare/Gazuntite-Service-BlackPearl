using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LiveProvider.Model.TypeDTOs.ELabs.QuickInput
{
    [DataContract]
    public class LabTestPriorityTypeDTO
    {
        [DataMember]
        public int LabTestPriorityTypeID { get; set; }

        [DataMember]
        public string PriorityName { get; set; }

        [DataMember]
        public string Description { get; set; }
        
    }
}
