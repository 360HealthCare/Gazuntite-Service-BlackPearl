using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.QuickInput.Physical
{
    [DataContract]
    public class PhysicalsDTO : QuivusBaseDTO
    {

        [DataMember]
        public int PatientPhysicalID { get; set; }
        [DataMember]
        public int VisitID { get; set; }
        [DataMember]
        public int PatientID { get; set; }

        [DataMember]
        public int PhysicalMainCategoryTypeID { get; set; }
        [DataMember]
        public int PhysicalSubCategoryTypeID { get; set; }

        # region Added On Request Of front Binding Issue Not for user of Backend .Backend Is independent of these 2 fields

        [DataMember]
        public string label { get; set; }
        [DataMember]
        public List<PhysicalsDTO> children { get; set; }

        #endregion

        [DataMember]
        public string MainCategoryName { get; set; }
        [DataMember]
        public string SubCategoryName { get; set; }

        [DataMember]
        public string IsNormal { get; set; }
        [DataMember]
        public Guid DictationID { get; set; }

        [DataMember]
        public string Notes { get; set; }
        [DataMember]
        public string NoteSubject { get; set; }
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


        [DataMember]
        public int PhysicalSubLevel3CategoryTypeID { get; set; }

        [DataMember]
        public string SubLevel3CategoryName { get; set; }
    }
}
