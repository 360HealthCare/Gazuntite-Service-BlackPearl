using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.QuickInput.Prescription
{
    [DataContract]
    public class PrescriptionDTO : QuivusBaseDTO
    {

        [DataMember]
        public int PatientPrescriptionID { get; set; }

        [DataMember]
        public int PatientID { get; set; }

        [DataMember]
        public int VisitID { get; set; }

        [DataMember]
        public bool IsVoided { get; set; }

        [DataMember]
        public bool IsDenied { get; set; }

        [DataMember]
        public string RCOPIAID { get; set; }


        [DataMember]
        public string RxReferenceNo { get; set; }

        [DataMember]
        public DateTime StopDate { get; set; }

        [DataMember]
        public DateTime CompletedDate { get; set; }

        [DataMember]
        public int PreparerUserID { get; set; }


        [DataMember]
        public int PreparerUserTypeID { get; set; }

        [DataMember]
        public int ProviderUserID { get; set; }


        [DataMember]
        public int ProviderUserTypeID { get; set; }

        [DataMember]
        public string CompletionAction { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }


        [DataMember]
        public DateTime ModifiedAt { get; set; }
    }
}
