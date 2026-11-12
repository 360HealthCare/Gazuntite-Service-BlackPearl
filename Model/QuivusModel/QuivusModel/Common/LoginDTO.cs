using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Quivus.Model;
using Quivus.Model.TypeDTOs;
using Quivus.Model.Image;
//M using UserManagerService.Model.AddressBook;
using System.ComponentModel;

namespace Quivus.Model.Common
{
    /// <summary>
    /// 
    /// </summary>

    [DataContract]
    public class LoginDTO : QuivusBaseDTO
    {
        public LoginDTO()
        {
            AllUserTypes = null;
            UserClinicBranchesList = null;
            //AccountStatus = new StatusTypeDTO();
        }

        [DataMember]
        public bool IsPatient { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string UserEmail { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string Suffix { get; set; }

        [DataMember]
        public string Prefix { get; set; }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        public string SSN { get; set; }

        [DataMember]
        public ImageDTO Image { get; set; }

        //[DataMember]
        //private StatusTypeDTO AccountStatus { get; set; }

        //[DataMember]
        //private DateTime CreatedDate { get; set; }

        [DataMember]
        public List<UserTypeDTO> AllUserTypes { get; set; }

        [DataMember]
        public List<UserClinicBranchDTO> UserClinicBranchesList { get; set; }
    }
}
