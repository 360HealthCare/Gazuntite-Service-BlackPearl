using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace QuivusSeciruty.ServiceResponce.ResponceDTO
{

     [DataContract]
    public class UserTypeResponce : BaseResponse
    {

         [DataMember]
         public List<ModuleTypeDTO> ModuleTypeData { get; set; }

    }
}
