using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class SpecialityTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int SpecialityTypeID { get; set; }

        [DataMember]
        public string SpecialityName { get; set; }
    }

}
