using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs;

namespace Quivus.Model.QuickInput.Procedure
{
    [DataContract]
    public class ProcedureDTO : QuivusBaseDTO
    {

        [DataMember]
        public int PatientProcedureID { get; set; }
        [DataMember]
        public int VisitID { get; set; }
        [DataMember]
        public int PatientID { get; set; }
        [DataMember]
        public CPTMedicalTypeDTO CPTMedicalType { get; set; }
        [DataMember]
        public string Notes { get; set; }
        [DataMember]
        public string NoteSubject { get; set; }
        [DataMember]
        public Guid DictationID { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime CreatedAt { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public DateTime ModifiedAt { get; set; }
        [DataMember]
        public DateTime VisitDate { get; set; }
        [DataMember]
        public bool IsRecordEditable { get; set; } 
      
    }
}
