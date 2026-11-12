using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusBilling.IntegratedServices.VisitService;


namespace QuivusBilling.ServiceResponse.Visit
{
    [DataContract]
    public class VisitDiagnosisResponse : ServiceResponse
    {
        [DataMember]
        public List<PatientDiagnosisDTO> VisitDiagnosisList { get; set; }
        [DataMember]
        public bool ResponseData { get; set; }
    }
}
