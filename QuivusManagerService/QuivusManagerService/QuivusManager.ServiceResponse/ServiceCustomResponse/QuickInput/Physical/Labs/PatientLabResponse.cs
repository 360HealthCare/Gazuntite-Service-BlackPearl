using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Labs
{
    /// <summary>
    /// Author Muammad Ismail
    /// Date 13 March 2011
    /// Class will Serve as a bean for managing patient lab insertion/updation :)
    /// </summary>
     [DataContract]
    public class PatientLabResponse : ServiceResponse
    {
        [DataMember]
        public int ResponseData { get; set; }

        [DataMember]
        public List<LabDTO> ResponseList { get; set; }
    }
}
