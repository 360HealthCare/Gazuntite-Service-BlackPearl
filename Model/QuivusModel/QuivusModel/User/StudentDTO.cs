using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model.TypeDTOs;
using Quivus.Model;

namespace QuivusModel.User
{
    [DataContract]
    public class StudentDTO : QuivusBaseDTO
    {

        public StudentDTO()
        {
            SpecialityType = new SpecialityTypeDTO();
        }

        [DataMember]
        public int StudentIdSeq { get; set; }
        [DataMember]
        public int USER_ID { get; set; }


        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        
        [DataMember]
        public SpecialityTypeDTO SpecialityType { get; set; }

        [DataMember]
        public DateTime CREATE_DT { get; set; }

        [DataMember]
        public DateTime ModifedAt { get; set; }

        

        [DataMember]
        public Guid RecGuid { get; set; }

        [DataMember]
        public string WorkingDays { get; set; }
        [DataMember]
        public string WorkStartTime { get; set; }
        [DataMember]
        public string WorkEndTime { get; set; }
        [DataMember]
        public bool IsProfileComplete { get; set; }


      


        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public bool IsLocked { get; set; }
    }
}
