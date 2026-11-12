using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.UserManagerService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.Image
{
    [DataContract]
    public class ImageTypeResponse : ServiceResponse
    {
        [DataMember]
        public List<ImageTypeDTO> ResponseData { get; set; }
    }
}
