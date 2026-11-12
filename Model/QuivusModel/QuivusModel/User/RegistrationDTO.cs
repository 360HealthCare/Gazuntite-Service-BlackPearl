using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model;
using System.Runtime.Serialization;
using Quivus.Model.User;

namespace Quivus.Model.User
{
    [Obsolete]

    [DataContract]
    public class RegistrationDTO : QuivusBaseDTO
    {
        [DataMember]
        public UserDTO User { get; set; }

        [DataMember]
        public int ClinicBranhId { get; set; }
    }
}
