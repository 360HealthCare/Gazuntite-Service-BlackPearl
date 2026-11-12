using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.UserManagerService;


namespace QuivusManager.ServiceResponse.ServiceCustomResponse.Search
{
    public class ContactSearchResponse : ServiceResponse
    {
        [DataMember]
        public List<ContactSearchDTO> ResponseData { get; set; }
    }
}
