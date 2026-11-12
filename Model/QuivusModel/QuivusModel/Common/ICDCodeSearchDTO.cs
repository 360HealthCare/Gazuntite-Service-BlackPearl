using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Quivus.Model;

namespace Quivus.Model.Common
{
    [DataContract]
    public class ICDCodeSearchDTO : QuivusBaseDTO
    {
        [DataMember]
        public int IcdCodeIdSeq { get; set; }

        [DataMember]
        public string IcdCodeName { get; set; }

        //[DataMember]
        //public DateTime CreatedDate { get; set; }

        [DataMember]
        public string IcdCodeDescription { get; set; }

        [DataMember]
        public string IsChronic { get; set; }
    }
}
