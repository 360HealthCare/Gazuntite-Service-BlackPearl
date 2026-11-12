using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.User.Patient
{
    [DataContract]
    public class PatientProfileSettingDTO : QuivusBaseDTO
    {


        [DataMember]
        public int UserUserProfileCategoryID { get; set; }
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public int ClinicBranchID { get; set; }
        [DataMember]
        public int SectionNo { get; set; }
        [DataMember]
        public int UserProfileCategoryID { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime CreatedAt { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
       
         [DataMember]
        public string DisplayText { get; set; }
         [DataMember]
        public string FieldData { get; set; }
        [DataMember]
         public string UserProfileCategoryName { get; set; }
        [DataMember]
        public bool IsSetAsDefault { get; set; }


        [DataMember]
        public string TooltipText { get; set; }
    }
}
