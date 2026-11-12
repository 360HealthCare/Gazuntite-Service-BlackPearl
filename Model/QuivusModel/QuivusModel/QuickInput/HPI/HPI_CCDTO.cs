using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.QuickInput.HPI
{
    //Author Mansoor
    //Date 21 Mar 2011
    [DataContract]
    public class HPI_CCDTO:QuivusBaseDTO
    {
        [DataMember]
        public string ChiefComplaint { get; set; }

        [DataMember]
        public HPIDetailsDTO HPIFields { get; set; }

        [DataMember]
        public int PatientHPIID { get; set; }

        [DataMember]
        public int PatientID { get; set; }

        [DataMember]
        public int VisitID { get; set; }

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
        public int ClinicBranchID { get; set; }

        [DataMember]
        public int CreatedByUserTypeID { get; set; }

        [DataMember]
        public int ModifiedByUserTypeID { get; set; }

        [DataMember]
        public DateTime VisitDate { get; set; }
        [DataMember]
        public bool IsRecordEditable { get; set; } 
    }
}
