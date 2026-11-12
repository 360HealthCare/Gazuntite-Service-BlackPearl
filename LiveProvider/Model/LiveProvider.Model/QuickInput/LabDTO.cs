using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using LiveProvider.Model.TypeDTOs.ELabs.QuickInput;
using LiveProvider.Model.TypeDTOs;

namespace LiveProvider.Model.QuickInput
{
   
    [DataContract]
    public class LabDTO : LiveProviderBaseDTO
    {
        [DataMember]
        public LabTestPriorityTypeDTO LabTestPriorityType { get; set; }

        [DataMember]
        public LabTestSpecimenTypeDTO LabTestSpecimenType { get; set; }

        [DataMember]
        public LabTestOrderMethodTypeDTO LabTestOrderMethodType { get; set; }

      //  [DataMember]
      //  public LabNatureTypeDTO LabTestNatureType { get; set; }

         [DataMember]
        public LIONICLabTestTypeDTO LIONICLabTestType { get; set; }
        

        [DataMember]
        public LabTypeDTO LabType { get; set; }

        [DataMember]
        public String OrderDate { get; set; }// This field is updated from DateTime to String As per Requirment By Ismail July 14 2011

        [DataMember]
        public LabTestTypeDTO LabTestType { get; set; }

        [DataMember]
        public int PatientLabID { get; set; }

        [DataMember]
        public int VisitID { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Priority { get; set; }
        [DataMember]
        public ICDCodeDTO ICDCode  { get; set; }
        [DataMember]
        public string IsFasting { get; set; }
        [DataMember]
        public string Specimen { get; set; }
        [DataMember]
        public String CollectionDate { get; set; }// This field is updated from DateTime to String As per Requirment By Ismail July 14 2011
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public String Onset { get; set; }// This field is updated from DateTime to String As per Requirment By Ismail July 14 2011
       
        [DataMember]
        public int LabID { get; set; }
        [DataMember]
        public bool IsLabIn { get; set; }
        [DataMember]
        public string LabNote { get; set; }
        [DataMember]
        public string NoteSubject { get; set; }
        [DataMember]
        public Guid DictationID { get; set; }

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
        public int PatientID { get; set; }

        [DataMember]
        public int SignedByUserID { get; set; }

        [DataMember]
        public int ClinicBranchID { get; set; }

        [DataMember]
        public int StationID { get; set; }
    }
}
