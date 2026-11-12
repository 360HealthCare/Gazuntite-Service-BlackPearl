using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class PrefixTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int PrefixId { get; set; }

        [DataMember]
        public string PrefixName { get; set; }

        [DataMember]
        public string PrefixCode { get; set; }
    }
}