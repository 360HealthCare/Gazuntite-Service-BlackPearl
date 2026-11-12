using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp;

namespace Quivus.Model.QuickInput.FollowUp
{
    //Author Mansoor
    //Date 22 Mar 2011
    [DataContract]
    public class PatientFollowUpDTO : QuivusBaseDTO
    {
        [DataMember]
        public int PatientFollowUpID { get; set; }

        [DataMember]
        public int PatientID { get; set; }

        [DataMember]
        public int VisitID { get; set; }

        [DataMember]
        public FollowUpMethodTypeDTO FollowUpMethodType { get; set; }

        [DataMember]
        public bool IsByDate { get; set; }

        [DataMember]
        public String FollowUpDate { get; set; }

        [DataMember]
        public bool IsOnCompletion { get; set; }

        [DataMember]
        public bool IsLabTestSelected { get; set; }

        [DataMember]
        public bool IsDrugselected { get; set; }        

        [DataMember]
        public int LabTestID { get; set; }

        [DataMember]
        public bool IsInHouseLabTestSelected { get; set; }

        [DataMember]
        public int DrugID { get; set; }

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
