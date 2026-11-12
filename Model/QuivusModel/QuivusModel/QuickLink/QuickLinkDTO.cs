using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Quivus.Model.QuickLink
{
    [DataContract]
    public class QuickLinkDTO : QuivusBaseDTO
    {
        [DataMember]
        public int UserQuickLinks { get; set; }

        [DataMember]
        public int QuickLinkTypeID { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public DateTime ModifiedAt { get; set; }

        [DataMember]
        public String label { get; set; }

        [DataMember]
        public String description { get; set; }

        [DataMember]
        public bool IsDefault { get; set; }

         [DataMember]
        public byte[] icon { get; set; }

         [DataMember]
         public int UserId { get; set; }

         [DataMember]
         public int ClinicBranchId { get; set; }

         [DataMember]
         public int ListOrder { get; set; }

    }
}
