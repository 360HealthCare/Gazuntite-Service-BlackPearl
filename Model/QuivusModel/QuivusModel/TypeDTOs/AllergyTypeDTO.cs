using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class AllergyTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int AllergyTypeIdSeq { get; set; }

        [DataMember]
        public string AllergyName { get; set; }

        [DataMember]
        public string UniiCode { get; set; }

        [DataMember]
        public int AllergyAgentId { get; set; }

        [DataMember]
        public string AllergyAgentName { get; set; }

        [DataMember]
        public string ProblemType { get; set; }

        [DataMember]
        public string SnomedCode { get; set; }

        [DataMember]
        public string SnomedDescription { get; set; }

        [DataMember]
        public string NKDA { get; set; }

        [DataMember]
        public int Severity { get; set; }

        [DataMember]
        public string NdfRt { get; set; }

        [DataMember]
        public string RxNormCode { get; set; }
    }
}
