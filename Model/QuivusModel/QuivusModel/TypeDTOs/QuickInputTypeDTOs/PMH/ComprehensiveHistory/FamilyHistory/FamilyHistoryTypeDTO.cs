using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.FamilyHistory
{
     [DataContract]
    public class FamilyHistoryTypeDTO : QuivusBaseTypeDTO
    {
         [DataMember]
         public int FamilyHistoryTypeID { get; set; }

         [DataMember]
         public string  FamilyHistoryTypeName { get; set; }

         [DataMember]
         public string Description { get; set; }

         //[DataMember]
         //public string PresentICDCode { get; set; }

         //[DataMember]
         //public string PresentICDCodeDescription { get; set; }

         //[DataMember]
         //public string PastICDCode { get; set; }

         //  [DataMember]
         //public string PastICDCodeDescription { get; set; }
        
    }
}
