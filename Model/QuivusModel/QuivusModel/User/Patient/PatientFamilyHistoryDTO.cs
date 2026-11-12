using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Quivus.Model.TypeDTOs;

namespace Quivus.Model.User.Patient
{
    [DataContract]
    public class PatientFamilyHistoryDTO : QuivusBaseDTO
    {
        public PatientFamilyHistoryDTO()
        {            
        }

        [DataMember]
        public int PatientHistoryFamilyId { get; set; }

        [DataMember]
        public int PatientId { get; set; }

        [DataMember]
        public ICDCodeDTO ICDCodeType { get; set; }

        [DataMember]
        public string IsActive { get; set; } // handled as bool

        [DataMember]
        public RelationshipTypeDTO RelationshipType { get; set; }

        //[DataMember]
        //public bool? IsActiveInternal { get; set; }

      

      
    }
}
