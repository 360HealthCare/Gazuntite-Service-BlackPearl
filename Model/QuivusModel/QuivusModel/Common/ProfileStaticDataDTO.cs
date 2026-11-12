using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model;
using System.Runtime.Serialization;

namespace Quivus.Model.Common
{
    [DataContract]
    public class ProfileStaticDataDTO : QuivusBaseDTO
    {

        [DataMember]
        public int UserProfileCategoryID { get; set; }
        [DataMember]
        public String MainCategoryName { get; set; }
        [DataMember]
        public String MainCategoryDisplayText { get; set; }
        [DataMember]
        public int MainUserProfileCategoryID { get; set; }
        [DataMember]
        public String CategoryName { get; set; }
        [DataMember]
        public String CategoryDisplayText { get; set; }
    }
}
