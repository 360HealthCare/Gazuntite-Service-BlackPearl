using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp
{
    public class ServiceTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int FollowupServiceTypeId { get; set; }

        [DataMember]
        public int ClinicBranchId { get; set; }

        [DataMember]
        public string FollowupServiceTypeName { get; set; }

        //Sami
        //added for Service Type new Auto complete change
        [DataMember]
        public string FollowupService { get; set; }
    }
}
