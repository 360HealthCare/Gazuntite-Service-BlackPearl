using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class GenderTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int GenderId { get; set; }

        [DataMember]
        public string GenderName { get; set; }

        [DataMember]
        public string GenderCode { get; set; }
    }
}