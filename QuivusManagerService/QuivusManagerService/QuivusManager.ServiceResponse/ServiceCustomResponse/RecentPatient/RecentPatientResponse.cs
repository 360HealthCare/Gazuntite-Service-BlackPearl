using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.UserManagerService;


namespace QuivusManager.ServiceResponse.ServiceCustomResponse.RecentPatient
{
    /// <summary>
    /// Author Mansoor
    /// Date 1 Feb 2011
    /// </summary>

    [DataContract]
    public class RecentPatientRespose : ServiceResponse
    {
        [DataMember]
        public List<RecentPatientDTO> MostRecentPatientsResponseData { get; set; }
    }
}
