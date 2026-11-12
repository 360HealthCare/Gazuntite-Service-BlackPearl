using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.VisitService;
using System.Runtime.Serialization;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Labs
{
    [DataContract]
    public class LabTestOrderMethodTypeResponse: ServiceResponse
    {
        [DataMember]
        public List<LabTestOrderMethodTypeDTO> ResponseList { get; set; }
    }
}
