using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace QuivusModel.TypeDTOs
{
    [DataContract]
    public class SearchEngineTypeDTO
    {

        [DataMember]
        public int SearchEngineTypeID { get; set; }

        [DataMember]
        public string SearchEngineName { get; set; }


        [DataMember]
        public string URL { get; set; }


        [DataMember]
        public byte[] Icon { get; set; }


        [DataMember]
        public byte[] Thumbnail { get; set; }


        [DataMember]
        public string Description { get; set; }


        [DataMember]
        public bool IsActive { get; set; }


        [DataMember]
        public bool IsDeleted { get; set; }


        [DataMember]
        public bool IsSystemGenerated { get; set; }

    }
}
