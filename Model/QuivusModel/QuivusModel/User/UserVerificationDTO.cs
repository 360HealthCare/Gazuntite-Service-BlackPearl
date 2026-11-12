using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model;
using System.Runtime.Serialization;

namespace Quivus.Model.User
{
    [DataContract]
    public class UserVerificationDTO:QuivusBaseDTO
    {
        [DataMember]
        public bool IsValidUser { get; set; }

        [DataMember]
        public String ErrorMessage { get; set; }
    }
}
