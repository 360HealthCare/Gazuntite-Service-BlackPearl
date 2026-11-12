using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class UserTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int UserTypeId { get; set; }

        [DataMember]
        public string UserTypeName { get; set; }

        [DataMember]
        public bool IsProvider { get; set; }

        [DataMember]
        public bool IsClinicUser { get; set; }

        public int ClinicBranchID { get; set; }
    }
}
