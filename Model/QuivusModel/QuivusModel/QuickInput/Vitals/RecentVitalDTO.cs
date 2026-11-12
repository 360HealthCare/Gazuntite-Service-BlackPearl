using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.QuickInput.Vitals
{

    [DataContract]
    public class RecentVitalDTO : QuivusBaseDTO
    {

        [DataMember]
        public int PatientVitalID { get; set; }
        [DataMember]
        public string VitalNote { get; set; }

        [DataMember]
        public int VisitID { get; set; }
        [DataMember]
        public string VitalDescription { get; set; }
        [DataMember]
        public Guid DictationID { get; set; }
        [DataMember]
        public string Height { get; set; }
        [DataMember]
        public string Weight { get; set; }
        [DataMember]
        public string Temperature { get; set; }
        [DataMember]
        public string Pulse { get; set; }
        [DataMember]
        public string Respiration { get; set; }
        [DataMember]
        public string BPSys { get; set; }
        [DataMember]
        public string BPDys { get; set; }

        public string BMI { get; set; }
        [DataMember]
        public bool IsMetricDisplaySelected { get; set; }
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
        public string VisitEncounterTypeName { get; set; }

         [DataMember]
         public string Oxygen { get; set; }
    }
}
