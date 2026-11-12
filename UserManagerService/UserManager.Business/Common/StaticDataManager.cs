using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

//M using Quivus.Business.BL.AddressBook.Impl;
using Quivus.Model.AddressBook;
using UserManagerService.DataAccess;
using Quivus.Utility.Factory;
//M using Quivus.Model.QuickInput;
using Quivus.Model.TypeDTOs;
using Quivus.Model.Insurance;
using Quivus.Utility.Logger;

//M using Quivus.Business.BL.AddressBook;

using Quivus.Model.Common;


using Quivus.Utility.CustomException;
 
using Quivus.Utility;

using UserManager.Business.Utility;
using QuivusModel.TypeDTOs;

namespace UserManagerService.Business.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class StaticDataManager
    {
        #region Data Fields

        private TypeListingDAO typeListingDAO;

        private static RegisterationStaticDataDTO _registerationStaticDataDTO;

        #endregion

        #region Constructor

        public StaticDataManager()
        {
            if (typeListingDAO == null)
                typeListingDAO = (TypeListingDAO)DAOFactory.GetDataAccessObject(typeof(TypeListingDAO));

            if (_registerationStaticDataDTO == null)
                _registerationStaticDataDTO = new RegisterationStaticDataDTO();
        }
        #endregion

        #region Methods

        /// <summary>
        /// To Get List of all status types
        /// </summary>
        /// <returns></returns>
        private List<StatusTypeDTO> ListAllStatusTypes()
        {
            List<StatusTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllStatusTypes");

                if (_registerationStaticDataDTO.AllStatusTypeList != null)
                {
                    list = _registerationStaticDataDTO.AllStatusTypeList;
                }
                else
                {                    
                    list = typeListingDAO.ListAllStatusTypesDTOs();
                    _registerationStaticDataDTO.AllStatusTypeList = list;
                }                
            }
            catch 
            {
               
                throw ;
            }
            return list;
        }

        private List<UserTypeDTO> ListAllUserTypeDTOs()
        {
            List<UserTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllStatusTypes");

                if (_registerationStaticDataDTO.UserTypes != null)
                {
                    list = _registerationStaticDataDTO.UserTypes;
                }
                else
                {                    
                    list = typeListingDAO.ListAllUserTypeDTOs();
                    _registerationStaticDataDTO.UserTypes = list;
                }
            }
            catch
            {
              
                throw ;
            }
            return list;
        }

        private List<PrefixTypeDTO> ListAllPrefixTypeDTOs()
        {
            List<PrefixTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllPrefixTypeDTOs");

                if (_registerationStaticDataDTO.PrefixTypes != null)
                {
                    list = _registerationStaticDataDTO.PrefixTypes;
                }
                else
                {
                    list = typeListingDAO.ListAllPrefixTypeDTOs();
                    _registerationStaticDataDTO.PrefixTypes = list;
                }
            }
            catch
            {
               
                throw ;
            }
            return list;
        }

        private List<GenderTypeDTO> ListAllGenderTypeDTOs()
        {
            List<GenderTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllGenderTypeDTOs");

                if (_registerationStaticDataDTO.GenderTypes != null)
                {
                    list = _registerationStaticDataDTO.GenderTypes;
                }
                else
                {
                    list = typeListingDAO.ListAllGenderTypeDTOs();
                    _registerationStaticDataDTO.GenderTypes = list;
                }
            }
            catch
            {
                
                throw ;
            }
            return list;
        }

        private List<MaritalStatusTypeDTO> ListAllMaritalStatusTypeDTOs()
        {
            List<MaritalStatusTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllMaritalStatusTypeDTOs");

                if (_registerationStaticDataDTO.MaritalStatusTypes != null)
                {
                    list = _registerationStaticDataDTO.MaritalStatusTypes;
                }
                else
                {
                    list = typeListingDAO.ListAllMaritalStatusTypeDTOs();
                    _registerationStaticDataDTO.MaritalStatusTypes = list;
                }
            }
            catch
            {
  
                throw ;
            }
            return list;
        }

        private List<RaceTypeDTO> ListAllRaceTypeDTOs()
        {
            List<RaceTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllRaceTypeDTOs");

                if (_registerationStaticDataDTO.RaceTypes != null)
                {
                    list = _registerationStaticDataDTO.RaceTypes;
                }
                else
                {
                    list = typeListingDAO.ListAllRaceTypeDTOs();
                    _registerationStaticDataDTO.RaceTypes = list;
                }
            }
            catch
            {
              
                throw ;
            }
            return list;
        }

        private List<EthinicityTypeDTO> ListAllEthinicityTypeDTOs()
        {
            List<EthinicityTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllEthinicityTypeDTOs");

                if (_registerationStaticDataDTO.EthinicityTypes != null)
                {
                    list = _registerationStaticDataDTO.EthinicityTypes;
                }
                else
                {
                    list = typeListingDAO.ListAllEthinicityTypeDTOs();
                    _registerationStaticDataDTO.EthinicityTypes = list;
                }
            }
            catch
            {

                throw ;
            }
            return list;
        }

        private List<OccupationTypeDTO> ListAllOccupationTypeDTOs()
        {
            List<OccupationTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllOccupationTypeDTOs");

                if (_registerationStaticDataDTO.OccupationTypes != null)
                {
                    list = _registerationStaticDataDTO.OccupationTypes;
                }
                else
                {
                    list = typeListingDAO.ListAllOccupationTypeDTOs();
                    _registerationStaticDataDTO.OccupationTypes = list;
                }                
            }
            catch 
            {
              
                throw ;
            }
            return list;
        }

        private List<LanguageTypeDTO> ListAllLanguageTypeDTOs()
        {
            List<LanguageTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllLanguageTypeDTOs");

                if (_registerationStaticDataDTO.LanguageTypes != null)
                {
                    list = _registerationStaticDataDTO.LanguageTypes;
                }
                else
                {
                    list = typeListingDAO.ListAllLanguageTypeDTOs();
                    _registerationStaticDataDTO.LanguageTypes = list;
                } 
            }
            catch
            {

                throw ;
            }
            return list;
        }

        /// <summary>
        /// To Get List of all Language Ability Mode Types
        /// </summary>
        /// <returns></returns>
        private List<LanguageAbilityModeDTO> ListAllLanguageAbilityModeTypeDTOs()
        {
            List<LanguageAbilityModeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllLanguageAbilityModeTypeDTOs");

                if (_registerationStaticDataDTO.LanguageAbilityModeDTOList != null)
                {
                    list = _registerationStaticDataDTO.LanguageAbilityModeDTOList;
                }
                else
                {
                    list = typeListingDAO.ListAllLanguageAbilityModeTypeDTOs();
                    _registerationStaticDataDTO.LanguageAbilityModeDTOList = list;
                }                
            }
            catch
            {
              
                throw ;
            }
            return list;
        }

        private List<RelationshipTypeDTO> ListAllRelationShipTypeDTOs()
        {
            List<RelationshipTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllRelationShipTypeDTOs");

                if (_registerationStaticDataDTO.RelationshipTypes != null)
                {
                    list = _registerationStaticDataDTO.RelationshipTypes;
                }
                else
                {
                    list = typeListingDAO.ListAllRelationShipTypeDTOs();
                    _registerationStaticDataDTO.RelationshipTypes = list;
                }                
            }
            catch
            {
              
                throw ;
            }
            return list;
        }

        private List<SmokingTypeDTO> ListAllSmokingTypeDTOs()
        {
            List<SmokingTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllSmokingTypeDTOs");

                if (_registerationStaticDataDTO.SmokingTyeDTOList != null)
                {
                    list = _registerationStaticDataDTO.SmokingTyeDTOList;
                }
                else
                {
                    list = typeListingDAO.ListAllSmokingTypeDTOs();
                    _registerationStaticDataDTO.SmokingTyeDTOList = list;
                } 
            }
            catch
            {
              
                throw ;
            }
            return list;
        }

        private List<InsuranceNatureDTO> ListAllInsuranceNatureTypeDTOs()
        {
            List<InsuranceNatureDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllInsuranceNatureTypeDTOs");

                if (_registerationStaticDataDTO.InsuranceNatureTypes != null)
                {
                    list = _registerationStaticDataDTO.InsuranceNatureTypes;
                }
                else
                {
                    list = typeListingDAO.ListAllInsuranceNatureDTOs();
                    _registerationStaticDataDTO.InsuranceNatureTypes = list;
                } 
            }
            catch
            {
            
                throw ;
            }
            return list;
        }

        private List<InsuranceTypeDTO> ListAllInsuranceTypeDTOs()
        {
            List<InsuranceTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllInsuranceTypeDTOs");

                if (_registerationStaticDataDTO.InsuranceTypes != null)
                {
                    list = _registerationStaticDataDTO.InsuranceTypes;
                }
                else
                {
                    list = typeListingDAO.ListAllInsuranceTypeDTOs();
                    _registerationStaticDataDTO.InsuranceTypes= list;
                }
            }
            catch
            {
            
                throw ;
            }
            return list;
        }

        private List<InsuranceOrganizationDTO> ListAllInsuranceOrganizationDTOs()
        {
            List<InsuranceOrganizationDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllInsuranceOrganizationDTOs");

                if (_registerationStaticDataDTO.InsuranceOrganizations != null)
                {
                    list = _registerationStaticDataDTO.InsuranceOrganizations;
                }
                else
                {
                    list = typeListingDAO.ListAllInsuranceOrganizationDTOs();
                    _registerationStaticDataDTO.InsuranceOrganizations = list;
                }
            }
            catch
            {
             
                throw ;
            }
            return list;
        }

        public RegisterationStaticDataDTO GetAllRegisterationStaticData()
        {
            RegisterationStaticDataDTO regSD = null;

            try
            {
                QuivusLogger.Info("start Loading static information");
                regSD = new RegisterationStaticDataDTO();

                regSD.AllStatusTypeList = ListAllStatusTypes();
                regSD.UserTypes = ListAllUserTypeDTOs();

                regSD.PrefixTypes = ListAllPrefixTypeDTOs();
                regSD.GenderTypes = ListAllGenderTypeDTOs();
                regSD.MaritalStatusTypes = ListAllMaritalStatusTypeDTOs();
                regSD.RaceTypes = ListAllRaceTypeDTOs();
                regSD.EthinicityTypes = ListAllEthinicityTypeDTOs();
                regSD.OccupationTypes = ListAllOccupationTypeDTOs();
                regSD.LanguageTypes = ListAllLanguageTypeDTOs();
                regSD.LanguageAbilityModeDTOList = ListAllLanguageAbilityModeTypeDTOs();
                regSD.RelationshipTypes = ListAllRelationShipTypeDTOs();
                regSD.SmokingTyeDTOList = ListAllSmokingTypeDTOs();

                regSD.InsuranceTypes = ListAllInsuranceTypeDTOs();
                regSD.InsuranceNatureTypes = ListAllInsuranceNatureTypeDTOs();
                regSD.InsuranceOrganizations = ListAllInsuranceOrganizationDTOs();

            }
            catch
            {
                throw;
            }
            return regSD;
        }

        public RegisterationStaticDataDTO GetRegisterationStaticDataByType(List<string> typeList)
        {
            RegisterationStaticDataDTO regSD = null;

            try
            {
                QuivusLogger.Info("start Loading static information GetRegisterationStaticDataByType :: Param typeList" + typeList);
                regSD = new RegisterationStaticDataDTO();

                foreach (string type in typeList)
                {
                    if (type.Equals(ProjectConstant.ALL_REGISTERATION_STATIC_DATA_ALL))
                    {
                        return GetAllRegisterationStaticData();
                    }
                    else if (type.Equals(ProjectConstant.STATUS_TYPE_REGISTERATION_STATIC_DATA))
                    {
                        regSD.AllStatusTypeList = ListAllStatusTypes();
                    }
                    else if (type.Equals(ProjectConstant.USER_TYPE_REGISTERATION_STATIC_DATA))
                    {
                        regSD.UserTypes = ListAllUserTypeDTOs();
                    }
                    else if (type.Equals(ProjectConstant.PREFIX_TYPE_REGISTERATION_STATIC_DATA))
                    {
                        regSD.PrefixTypes = ListAllPrefixTypeDTOs();
                    }
                    else if (type.Equals(ProjectConstant.GENDER_TYPE_REGISTERATION_STATIC_DATA))
                    {
                        regSD.GenderTypes = ListAllGenderTypeDTOs();
                    }
                    else if (type.Equals(ProjectConstant.MARITAL_STATUS_TYPE_REGISTERATION_STATIC_DATA))
                    {
                        regSD.MaritalStatusTypes = ListAllMaritalStatusTypeDTOs();
                    }
                    else if (type.Equals(ProjectConstant.RACE_TYPE_REGISTERATION_STATIC_DATA))
                    {
                        regSD.RaceTypes = ListAllRaceTypeDTOs();
                    }
                    else if (type.Equals(ProjectConstant.ETHINICITY_TYPE_REGISTERATION_STATIC_DATA))
                    {
                        regSD.EthinicityTypes = ListAllEthinicityTypeDTOs();
                    }
                    else if (type.Equals(ProjectConstant.OCCUPATION_TYPE_REGISTERATION_STATIC_DATA))
                    {
                        regSD.OccupationTypes = ListAllOccupationTypeDTOs();
                    }
                    else if (type.Equals(ProjectConstant.LANGUAGE_TYPE_REGISTERATION_STATIC_DATA))
                    {
                        regSD.LanguageTypes = ListAllLanguageTypeDTOs();
                    }
                    else if (type.Equals(ProjectConstant.LANGUAGE_ABILITY_MODE_TYPE_REGISTERATION_STATIC_DATA))
                    {
                        regSD.LanguageAbilityModeDTOList = ListAllLanguageAbilityModeTypeDTOs();
                    }
                    else if (type.Equals(ProjectConstant.RELATIONSHIP_TYPE_REGISTERATION_STATIC_DATA))
                    {
                        regSD.RelationshipTypes = ListAllRelationShipTypeDTOs();
                    }
                    else if (type.Equals(ProjectConstant.SMOKING_TYPE_REGISTERATION_STATIC_DATA))
                    {
                        regSD.SmokingTyeDTOList = ListAllSmokingTypeDTOs();
                    }
                    else if (type.Equals(ProjectConstant.INSURANCE_TYPE_REGISTERATION_STATIC_DATA))
                    {
                        regSD.InsuranceTypes = ListAllInsuranceTypeDTOs();
                    }
                    else if (type.Equals(ProjectConstant.INSURANCE_NATURE_TYPE_REGISTERATION_STATIC_DATA))
                    {
                        regSD.InsuranceNatureTypes = ListAllInsuranceNatureTypeDTOs();
                    }
                    else if (type.Equals(ProjectConstant.INSURANCE_ORGANIZATION_TYPE_REGISTERATION_STATIC_DATA))
                    {
                        regSD.InsuranceOrganizations = ListAllInsuranceOrganizationDTOs();
                    }
                }

            }
            catch 
            {
                throw;
            }
            return regSD;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get the module types for notes etc
        /// </summary>              
        /// <returns></returns>
        public List<ModuleTypeDTO> ListAllModuleTypeDTOs()
        {
            List<ModuleTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllModuleTypeDTOs");

                if (typeListingDAO == null)
                {
                    typeListingDAO = (TypeListingDAO)DAOFactory.GetDataAccessObject(typeof(TypeListingDAO));
                }

                list = typeListingDAO.ListAllModuleTypeDTOs();
            }
            catch
            {

                throw;
            }
            return list;
        }


        public List<QuickLinkTypeDTO> ListDefaultQuickLinkTypeDTOs()
        {
            List<QuickLinkTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListDefaultQuickLinkTypeDTOs");

                if (typeListingDAO == null)
                {
                    typeListingDAO = (TypeListingDAO)DAOFactory.GetDataAccessObject(typeof(TypeListingDAO));
                }

                list = typeListingDAO.ListDefaultQuickLinkTypeDTOs();
            }
            catch
            {

                throw;
            }
            return list;
        }


        public List<SearchEngineTypeDTO> ListSystemGeneratedSearchEngineTypeDTOs()
        {
            List<SearchEngineTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListSystemGeneratedSearchEngineTypeDTOs");

                if (typeListingDAO == null)
                {
                    typeListingDAO = (TypeListingDAO)DAOFactory.GetDataAccessObject(typeof(TypeListingDAO));
                }

                list = typeListingDAO.ListSystemGeneratedSearchEngineTypeDTOs();
            }
            catch
            {

                throw;
            }
            return list;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 06 Oct 2011
        /// Method Used To load Speciality Types
        /// </summary>
        /// <returns></returns>    
        public List<SpecialityTypeDTO> LoadSpecialityTypes()
        {
            List<SpecialityTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading LoadSpecialityTypes");

                if (typeListingDAO == null)
                {
                    typeListingDAO = (TypeListingDAO)DAOFactory.GetDataAccessObject(typeof(TypeListingDAO));
                }

                list = typeListingDAO.LoadSpecialityTypes();
            }
            catch
            {

                throw;
            }
            return list;
        }

        #endregion
    }
}
