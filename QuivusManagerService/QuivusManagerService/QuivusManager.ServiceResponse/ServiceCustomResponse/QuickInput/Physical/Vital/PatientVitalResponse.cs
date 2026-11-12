using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Vital
{
    /// <summary>
    /// Author Muammad Ismail
    /// Date 13 March 2011
    /// Class will Serve as a bean for managing patient vital insertion/updation :)
    /// </summary>
    /// 
    [DataContract]
    public class PatientVitalResponse : ServiceResponse
    {
        [DataMember]
        public int ResponseData { get; set; }

        [DataMember]
        public List<VitalDTO> ResponseList { get; set; }

              [DataMember]
        public List<RecentVitalDTO> ResponseRecentList { get; set; }
        
    }
}
