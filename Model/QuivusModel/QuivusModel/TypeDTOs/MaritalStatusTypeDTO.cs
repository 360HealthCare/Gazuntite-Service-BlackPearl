using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class MaritalStatusTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int MaritalStatusId { get; set; }

        [DataMember]
        public string MaritalStatusName { get; set; }


        [DataMember]
        public string MaritalStatusCode { get; set; }
    }
}
