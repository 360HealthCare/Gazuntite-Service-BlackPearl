using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.UserManagerService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickLinks
{
    /// <summary>
    /// Author Muhammad Ismail
    /// Date 14 Jan 2011
    /// This class is a custom wrapper of the quick link resposne
    /// </summary>
    /// 
    [DataContract]
    public class QuickLinkResponse : ServiceResponse
    {
        [DataMember]
        public List<QuickLinkDTO> ResponseData { get; set; }
    }
}