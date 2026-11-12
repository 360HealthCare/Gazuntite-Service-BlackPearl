using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.Visit
{
    [DataContract]
    public class VisistStakeHolderResponse : ServiceResponse
    {
        [DataMember]
        public List<VisistStakeHolderDTO> ResponseList { get; set; }
    }
}
