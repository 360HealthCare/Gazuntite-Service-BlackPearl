using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.QuickInput
{
    [DataContract]
    public class MedicationPrescribedDTO : QuivusBaseDTO
    {

        [DataMember]
        public int PatientMedicationPrescribedID { get; set; }

        [DataMember]
        public int MedicationTypeID { get; set; }

        [DataMember]
        public string MedicationPrescribedNote { get; set; }

         [DataMember]
        public string DrugName { get; set; }
        

        [DataMember]
        public int VisitID { get; set; }

        [DataMember]
        public string MedicationStrength { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string ApplicableStatus { get; set; }

        [DataMember]
        public string SIG { get; set; }

        [DataMember]
        public string Quantity { get; set; }

        [DataMember]
        public string NoOfRefills { get; set; }

        [DataMember]
        public string RecentRefills { get; set; }

        [DataMember]
        public int OrderDays { get; set; }

        [DataMember]
        public float Days { get; set; }

        [DataMember]
        public string IsChronic { get; set; }

        [DataMember]
        public string DosageForm { get; set; }

        [DataMember]
        public string Form { get; set; }

        [DataMember]
        public string Dose { get; set; }

        [DataMember]
        public int FrequencyTypeID { get; set; }

        [DataMember]
        public string Route { get; set; }

        [DataMember]
        public bool PRN { get; set; }

        [DataMember]
        public DateTime Onset { get; set; }
        [DataMember]
        public string Note { get; set; }
        [DataMember]
        public string NoteSubject { get; set; }

        [DataMember]
        public Guid DictationID { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime ModifiedAt { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public string RCOPIAID { get; set; }

        [DataMember]
        public DateTime VisitDate { get; set; }
        [DataMember]
        public bool IsRecordEditable { get; set; } 

     /* old  
      * [DataMember]
        public int PatientChronicMedicationId { get; set; }

        [DataMember]
        public int MedicationTypeID { get; set; }
      
        [DataMember]
        public DateTime DiscontinueDate { get; set; }

        [DataMember]
        public string ChronicMedicationTypeStatus { get; set; }

        [DataMember]
        public string Sig { get; set; }

        [DataMember]
        public string Quantity { get; set; }

        [DataMember]
        public string NumberOfRefills { get; set; }

        [DataMember]
        public DateTime OnSet { get; set; }

        [DataMember]
        public string RecentRefills { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public int VisitId { get; set; }

        [DataMember]
        public int OrderDays { get; set; }

        [DataMember]
        public float Days { get; set; }

        [DataMember]
        public bool IsChronic { get; set; }

        [DataMember]
        public string Status { get; set; }

         [DataMember]
        public string ApplicableStatus;

        [DataMember]
        public string DosageForm { get; set; }

        [DataMember]
        public string Form { get; set; }
      

        [DataMember]
        public string Dose { get; set; }

        [DataMember]
        public int FrequencyTypeID { get; set; }

        [DataMember]
        public string Route { get; set; }

        [DataMember]
        public bool PRN { get; set; }


        [DataMember]
        public DateTime VisitDate { get; set; }

        [DataMember]
        public string MedicationPrescribedNote { get; set; }

        [DataMember]
        public string MedicationStrength { get; set; }

        [DataMember]
        public Guid DictationID { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime ModifiedAt { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public string RCOPIAID { get; set; }
        */

        //[DataMember]
        //public DateTime DictationTimeStamp { get; set; }
    }
}
