using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.ROS
{
    [DataContract]
    public class ROSMainCategoryTypeDTO : QuivusBaseTypeDTO
    {

        [DataMember]
        public int ROSMainCategoryTypeID { get; set; }

        [DataMember]
        public string MainCategoryName { get; set; }

        [DataMember]
        public string label { get; set; }

        [DataMember]
        public string ShortName { get; set; }
        [DataMember]
        public bool IsMaleApplicable { get; set; }
        [DataMember]
        public bool IsFemaleApplicable { get; set; }

        [DataMember]
        public int UIIndex { get; set; }

        [DataMember]
        public List<ROSSubCategoryTypeDTO> children { get; set; }

        //[DataMember]
        //public List<ROSSubCategoryTypeDTO> GeneralSubList { get; set; }

        //[DataMember]
        //public List<ROSSubCategoryTypeDTO> MaleSubList { get; set; }

        //[DataMember]
        //public List<ROSSubCategoryTypeDTO> FeMaleSubList { get; set; }
    }
}
