using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.UserManagerService;



namespace QuivusManager.ServiceResponse.ServiceCustomResponse.PatientProfileCustomization
{
    /// <summary>
    /// Author Mansoor
    /// Date 03 Feb 2011
    /// This class is a custom wrapper of the Patient profile info for tagged windows
    /// </summary>

    [DataContract]
    public class PatientProfileDiagnosisResponse : ServiceResponse
    {
        [DataMember]
        public PatientProfileDiagnosisDTO ProfileDiagnosis { get; set; }
         //[DataMember]
         //public int PatientId { get; set; }

         //[DataMember]
         //public List<PatientDiagnosisInfoDTO> PatientDiagnosisInfoData { get; set; }
    }
}
