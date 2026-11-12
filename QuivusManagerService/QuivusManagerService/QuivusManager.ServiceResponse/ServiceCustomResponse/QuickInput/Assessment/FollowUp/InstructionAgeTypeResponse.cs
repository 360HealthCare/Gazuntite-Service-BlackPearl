using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.VisitService;
using System.Runtime.Serialization;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.FollowUp
{
    [DataContract]
    public class InstructionAgeTypeResponse : ServiceResponse
    {
        [DataMember]
        public List<InstructionAgeTypeDTO> InstructionAgeTypeList { get; set; }
    }

}
