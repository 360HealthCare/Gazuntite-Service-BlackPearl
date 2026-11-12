using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EprescriptionService.Common.DTOs
{
    [DataContract]
    public class PatientMedicationsInfoDTO
    {
        [DataMember] 
        public List<MedicationInfoDTO> MedicationInfoDtoList;
        [DataMember]
        public String PatientUserID { get; set; }
        [DataMember]
        public String PatientRcopiaID { get; set; }

        [DataMember]
        public String LastUpdatedDate { get; set; }

        [DataMember]
        public String PracticeName { get; set; }
         [DataMember]
         public String Status { get; set; }

    }
}
