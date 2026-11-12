using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LiveProvider.Model.TypeDTOs
{
    [DataContract]
    public class ICDCodeDTO
    {
        [DataMember]
        public int IcdCodeIdSeq { get; set; }

        [DataMember]
        public string IcdCodeName { get; set; }

        [DataMember]
        public string IcdCodeDescription { get; set; }

        [DataMember]
        public string IsChronic { get; set; }

        [DataMember]
        public string ProblemType { get; set; }

        [DataMember]
        public string SNOMEDCode { get; set; }

        [DataMember]
        public string SNOMEDDescription { get; set; }

        [DataMember]
        public int DiagnosisId { get; set; }
    }
}
