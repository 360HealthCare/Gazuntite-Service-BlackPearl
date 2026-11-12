using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class InsuranceNatureDTO : QuivusBaseTypeDTO
    { 
        [DataMember]
        public int InsuranceNatureId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
