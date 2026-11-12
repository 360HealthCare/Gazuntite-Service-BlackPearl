using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.UserManagerService;


namespace QuivusManager.ServiceResponse.ServiceCustomResponse.StaticData
{
     [DataContract]
    public class ProfileStaticDataResponse : ServiceResponse
    {
         [DataMember]
         public List<ProfileStaticDataDTO> ResponseData { get; set; }
    }
}
