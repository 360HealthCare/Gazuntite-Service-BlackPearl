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
    /// Class will Serve as a bean for Medical History Types :)
    /// </summary>
    /// 
    [DataContract]
    public class MedicalHistoryTypeResponse : ServiceResponse
    {
        [DataMember]
        public List<MedicalHistoryTypeDTO> MedicalHistoryList { get; set; }
    }
}
