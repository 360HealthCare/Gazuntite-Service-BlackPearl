using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.User
{
    /// <summary>
    /// Author Mansoor
    /// Date 21 Jan 2011
    /// </summary>

    [DataContract]
    public class PatientSaveResponse : ServiceResponse
    {
        [DataMember]
        public int PatientId { get; set; }
    }
}
