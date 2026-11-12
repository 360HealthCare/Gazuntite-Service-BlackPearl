using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace QuivusModel.QuickInput.ComprehensiveHistory.Social
{
    [DataContract]
    public class SocialhistoryVBDTO : QuivusBaseDTO
    {
        [DataMember]
        public Guid DictationId { get; set; }
        [DataMember]
        public string EndDate { get; set; }
        [DataMember]
        public string OnSet { get; set; }
        [DataMember]
        public string Educated { get; set; }
        [DataMember]
        public string Amount { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Current { get; set; }
        [DataMember]
        public string Note { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string Social { get; set; }

        [DataMember]
        public int PatientSocialHistoryID { get; set; }

        [DataMember]
        public int VisitID { get; set; }

        [DataMember]
        public DateTime VisitDate { get; set; }
        [DataMember]
        public bool IsRecordEditable { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime ModifiedAt { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public int PatientID { get; set; }
    }
}
