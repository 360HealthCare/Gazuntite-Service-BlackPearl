using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusBilling.IntegratedServices.VisitService;

namespace QuivusBilling.ServiceResponse.Visit
{
    [DataContract]
    public class VisitResponse : ServiceResponse
    {
        [DataMember]
        public QBVisitDTO VisitData { get; set; }
    }
}
