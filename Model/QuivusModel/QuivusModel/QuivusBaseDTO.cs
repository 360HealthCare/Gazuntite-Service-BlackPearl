using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model
{
    /// <summary>
    /// Author Mansoor
    /// Date 17 Jan 2011
    /// </summary>

    [DataContract]
    public class QuivusBaseDTO
    {
        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public bool IsDirty { get; set; }

        //[DataMember]
        //public int CreatedBy { get; set; }

        //[DataMember]
        //public DateTime CreatedAt { get; set; }

        //[DataMember]
        //public int UpdatedBy { get; set; }

        //[DataMember]
        //public DateTime UpdatedAt { get; set; }

        

      

    }
}
