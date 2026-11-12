using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusSecurity.IntegratedService.UserManagerService;


namespace ServiceResponce.ResponceDTO.Static
{
     [DataContract]
    public class UserTypeResponce:BaseResponce
    {
         [DataMember]
         public List<UserTypeDTO> UserTypeDTOList { get; set; }
    }
}
