using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Quivus.Model.Clinic;
using Quivus.Model.TypeDTOs;

namespace Quivus.Model.Common
{
    [DataContract]
    public class UserClinicRoleDTO : QuivusBaseDTO
    {
        public UserClinicRoleDTO()
        {
            //UserTypeRole = new TypeDTOs.UserTypeDTO();
        }

        [DataMember]
        public int UserTypeProfileID { get; set; }

        [DataMember]
        public bool IsProfileComplete { get; set; }

        [DataMember]
        public UserTypeDTO UserTypeRole { get; set; }
    }
}