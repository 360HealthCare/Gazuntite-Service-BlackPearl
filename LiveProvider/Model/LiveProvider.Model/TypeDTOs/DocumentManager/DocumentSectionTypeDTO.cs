using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LiveProvider.Model.TypeDTOs.DocumentManager
{
    /// <summary>
    /// Author Muhammad Ismail
    /// Date July 04 2011
    /// Type DTO for mapping document sections
    /// </summary>
    [DataContract]
    public class DocumentSectionTypeDTO : LiveProviderBaseTypeDTO
    {
        [DataMember]
        public int SectionId { get; set; }
        [DataMember]
        public string SectionName { get; set; }
        [DataMember]
        public int ParentId { get; set; }
        [DataMember]
        public int OrderNo { get; set; }
        [DataMember]
        public List<DocumentSectionTypeDTO> ChildSectionType { get; set; }

    }
}
