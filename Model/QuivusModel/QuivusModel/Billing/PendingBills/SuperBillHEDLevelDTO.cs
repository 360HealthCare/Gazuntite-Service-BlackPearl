using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.Billing.PendingBills
{
    [DataContract]
    public class SuperBillHEDLevelDTO
    {
        [DataMember]
        public int VisitID { get; set; }

        [DataMember]
        public int ChronicDiagnosis { get; set; }

        [DataMember]
        public int HPI { get; set; }

        [DataMember]
        public int ROS { get; set; }

        [DataMember]
        public int MedicalHistory { get; set; }

        [DataMember]
        public int FamilyHistory { get; set; }

        [DataMember]
        public int SocialHistory { get; set; }

        [DataMember]
        public int PhysicalTypes { get; set; }

        [DataMember]
        public Boolean IsNewEncounter { get; set; }
    }
}
