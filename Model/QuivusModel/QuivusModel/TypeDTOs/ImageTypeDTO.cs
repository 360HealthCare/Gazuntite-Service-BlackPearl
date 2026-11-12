using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class ImageTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int ImageTypeId { get; set; }

        [DataMember]
        public string TypeName { get; set; }

        [DataMember]
        public string TypeDescription { get; set; }
    }
}