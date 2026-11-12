using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class DiseaseTypeDTO : QuivusBaseTypeDTO
    {


        [DataMember]
        public int FamilyHistoryTypeId { get; set; }

        [DataMember]
        public int DiseaseTypeID { get; set; }
        [DataMember]
        public string DiseaseTypeName { get; set; }
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string DiseaseTypeCode { get; set; }
        

        [DataMember]
        public bool IsSystemSpecified { get; set; }
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public int UserTypeID { get; set; }
        [DataMember]
        public int ClinicBranchID { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime CreatedAt { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public DateTime ModifiedAt { get; set; }
       

    }
}
