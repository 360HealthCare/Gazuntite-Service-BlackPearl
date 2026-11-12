using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.Visit
{
    [DataContract]
    public class VisistStakeHolderDTO:QuivusBaseDTO
    {
        [DataMember]
        public int ActionByUserID { get; set; }
        [DataMember]
        public int ActionByUserTypeID { get; set; }
    }
}
