using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class RaceTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int RaceId { get; set; }

        [DataMember]
        public string RaceName { get; set; }

        [DataMember]
        public string RaceCode { get; set; }
    }
}
