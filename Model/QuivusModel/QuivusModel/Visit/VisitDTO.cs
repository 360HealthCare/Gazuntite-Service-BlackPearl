using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Quivus.Model.Clinic;
using Quivus.Model.TypeDTOs;

namespace Quivus.Model.Visit
{
    [DataContract]
    public class VisitDTO : QuivusBaseDTO
    {
       
        public VisitDTO()
        {
            //ClinicBranch = new ClinicBranchDTO();
            //UserType = new UserTypeDTO();
        }

        [DataMember]
        public bool IsValidDataExists { get; set; }

        [DataMember]
        public Int32 VisitId { get; set; }
        
        [DataMember]
        public int PatientId { get; set; }

        [DataMember]
        public int OwnerId { get; set; }

        [DataMember]
        public UserTypeDTO OwnerUserType { get; set; } /* Added By Ismail :)*/

        [DataMember]
        public ClinicBranchDTO ClinicBranch { get; set; }

        [DataMember]
        public DateTime VisitDate { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }

        [DataMember]
        public string RecordStatus { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public UserTypeDTO UserType { get; set; }

        [DataMember]
        public string VisitStatus { get; set; }

        [DataMember]
        public Guid RecGuid { get; set; }

        [DataMember]
        public bool IsVirtualVisit { get; set; }

        [DataMember]
        public int AppointmentId { get; set; }

        [DataMember]
        public int EncounterTypeId { get; set; }

        [DataMember]
        public int TemplateId { get; set; }

        [DataMember]
        public int SignedByUserId { get; set; }

        [DataMember]
        public UserTypeDTO SignedByUserType { get; set; }

        [DataMember]
        public DateTime SignedDate { get; set; }

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
        public DateTime CoSignedDate { get; set; }

        [DataMember]
        public string IsDressedForPhysical { get; set; }

        [DataMember]
        public int TemplateVisitId { get; set; }

        [DataMember]
        public bool IsRecordEditable { get; set; }

        [DataMember]
        public int CreatedStationID { get; set; }
    }
}
