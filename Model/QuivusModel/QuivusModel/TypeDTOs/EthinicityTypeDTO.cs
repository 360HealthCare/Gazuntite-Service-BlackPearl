using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs
{
    public class EthinicityTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int EthinicityId { get; set; }

        [DataMember]
        public string EthinicityName { get; set; }

        [DataMember]
        public string EthinicityCode { get; set; }
    }
}
