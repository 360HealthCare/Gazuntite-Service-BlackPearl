using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Social
{
    [DataContract]
    public class SocialMaritalStatusTypeResponse : ServiceResponse
    {
        [DataMember]
        public List<SocialMaritalStatusTypeDTO> SocialHistoryMaritalList { get; set; }
    }
}
