using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Recommendation
{
    /// <summary>
    /// Author Mansoor 
    /// Date 22 Mar 2011
    /// </summary>
    /// 
    [DataContract]
    public class PatientRecommendationResponse : ServiceResponse
    {
        [DataMember]
        public List<PatientRecommendationDTO> RecommendationList { get; set; }
    }
}
