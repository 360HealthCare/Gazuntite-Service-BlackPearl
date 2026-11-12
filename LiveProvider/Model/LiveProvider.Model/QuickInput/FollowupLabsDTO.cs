using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LiveProvider.Model.QuickInput
{
    [DataContract]
    public class FollowupLabsDTO
    {
        [DataMember]
        public int PatientFollowUpLabID { get; set; }

        [DataMember]
        public string LabTestDescription { get; set; }

        [DataMember]
        public bool IsInHouseLab { get; set; }

        [DataMember]
        public string TestCode { get; set; }
        [DataMember]
        public bool IsRecordEditable { get; set; }
    }
}
