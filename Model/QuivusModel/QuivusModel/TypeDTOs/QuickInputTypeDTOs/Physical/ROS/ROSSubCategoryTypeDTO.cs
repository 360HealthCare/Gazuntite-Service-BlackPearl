using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model;
using System.Runtime.Serialization;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.ROS
{
    [DataContract]
    public class ROSSubCategoryTypeDTO : QuivusBaseTypeDTO
    {

        [DataMember]
        public int ROSSubCategoryTypeID { get; set; }
        [DataMember]
        public int ROSMainCategoryTypeID { get; set; }

        [DataMember]
        public string label { get; set; }
        [DataMember]
        public string SubCategoryName { get; set; }
        [DataMember]
        public string ShortName { get; set; }
        [DataMember]
        public bool IsMaleApplicable { get; set; }
        [DataMember]
        public bool IsFemaleApplicable { get; set; }

        [DataMember]
        public int UIIndex { get; set; }
    }
}
