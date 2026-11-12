using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LiveProvider.Model.TypeDTOs.DocumentManager
{
    [DataContract]
    public class SearchFieldTypeDTO : LiveProviderBaseTypeDTO
    {
        [DataMember]
        public int FieldID { get; set; }

        [DataMember]
        public string FieldName { get; set; }
    }
}
