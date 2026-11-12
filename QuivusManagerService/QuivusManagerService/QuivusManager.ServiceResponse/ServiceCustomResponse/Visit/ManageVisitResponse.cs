using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.Visit
{
    [DataContract]
    public class ManageVisitResponse : ServiceResponse
    {
        [DataMember]
        public bool ResultStatus { get; set; }
    }
}
