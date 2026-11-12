using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.QuickInput.ROS
{
    [DataContract]
    public class ROSDTO : QuivusBaseDTO
    {
        [DataMember]
        public int ROSSubLevel3CategoryTypeID { get; set; }

        [DataMember]
        public int PatientROSID { get; set; }
        [DataMember]
        public int VisitID { get; set; }
        [DataMember]
        public int PatientID { get; set; }
        [DataMember]
        public int ROSMainCategoryTypeID { get; set; }
        [DataMember]
        public int ROSSubCategoryTypeID { get; set; }

        [DataMember]
        public string MainCategoryName { get; set; }

        # region Added On Request Of front Binding Issue Not for use of Backend .Backend Is independent of these 2 fields 

        [DataMember]
        public string label { get; set; }   
        [DataMember]
        public List<ROSDTO> children { get; set; }

        #endregion

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
        public string ROSSubLevel3CategoryName { get; set; }
    }
}
