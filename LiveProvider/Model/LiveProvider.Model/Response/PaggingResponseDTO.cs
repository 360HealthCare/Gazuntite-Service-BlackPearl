using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using LiveProvider.Model.TypeDTOs.DocumentManager;

namespace LiveProvider.Model.Response
{
    [DataContract]
    public class PaggingResponseDTO
    {
        [DataMember]
        public int TotalRecord { get; set; }

        [DataMember]
        public int FirstRecord { get; set; }

        [DataMember]
        public int LastRecord { get; set; }

        [DataMember]
        public List<PatientDocumentDTO> PatientDocumentsList { get; set; }
    }
}
