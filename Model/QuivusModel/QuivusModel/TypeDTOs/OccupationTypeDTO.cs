using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class OccupationTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int OccupationId { get; set; }

        [DataMember]
        public string OccupationName { get; set; }

        [DataMember]
        public string OccupationCode { get; set; }
    }
}
