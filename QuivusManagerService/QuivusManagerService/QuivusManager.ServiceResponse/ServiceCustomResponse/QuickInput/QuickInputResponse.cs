using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput
{
    [DataContract]
    public class QuickInputResponse : ServiceResponse
    {
        [DataMember]
        public QuickInputDTO ResponseData { get; set; }
    }
}
