using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class SmokingTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int SmokingTypeId { get; set; }

        [DataMember]
        public string SmokingTypeCode { get; set; }

        [DataMember]
        public string SmokingTypeName { get; set; }
    }
}
