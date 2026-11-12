using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class StatusTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int StatusID { get; set; }

        [DataMember]
        public string StatusName { get; set; }
    }
}