using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model;
using System.Runtime.Serialization;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Labs
{
    [DataContract]
    public class LIONICLabTestTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int LionicLabTestTypeID { get; set; }
        [DataMember]
        public string LOINC_NUM { get; set; }
        [DataMember]
        public string SHORTNAME { get; set; }
    }
}
