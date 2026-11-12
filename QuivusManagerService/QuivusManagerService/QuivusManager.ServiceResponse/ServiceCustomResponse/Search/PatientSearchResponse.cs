using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.UserManagerService;


namespace QuivusManager.ServiceResponse.ServiceCustomResponse.Search
{
    /// <summary>
    /// Author Mansoor
    /// Date 27 Jan 2011
    /// Custom response to contain the data of patient search use case
    /// </summary>
    
    public class PatientSearchResponse : ServiceResponse
    {
        [DataMember]
        public PatientSearchResultDTO PatientSearchResultResponseData { get; set; }
    }
}
