using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LiveProvider.Model.TypeDTOs.DocumentManager
{
    /// <summary>
    /// Author Muhammad Ismail
    /// Date 04 July 2011
    /// A Type DTo used to populate the static document information
    /// </summary>
    [DataContract]
    public class DocumentTypeDTO : LiveProviderBaseTypeDTO
    {
        [DataMember]
        public int DocumentTypeId { get; set; }
        [DataMember]
        public string DocumentTypeName { get; set; }

    }
}
