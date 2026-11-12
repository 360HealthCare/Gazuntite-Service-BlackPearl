using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class ImmunizationTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int ImmunizationTypeId { get; set; }

        [DataMember]
        public string ImmunizationTypeName { get; set; }

        //[DataMember]
        //public DateTime CREATED_DT { get; set; }
    }
}
