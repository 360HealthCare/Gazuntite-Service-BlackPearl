using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace QuivusModel.TypeDTOs.QuickInputTypeDTOs.Physical.Physicals
{
    [DataContract]
    public class PhysicalLevel3CategoryTypeDTO : QuivusBaseTypeDTO
    {

        [DataMember]
        public int PhysicalSubLevel3CategoryTypeID { get; set; }

        [DataMember]
        public int PhysicalSubCategoryTypeID { get; set; }

        [DataMember]
        public string ShortName { get; set; }

        [DataMember]
        public string SubLevel3CategoryName { get; set; }

        [DataMember]
        public bool IsMaleApplicable { get; set; }

        [DataMember]
        public bool IsFemaleApplicable { get; set; }

        [DataMember]
        public int UIIndex { get; set; }
    }
}
