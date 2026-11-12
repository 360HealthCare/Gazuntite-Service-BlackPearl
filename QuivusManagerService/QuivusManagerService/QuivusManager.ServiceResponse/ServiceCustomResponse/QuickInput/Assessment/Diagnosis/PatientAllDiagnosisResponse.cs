using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.VisitService;
using System.Runtime.Serialization;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Diagnosis
{
    /// <summary>
    /// Class: PatientAllDiagnosisResponse
    /// Description: this repsonse will be used for all patient diagnosis for all three types acute , resolved and chronic
    /// </summary>
    public class PatientAllDiagnosisResponse:ServiceResponse
    {
        [DataMember]
        public List<PatientDiagnosisDTO> PatientAcuteDiagnosisList { get; set; }
        [DataMember]
        public List<PatientDiagnosisDTO> PatientResolvedDiagnosisList { get; set; }
        [DataMember]
        public List<PatientDiagnosisDTO> PatientChronicDiagnosisList { get; set; }
        [DataMember]
        public bool ResponseData { get; set; }
    }
}
