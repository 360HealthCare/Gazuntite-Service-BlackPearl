using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.FollowUp
{
    
    [DataContract]
    public class PatientFollowupInstructionResponse : ServiceResponse
    {
        [DataMember]
        public List<PatientFollowupInstructionDTO> PatientFollowupInstructionList { get; set; }
    }
}
