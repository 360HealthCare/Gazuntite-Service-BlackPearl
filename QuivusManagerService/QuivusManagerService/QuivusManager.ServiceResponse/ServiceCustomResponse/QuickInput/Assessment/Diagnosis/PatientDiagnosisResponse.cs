using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Diagnosis
{
    /// <summary>
    /// Author Mansoor 
    /// Date 24 Mar 2011
    /// </summary>

    [DataContract]
    public class PatientDiagnosisResponse : ServiceResponse
    {
        [DataMember]
        public List<PatientDiagnosisDTO> PatientDiagnosisList { get; set; }
          [DataMember]
        public bool ResponseData { get; set; }
    }
}
