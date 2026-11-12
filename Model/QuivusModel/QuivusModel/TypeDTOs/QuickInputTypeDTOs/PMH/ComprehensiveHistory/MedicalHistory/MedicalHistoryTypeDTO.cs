using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;
 

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.MedicalHistory
{
    /// <summary>
    /// Author Muhammad Ismail 
    /// Date 11 March 2011
    /// Simple DTO to load medical history types i.e Static Info From DB
    /// </summary>
    [DataContract]
    public class MedicalHistoryTypeDTO : QuivusBaseTypeDTO
    {

        [DataMember]
        public int MedicalHistoryTypeID { get; set; }

        [DataMember]
        public string MedicalHistoryTypeName { get; set; }
    }
}
