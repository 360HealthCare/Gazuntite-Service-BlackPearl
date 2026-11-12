using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model.TypeDTOs;
using Quivus.Model.Image;

namespace Quivus.Model.User
{
    [DataContract]
    public class BaseUserDTO : QuivusBaseDTO
    {
         [DataMember]
        public bool IsActive { get; set; }
         [DataMember]

         public bool IsLockedOut { get; set; }
         [DataMember]

         public string TextPassword { get; set; }
        //Mansoor
        //Added on 8 Feb 2011
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public PrefixTypeDTO Prefix { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string MiddleName { get; set; }
        [DataMember]
        public string Suffix { get; set; }
        [DataMember]
        public string UserEmail { get; set; }
        [DataMember]
        public string UserPassword { get; set; }
        [DataMember]
        public GenderTypeDTO Gender { get; set; }
        [DataMember]
        public string SSN { get; set; }
        [DataMember]
        public StatusTypeDTO AccountStatus { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public string LicenseNumber { get; set; }
        [DataMember]
        public string FederalTaxId { get; set; }
        [DataMember]
        public string EIN { get; set; }

         [DataMember]
        public string QuivusUserID { get; set; }

      
        [DataMember]
        public int ClinicBranhId { get; set; }

        [DataMember]
        public UserTypeDTO ActiveUserType { get; set; }

        [DataMember]
        public ImageDTO Image { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }
        [DataMember]
        public DateTime ModifiedAt { get; set; }
         [DataMember]
        public string UserName { get; set; }
    }
}
