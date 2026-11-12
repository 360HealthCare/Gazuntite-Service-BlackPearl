using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.UserManagerService;


namespace QuivusManager.ServiceResponse.ServiceCustomResponse.PatientProfileCustomization
{
    [DataContract]
    public class PatientProfileInsuranceResponse : ServiceResponse
    {
        [DataMember]
        public PatientProfileInsuranceDTO ProfileInsuranceDTO { get; set; }
        //[DataMember]
        //public int PatientId { get; set; }

        //[DataMember]
        //public List<PatientInsuracneInfoDTO> PatientInsuranceInfoData { get; set; }
    }
}
