using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.FollowUp
{
     [DataContract]
    public class  FollowupLabsResponse: ServiceResponse
    {
        [DataMember]
        public List<FollowupLabsDTO> ResponseList { get; set; }
    }
}
