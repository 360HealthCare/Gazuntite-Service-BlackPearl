using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LiveProvider.Model
{
    /// <summary>
    /// Author Muhammad Ismail
    /// Date 04 July 2011
    /// Base DTO for  DTOs
    /// </summary>
   
    [DataContract]
    public class LiveProviderBaseDTO
    {
        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public bool IsDirty { get; set; }
    }
}
