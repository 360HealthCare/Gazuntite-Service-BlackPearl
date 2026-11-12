using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model;
using System.Runtime.Serialization;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Labs;

namespace Quivus.Model.QuickInput.Labs
{
    [DataContract]
    public class InHouseLabDTO : QuivusBaseDTO
    {

        [DataMember]
        public int PatientInHouseLabTestID { get; set; }

        [DataMember]
        public int PatientID { get; set; }

        [DataMember]
        public int VisitID { get; set; }

        [DataMember]
        public InHouseLabTestTypeDTO InHouseLabTestType { get; set; }
        [DataMember]
        public string TestResult { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public string NoteSubject { get; set; }

        [DataMember]
        public byte[] ResultAttachment { get; set; }

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
