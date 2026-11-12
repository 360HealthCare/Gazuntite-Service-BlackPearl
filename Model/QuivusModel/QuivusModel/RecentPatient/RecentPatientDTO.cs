using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.RecentPatient
{
    [DataContract]
    public class RecentPatientDTO : QuivusBaseDTO
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int ClinicBranchId { get; set; }

        [DataMember]
        public bool AllowEmailCommunication { get; set; }
        
         [DataMember]
        public int UserRecentPatientId { get; set; }
        
        [DataMember]
        public int Index { get; set; }

        [DataMember]
        public int PatientUserID { get; set; }

        [DataMember]
        public int PatientId { get; set; }

        [DataMember]
        public string PatientName { get; set; }

        [DataMember]
        public string QuivusId { get; set; }

        [DataMember]
        public DateTime PatientDOB { get; set; }

        [DataMember]
        public string LastVisitDate { get; set; }

        [DataMember]
        public bool IsEmailIdAvailable { get; set; }

        [DataMember]
        public bool IsSnailIdAvailable { get; set; }

        [DataMember]
        public bool IsSMSNoAvalable { get; set; }

        [DataMember]
        public bool IsChatIdAvailable { get; set; }

        [DataMember]
        public bool IsPhoneNoAvailable { get; set; }
    }
}
