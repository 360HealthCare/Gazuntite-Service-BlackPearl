using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.Billing.User
{
    /// <summary>
    /// Auhtor: Shahid Munir
    /// Date:   01/06/2012
    /// </summary>
    [DataContract]
    public class PatientQuivusIDDTO
    {
        [DataMember]
        public int PatientId { get; set; }

        [DataMember]
        public string QuivusId { get; set; }

        [DataMember]
        public int PatientUserId { get; set; }
    }
}
