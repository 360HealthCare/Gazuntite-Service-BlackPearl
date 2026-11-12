using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Procedure
{
    [DataContract]
    public class PatientProcedureResponse : ServiceResponse
    {
        [DataMember]
        public List<ProcedureDTO> ResponseList { get; set; }
    }
}
