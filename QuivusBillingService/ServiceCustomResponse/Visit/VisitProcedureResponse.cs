using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusBilling.IntegratedServices.VisitService;

namespace QuivusBilling.ServiceResponse.Visit
{
    [DataContract]
    public class VisitProcedureResponse : ServiceResponse
    {
        [DataMember]
        public List<QBProcedureDTO> VisitProcedureList { get; set; }
        [DataMember]
        public bool ResponseData { get; set; }
    }
}
