using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.PatientProfileCustomization
{
    /// <summary>
    /// This is a light weight DTO which contains the patient profile basic info, for tagged windows 
    /// </summary>

    [DataContract]
    public class PatientProfileBasicInfoDTO : QuivusBaseDTO
    { 
        [DataMember]        
        public string FirstName { get; set; }
      
        [DataMember]        
        public string LastName  { get; set; }
      
        [DataMember]        
        public string MiddleName  { get; set; }

        [DataMember]        
        public DateTime DateOfBirth { get; set; }
      
        [DataMember]        
        public string QuivusID { get; set; }

        [DataMember]
        public Guid RecGuid { get; set; }
    }
}