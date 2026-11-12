using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model.TypeDTOs;

namespace Quivus.Model.User.Patient
{
    /// <summary>
    /// Author Mansoor
    /// Class Used In Registration Section
    /// </summary>
    [DataContract]
    public class PatientMedicalHistoryDTO : QuivusBaseDTO
    {
        public PatientMedicalHistoryDTO()
        {            
        }

        [DataMember]
        public int PatientHistoryMedicalId { get; set; }

        [DataMember]
        public int PatientId { get; set; }

        [DataMember]
        public string IsActive { get; set; } // handled as bool

        [DataMember]
        public ICDCodeDTO ICDCodeType { get; set; }

        //[DataMember]
        //public bool? IsActiveInternal { get; set; }

      

   
    }
}
