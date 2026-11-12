using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.QuickInput
{
    [DataContract]
    public class ChronicDiagnosisDTO : QuivusBaseDTO
    {
        [DataMember]
        public int PatinetChronicDiagnosisId { get; set; }

        [DataMember]
        public DateTime OnSet { get; set; }

        [DataMember]
        public string ChronicDiagnosisTypeStatus { get; set; }

        //[DataMember]
        //public ICDCode ICDCode
        //{ get; set; }

        [DataMember]
        public string Note { get; set; }

        [DataMember]
        public string NoteSubject { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public DateTime UpdatedDate { get; set; }

        [DataMember]
        public int VisitId { get; set; }

        //[DataMember]
        //public string IsOld { get; set; }

        [DataMember]
        public DateTime VisitDate { get; set; }      

        [DataMember]
        public Guid DictationID { get; set; }

        //[DataMember]
        //public DateTime DictationTimeStamp { get; set; }
    }
}
