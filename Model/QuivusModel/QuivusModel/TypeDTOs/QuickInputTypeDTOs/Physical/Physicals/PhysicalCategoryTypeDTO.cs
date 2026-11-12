using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace QuivusModel.TypeDTOs.QuickInputTypeDTOs.Physical.Physicals
{
    [DataContract]
    public class PhysicalCategoryTypeDTO : QuivusBaseTypeDTO
    {

        [DataMember]
        public int PhysicalCategoryTypeID { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

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
        public int ParentId { get; set; }

    }
}
