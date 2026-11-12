using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class InsuranceTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int InsuranceTypeId { get; set; }

        [DataMember]
        public string InsuranceTypeName { get; set; }
    }
}
