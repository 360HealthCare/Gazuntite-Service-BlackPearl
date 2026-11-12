using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class RelationshipTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int RelationshipId { get; set; }

        [DataMember]
        public string RelationshipName { get; set; }

        [DataMember]
        public string RelationshipCode { get; set; }
    }
}
