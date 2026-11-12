using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model;
using System.Runtime.Serialization;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Physicals
{
    [DataContract]
    public class PhysicalSubCategoryTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int PhysicalSubCategoryTypeID { get; set; }

        [DataMember]
        public int PhysicalMainCategoryTypeID { get; set; }

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
