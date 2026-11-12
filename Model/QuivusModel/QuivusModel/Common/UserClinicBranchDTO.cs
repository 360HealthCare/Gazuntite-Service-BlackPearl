using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Quivus.Model.Clinic;
using QuivusModel.Common;

namespace Quivus.Model.Common
{
    [DataContract]
    public class UserClinicBranchDTO : QuivusBaseDTO
    {
        public UserClinicBranchDTO()
        {
            //ClinicBranchUserRolesList = new List<UserClinicRoleDTO>();
            //UserClinicBranch =  new ClinicBranchDTO();
        }

        [DataMember]
        public ClinicBranchDTO ClinicBranch { get; set; }

        //[DataMember]
        //public List<UserClinicRoleDTO> ClinicBranchUserRolesList { get; set; }

        [DataMember]
        public UserClinicRoleDTO ClinicBranchUserRole { get; set; }

        [DataMember]
        public List<StationDTO> StationList { get; set; } 
    }
}