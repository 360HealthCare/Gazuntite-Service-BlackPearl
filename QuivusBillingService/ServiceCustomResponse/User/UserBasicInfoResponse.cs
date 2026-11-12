using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusBilling.IntegratedServices.UserManagerService;
using System.Runtime.Serialization;
using QuivusBilling.ServiceResponse;

namespace QuivusBilling.ServiceResponse.User
{
    [DataContract]
    public class UserBasicInfoResponse : ServiceResponse
    {
        [DataMember]
        public List<UserBasicInfoDTO> UserBasicInfoList { get; set; }
        [DataMember]
        public bool ResponseData { get; set; }
    }
}
