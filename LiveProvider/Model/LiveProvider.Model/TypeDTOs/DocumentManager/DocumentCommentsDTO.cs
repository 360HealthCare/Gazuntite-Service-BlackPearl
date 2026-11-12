using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LiveProvider.Model.TypeDTOs.DocumentManager
{
    [DataContract]
    public class DocumentCommentDTO : LiveProviderBaseDTO
    {
        [DataMember]
        public int CommentId { get; set; }

        [DataMember]
        public int DocumentId { get; set; }

        [DataMember]
        public int CommentedByUserId { get; set; }

        [DataMember]
        public int CommentedByUserTypeId { get; set; }

        [DataMember]
        public string Note { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime ModifiedAt { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }
    }
}
