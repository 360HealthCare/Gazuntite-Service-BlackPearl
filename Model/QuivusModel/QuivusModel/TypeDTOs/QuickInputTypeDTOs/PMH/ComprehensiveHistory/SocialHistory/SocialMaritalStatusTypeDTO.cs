using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.SocialHistory
{
    [DataContract]
    public class SocialMaritalStatusTypeDTO : QuivusBaseTypeDTO
    {
      
        [DataMember]
        public int MaritalStatusID { get; set; }
        [DataMember]
        public string MaritalStatusAbbriviation { get; set; }
        [DataMember]
        public string MaritalStatusName { get; set; }
        [DataMember]
        public string MaritalStatusCode { get; set; }

    }
}
