using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse
{
    /// <summary>
    /// Author Mansoor
    /// Date 17 Jan 2011
    /// This class is a custom wrapper of the servcie resposne
    /// </summary>

    [DataContract]
    public class EmailResponse : ServiceResponse
    {
        [DataMember]
        public string UserEmail { get; set; }
    }
}
