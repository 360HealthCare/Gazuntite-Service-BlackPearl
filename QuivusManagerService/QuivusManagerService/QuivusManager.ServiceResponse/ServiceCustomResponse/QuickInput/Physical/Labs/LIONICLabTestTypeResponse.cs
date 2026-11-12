using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Labs
{
    [DataContract]
    public class LIONICLabTestTypeResponse : ServiceResponse
    {
        [DataMember]
        public List<LIONICLabTestTypeDTO> ResponseList { get; set; }
    }
}
