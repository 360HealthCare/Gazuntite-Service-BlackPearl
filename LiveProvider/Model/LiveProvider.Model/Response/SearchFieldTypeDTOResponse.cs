using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using LiveProvider.Model.TypeDTOs;

namespace LiveProvider.Model.Response
{  
    [DataContract]
    [XmlRoot(ElementName = "SerachTypes")]
    public class SearchFieldTypeDTOResponse
    {
         [DataMember]
        [XmlArrayAttribute("Seraches")]
        public List<SearchFieldTypeDTO> SearchTypeList { get; set; }

        public SearchFieldTypeDTOResponse()
        {
            if (SearchTypeList == null)
            {
                SearchTypeList = new List<SearchFieldTypeDTO>();
            }
        }
    }
}
