using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LiveProvider.Model.TypeDTOs.DocumentManager
{
    [DataContract]
    public class LPatientSearchResultDTO :LiveProviderBaseDTO
    {
        [DataMember]
        public List<LPatientSearchDTO> PatientSearchList { get; set; }

        [DataMember]
        public int CurrentPageNo { get; set; }

        [DataMember]
        public int CurrentFirstRecordNo { get; set; }

        [DataMember]
        public int CurrentLastRecordNo { get; set; }

        [DataMember]
        public int TotalNoOfRecords { get; set; }
    }
}
