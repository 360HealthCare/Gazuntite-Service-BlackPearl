using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;
using QuivusManager.IntegratedServies.UserManagerService;


namespace QuivusManager.ServiceResponse.ServiceCustomResponse.ICDCode
{   
    /// <summary>
    /// Author Mansoor
    /// Date 21 Jan 2011
    /// </summary>

    [DataContract]
    public class ICDCodeResponse : ServiceResponse
    {
        [DataMember]
        public List<ICDCodeSearchDTO> ResponseData { get; set; }

        [DataMember]
        public List<QuivusManager.IntegratedServies.VisitService.ICDCodeDTO> ResponseList { get; set; }
    }
}
