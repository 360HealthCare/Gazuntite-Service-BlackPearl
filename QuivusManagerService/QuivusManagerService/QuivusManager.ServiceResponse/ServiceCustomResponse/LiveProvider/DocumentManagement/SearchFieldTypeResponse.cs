using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.DocumentManagerService;
using System.Runtime.Serialization;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.LiveProvider.DocumentManagement
{
    [DataContract]
    public class SearchFieldTypeResponse : ServiceResponse
    {
        [DataMember]
        public List<SearchFieldTypeDTO> ResponseData { get; set; }
    }
}
