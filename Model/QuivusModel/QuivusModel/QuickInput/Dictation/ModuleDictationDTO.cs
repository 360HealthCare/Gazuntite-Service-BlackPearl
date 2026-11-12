using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.QuickInput.Dictation
{
    [DataContract]
    public class ModuleDictationDTO : QuivusBaseDTO
    {
        [DataMember]
        public int ModuleDictationId { get; set; }

        [DataMember]
        public int VisitID { get; set; }

        [DataMember]
        public DateTime VisitDate { get; set; }

        [DataMember]
        public Guid DictationID { get; set; }

        [DataMember]
        public string ModuleType { get; set; }

        [DataMember]
        public int ModuleTypeID { get; set; }

        [DataMember]
        public DateTime DictationTimeStamp { get; set; }

        [DataMember]
        public int ManagedByUserId { get; set; }

        //M 3 May 2011
        [DataMember]
        public int ManagedByUserTypeId { get; set; }

     //   [DataMember]
        public int CreatedBy { get; set; }

    //    [DataMember]
        public DateTime CreatedAt { get; set; }

    //    [DataMember]
        public int ModifiedBy { get; set; }

    //    [DataMember]
        public DateTime ModifiedAt { get; set; }

        [DataMember]
        public bool IsRecordEditable { get; set; } 

    }
}
