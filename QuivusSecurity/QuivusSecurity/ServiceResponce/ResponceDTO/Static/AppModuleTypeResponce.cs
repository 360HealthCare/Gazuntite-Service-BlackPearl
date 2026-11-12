using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusSecurity.IntegratedService.UserManagerService;
using QuivusModel.Security;


namespace ServiceResponce.ResponceDTO.Static
{
     [DataContract]
    public class AppModuleTypeResponce:BaseResponce
    {
         [DataMember]
         public List<AppModuleDTO> AppModuleDTOList { get; set; }
    }
}
