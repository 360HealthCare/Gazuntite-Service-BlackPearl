using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Quivus.Model.TypeDTOs;

namespace Quivus.Model.User.Patient
{
    [DataContract]
    public class PatientAllergyDTO : QuivusBaseDTO
    {
        public PatientAllergyDTO()
        {   
        }

        [DataMember]
        public AllergyTypeDTO AllergyType { get; set; }

        [DataMember]
        public int PatientHistoryAllergyId { get; set; }

        [DataMember]
        public int PatientId { get; set; }

        [DataMember]
        public string IsActive { get; set; } // handled as bool

        //[DataMember]
        //public String AllergyTypeName { get; set; }

        [DataMember]
        public string Reaction { get; set; }

       

      

        [DataMember]
        public String OnSetDate { get; set; }
    }
}
