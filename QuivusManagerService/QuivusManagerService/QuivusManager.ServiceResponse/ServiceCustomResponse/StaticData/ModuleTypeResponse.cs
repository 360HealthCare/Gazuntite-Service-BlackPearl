using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;


namespace QuivusManager.ServiceResponse.ServiceCustomResponse.Notes
{
    /// <summary>
    /// Author Mansoor
    /// Date 22 Feb 2011
    /// This class is a custom wrapper of the servcie resposne
    /// </summary>
    
    [DataContract]
    public class ModuleTypeResponse : ServiceResponse
    {   
        [DataMember]
        public List<ModuleTypeDTO> ModuleTypeData { get; set; }
    }
}
