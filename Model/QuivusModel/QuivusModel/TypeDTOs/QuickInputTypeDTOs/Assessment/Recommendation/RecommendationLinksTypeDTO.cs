using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace QuivusModel.TypeDTOs.QuickInputTypeDTOs.Assessment.Recommendation
{

    [DataContract]
    public class RecommendationLinksTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int RecommendationLinksTypeID { get; set; }

        [DataMember]
        public string Link { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public bool IsSystemSpecified { get; set; }

        [DataMember]
        public int ClinicBranchID { get; set; }

        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public int UserTypeID { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public DateTime ModifiedAt { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

     

    }
}
