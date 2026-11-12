using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Quivus.Model.TypeDTOs;
using Quivus.Model.Insurance;

namespace Quivus.Model.Common
{
    [DataContract]
    public class RegisterationStaticDataDTO : QuivusBaseDTO
    {
        #region Data Fields        

        #endregion


        #region Constructor

        public RegisterationStaticDataDTO()
        {
            
        }

        #endregion

        #region Properties
        

        [DataMember]
        public List<GenderTypeDTO> GenderTypes { get; set; }

        [DataMember]
        public List<EthinicityTypeDTO> EthinicityTypes { get; set; }

        [DataMember]
        public List<ImageTypeDTO> ImageTypes { get; set; }

        [DataMember]
        public List<LanguageTypeDTO> LanguageTypes { get; set; }

        [DataMember]
        public List<MaritalStatusTypeDTO> MaritalStatusTypes { get; set; }

        [DataMember]
        public List<OccupationTypeDTO> OccupationTypes { get; set; }

        [DataMember]
        public List<PrefixTypeDTO> PrefixTypes { get; set; }

        [DataMember]
        public List<RaceTypeDTO> RaceTypes { get; set; }

        [DataMember]
        public List<RelationshipTypeDTO> RelationshipTypes { get; set; }

        [DataMember]
        public List<InsuranceOrganizationDTO> InsuranceOrganizations { get; set; }

        [DataMember]
        public List<InsuranceTypeDTO> InsuranceTypes { get; set; }

        [DataMember]
        public List<UserTypeDTO> UserTypes { get; set; }

        [DataMember]
        public List<InsuranceNatureDTO> InsuranceNatureTypes { get; set; }      

        [DataMember]
        public List<StatusTypeDTO> AllStatusTypeList { get; set; }      

        [DataMember]
        public List<LanguageAbilityModeDTO> LanguageAbilityModeDTOList { get; set; }       

        [DataMember]
        public List<SmokingTypeDTO> SmokingTyeDTOList { get; set; }

        #endregion       

    }
}
