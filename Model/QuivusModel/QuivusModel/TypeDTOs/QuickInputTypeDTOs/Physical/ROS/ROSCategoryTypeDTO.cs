using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model;
using System.Runtime.Serialization;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.ROS
{
    [DataContract]
    public class ROSCategoryTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public List<ROSMainCategoryTypeDTO> GeneralMainList { get; set; }
        [DataMember]
        public List<ROSMainCategoryTypeDTO> MaleMainList { get; set; }
        [DataMember]
        public List<ROSMainCategoryTypeDTO> FemaleMailList { get; set; }
    }
}
