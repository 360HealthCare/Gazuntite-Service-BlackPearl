using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.User
{
    [DataContract]
    public class UserBasicInfoDTO : QuivusBaseDTO
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int PrefixTypeID { get; set; }
        [DataMember]
        public int GenderTypeID { get; set; }
        [DataMember]
        public String PrefixName { get; set; }
        [DataMember]
        public String GenderName { get; set; }
        [DataMember]
        public String FirstName { get; set; }
        [DataMember]
        public String LastName { get; set; }
        [DataMember]
        public String MiddleName { get; set; }
        [DataMember]
        public String Suffix { get; set; }
        [DataMember]
        public String UserEmail { get; set; }
        [DataMember]
        public String SSN { get; set; }

         [DataMember]
        public string UserName { get; set; }
    }
}
