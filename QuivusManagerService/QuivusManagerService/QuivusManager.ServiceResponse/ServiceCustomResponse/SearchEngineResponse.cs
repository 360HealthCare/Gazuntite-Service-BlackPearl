using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.UserManagerService;


namespace QuivusManager.ServiceResponse.ServiceCustomResponse
{
    /// <summary>
    /// Author Muhammad Ismail
    /// Date 14 Jan 2011
    /// This class is a custom wrapper of the search engine resposne
    /// </summary>
    [DataContract]
    public class SearchEngineResponse : ServiceResponse
    {
        [DataMember]
        public List<SearchEngineDTO> ResponseData { get; set; }

        [DataMember]
        public int ResponseResult { get; set; }
    }
}