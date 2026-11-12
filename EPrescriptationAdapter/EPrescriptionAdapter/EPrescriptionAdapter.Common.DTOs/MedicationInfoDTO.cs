using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EprescriptionService.Common.DTOs
{
    [DataContract]
    public class MedicationInfoDTO
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
        public string Status { get; set; }

        [DataMember]
        public DateTime VisitDate { get; set; }

        [DataMember]
        public DateTime LastModifiedDate { get; set; }

        [DataMember]
        public String ModifiedBy { get; set; }


    }
}
