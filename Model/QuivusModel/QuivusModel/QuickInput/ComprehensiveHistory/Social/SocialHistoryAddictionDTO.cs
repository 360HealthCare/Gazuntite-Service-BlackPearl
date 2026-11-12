using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.QuickInput.ComprehensiveHistory.Social
{
    [DataContract]
    public class SocialHistoryAddictionDTO : QuivusBaseDTO
    {       
       
        [DataMember]
        public int SocialHistoryAddictionID { get; set; }
        [DataMember]
        public String IsPresentSelected { get; set; }
        [DataMember]
        public String IsEducated { get; set; }
        [DataMember]
        public bool IsUnknown { get; set; }
        [DataMember]
        public String OnSet { get; set; }// This field is updated from DateTime to String As per Requirment By Ismail July 14 2011
        [DataMember]
        public String EndDate { get; set; }// This field is updated from DateTime to String As per Requirment By Ismail July 14 2011
        [DataMember]
        public int PatientSocialHistoryID { get; set; }
        [DataMember]
        public int SocialHistoryAddictionTypeID { get; set; }
        [DataMember]
        public string AddictionNotes { get; set; }
        [DataMember]
        public string NoteSubject { get; set; }
        [DataMember]
        public int MediumTypeID { get; set; }
        [DataMember]
        public int UsageAmountID { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime ModifiedAt { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public int VisitId { get; set; }
        
      

        [DataMember]
        public Guid DictationId { get; set; }

           [DataMember]
        public string IsYesSelected { get; set; }

          [DataMember]
        public string IsNoSelected { get; set; }
    }
}
