using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.QuickInput.Medication
{
    [DataContract]
    public class MedicationDTO : QuivusBaseDTO
    {
       

        [DataMember]
        public int PatientMedicationID { get; set; }

         [DataMember]
        public int PatientId { get; set; }
        
        [DataMember]
        public string RCOPIAID { get; set; }

        [DataMember]
        public string NDCID { get; set; }

        [DataMember]
        public string FirstDataBankMedID { get; set; }

        [DataMember]
        public string BrandName { get; set; }

        [DataMember]
        public string GenericName { get; set; }

        [DataMember]
        public string Form { get; set; }
        [DataMember]
        public string Strength { get; set; }

        [DataMember]
        public string Action { get; set; }

        [DataMember]
        public string Dose { get; set; }

        [DataMember]
        public string DoseUnit { get; set; }

        [DataMember]
        public string Route { get; set; }

        [DataMember]
        public string DoseTiming { get; set; }

        [DataMember]
        public string DoseOther { get; set; }

        [DataMember]
        public string Duration { get; set; }

        [DataMember]
        public string Quantity { get; set; }

        [DataMember]
        public string QuantityUnit { get; set; }

        [DataMember]
        public string Refills { get; set; }

        [DataMember]
        public bool IsSubstitutionPermitted { get; set; }

        [DataMember]
        public string OtherNotes { get; set; }

        [DataMember]
        public string PatientNotes { get; set; }

        [DataMember]
        public string Comments { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public DateTime ModifiedAt { get; set; }

         [DataMember]
        public String NoteSubject{ get; set; }
        [DataMember]
        public int CreatedBy{ get; set; }
        [DataMember]
        public int ModifiedBy{ get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public DateTime VisitDate { get; set; }

        [DataMember]
        public string Signature { get; set; }

        [DataMember]
        public string VisitId { get; set; }
        [DataMember]
        public bool IsRecordEditable { get; set; }

        string _IsActive = "true";
        [DataMember]
        public string IsActive {
            get { return _IsActive; }
            set { _IsActive = value; }
        } // handled as default true
    }
}
