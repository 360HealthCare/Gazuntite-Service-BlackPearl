using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace QuivusModel.TypeDTOs
{
    [DataContract]
    public class QuickLinkTypeDTO : QuivusBaseTypeDTO
    {



        [DataMember]
        public int QuickLinkTypeID { get; set; }


        [DataMember]
        public string QuickLinkName { get; set; }


        [DataMember]
        public byte[] Icon { get; set; }


        [DataMember]
        public string Description { get; set; }


        [DataMember]
        public bool IsDefault { get; set; }


        [DataMember]
        public bool IsActive { get; set; }




    }
}
