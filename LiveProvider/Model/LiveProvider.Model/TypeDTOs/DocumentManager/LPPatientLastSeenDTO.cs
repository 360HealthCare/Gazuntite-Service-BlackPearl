using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LiveProvider.Model.TypeDTOs.DocumentManager
{
    [DataContract]
    public class LPPatientLastSeenDTO
    {
        [DataMember]
        public int LastSeenByUserID { get; set; }

        [DataMember]
        public string LastSeenDate { get; set; }
    }
}
