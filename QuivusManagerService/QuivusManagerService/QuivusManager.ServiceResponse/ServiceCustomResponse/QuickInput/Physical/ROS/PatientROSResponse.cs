using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.ROS
{
    [DataContract]
    public class PatientROSResponse : ServiceResponse
    {
        [DataMember]
        public List<ROSDTO> ResponseList { get; set; }
    }
}
