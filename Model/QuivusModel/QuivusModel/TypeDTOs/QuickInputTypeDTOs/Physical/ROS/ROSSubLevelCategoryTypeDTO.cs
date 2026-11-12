using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.QuickInputTypeDTOs.Physical.ROS
{
    [DataContract]
    public class ROSSubLevelCategoryTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int ROSSubLevel3CategoryTypeID { get; set; }

        [DataMember]
        public int ROSSubCategoryTypeID { get; set; }
        [DataMember]
        public string SubLevel3CategoryName { get; set; }
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
