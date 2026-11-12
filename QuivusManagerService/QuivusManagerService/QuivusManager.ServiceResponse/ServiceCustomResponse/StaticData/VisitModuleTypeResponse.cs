using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.StaticData
{
    [DataContract]
    public class VisitModuleTypeResponse:ServiceResponse
    {
        [DataMember]
        public List<ModuleTypeDTO> ModuleTypeData { get; set; }
    }
}
