using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Medical
{
    /// <summary>
    /// Author Muammad Ismail
    /// Date 13 March 2011
    /// Class will Serve as a bean for managing patient medical history insertion/updation :)
    /// </summary>
    /// 
    [DataContract]
    public class PatientMedicalHistoryResponse : ServiceResponse
    {
        [DataMember]
        public int ResponseData { get; set; }

        [DataMember]
        public List<MedicalHistoryDTO> ResponseList { get; set; }
    }
}
