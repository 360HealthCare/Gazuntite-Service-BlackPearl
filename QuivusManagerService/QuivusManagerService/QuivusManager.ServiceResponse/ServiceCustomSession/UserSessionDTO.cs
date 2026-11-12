using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusManager.ServiceResponse.ServiceCustomSession
{
    [DataContract]
    public class UserSessionDTO
    {
         [DataMember]
        public bool IsProvider { get; set; }

        [DataMember]
        public Guid SessionID { get; set; }

        public int UserID { get; set; }

        public int UserTypeID { get; set; }

        public int ClinicBranchID { get; set; }

        public bool IsActive { get; set; }

        [DataMember]
        public int TimeZoneId { get; set; }


        public DateTime LastActivityPerformedAt { get; set; }

        public DateTime CreatedAt { get; set; }

         [DataMember]
        public bool IsAlreadyLoggedIn { get; set; }

         [DataMember]
        public string ClientIPAdress { get; set; }

         [DataMember]
         public bool KickOutPreviousSession { get; set; }
    }
}
