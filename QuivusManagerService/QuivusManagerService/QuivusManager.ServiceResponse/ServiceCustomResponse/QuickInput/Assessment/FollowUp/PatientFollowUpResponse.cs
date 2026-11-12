using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.FollowUp
{
    /// <summary>
    /// Author Mansoor 
    /// Date 25 Mar 2011
    /// </summary>

     [DataContract]
    public class PatientFollowUpResponse : ServiceResponse
    {
          [DataMember]
         public bool ResponseData { get; set; }
        [DataMember]
         public PatientFollowUpDTO FollowUp { get; set; }

        [DataMember]
        public List<PatientFollowUpDTO> ResponseList { get; set; }
    }
}
