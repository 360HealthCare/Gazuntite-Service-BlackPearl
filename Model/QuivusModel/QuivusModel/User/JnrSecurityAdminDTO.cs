using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace QuivusModel.User
{
    [DataContract]
    public class JnrSecurityAdminDTO : QuivusBaseDTO
    {

        [DataMember]
        public int JrSecurityAdminId { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public Guid RecGuid { get; set; }
        [DataMember]
        public bool IsProfileComplete { get; set; }


        [DataMember]
        public DateTime ModifiedDate { get; set; }




        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public bool IsLocked { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

    }
}
