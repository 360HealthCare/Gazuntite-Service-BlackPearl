using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model.TypeDTOs;
using Quivus.Model;

namespace Quivus.Model.QuickInput.Diagnosis
{
    //Author Mansoor
    //Date 22 Mar 2011
    [DataContract]
    public class PatientDiagnosisDTO : QuivusBaseDTO
    {
        [DataMember]
        public int PatientDiagnosisID { get; set; }

        [DataMember]
        public int PatientID { get; set; }

        [DataMember]
        public int VisitID { get; set; }

        [DataMember] // for grid only
        public DateTime VisitDate { get; set; }

        [DataMember]
        public String OnSet { get; set; }// This field is updated from DateTime to String As per Requirment By Ismail July 14 2011

        [DataMember]
        public ICDCodeDTO ICDCode { get; set; }

        //[DataMember]
        //public string IsChronic { get; set; }

        [DataMember]
        public bool IsChronic { get; set; }

        [DataMember]
        public bool IsActive { get; set; }
        
        [DataMember]
        public string Note { get; set; }
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
        public string RCOPIAID { get; set; }

        [DataMember]
        public string ICDCodeName { get; set; }
        [DataMember]
        public string ICDCodeDescription { get; set; }

        [DataMember]
        public bool IsRecordEditable { get; set; } 
    }
}
