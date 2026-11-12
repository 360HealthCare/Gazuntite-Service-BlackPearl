using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.UserManagerService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Allergy
{
    /// <summary>
    /// Author Mansoor
    /// Date 21 Jan 2011
    /// </summary>

    [DataContract]
    public class AllergyTypeResponse : ServiceResponse
    {
        [DataMember]
        public List<AllergySearchDTO> ResponseData { get; set; }
    }
}
