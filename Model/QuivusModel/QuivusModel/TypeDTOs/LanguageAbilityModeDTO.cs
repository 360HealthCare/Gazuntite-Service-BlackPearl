using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class LanguageAbilityModeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int LanguageAbilityModeId { get; set; }

        [DataMember]
        public string LanguageAbilityModeCode { get; set; }

        [DataMember]
        public string LangaugeAbilityModeDescription { get; set; }
    }
}
