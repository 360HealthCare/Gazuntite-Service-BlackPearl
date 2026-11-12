using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.QuickInput
{
    [DataContract]
    public class AllergyDTO : QuivusBaseDTO
    {
        //[DataMember]
        //public AllergyType AllergyType
        //{
        //    get { return _allergytype; }
        //    set { _allergytype = value; }
        //}
              

        [DataMember]
        public int PatientAllergyId { get; set; }

        [DataMember]
        public string Reaction { get; set; }

        [DataMember]
        public String OnSet { get; set; } // This field is updated from DateTime to String As per Requirment Ismail July 14 2011

        [DataMember]
        public int VisitId { get; set; }

        [DataMember]
        public string Note { get; set; }
        
        [DataMember]
        public string NoteSubject { get; set; }

        //[DataMember]
        //public string IsOld { get; set; }

        [DataMember]
        public DateTime VisitDate { get; set; }

        [DataMember]
        public Guid DictationID { get; set; }

        [DataMember]
        public string AllergyName { get; set; }

        [DataMember]
        public int AllergyTypeID { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime ModifiedAt { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public int RCOPIAID { get; set; }
        [DataMember]
        public string Drug { get; set; }

        [DataMember]
        public bool IsRecordEditable { get; set; }


        string _IsActive = "true";
        [DataMember]
        public string IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        } // handled as default true


        //[DataMember]
        //public DateTime DictationTimeStamp { get; set; }        

    }
}
