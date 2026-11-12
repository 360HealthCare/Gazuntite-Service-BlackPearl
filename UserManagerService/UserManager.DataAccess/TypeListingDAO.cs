using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

using Quivus.Model.TypeDTOs;
using Quivus.Utility.Logger;
using Quivus.Model.Insurance;
using Quivus.Utility;

using UserManagerService.DataAccess.DB_Util;
using UserManagerService.DataAccess;
using QuivusModel.TypeDTOs;

namespace UserManagerService.DataAccess
{
    public class TypeListingDAO : BaseDAO
    {
        #region Listing Methods To Return DTOs

        public List<StatusTypeDTO> ListAllStatusTypesDTOs()
        {
            List<StatusTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.TYPE_LOAD_ALL_STATUS_SP;
            try
            {  
                QuivusLogger.Info("Loading LisAllStatusTypes sProc::" + sProc);
                list = new List<StatusTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(StatusTypeDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }            
            return list;
        }

        public List<UserTypeDTO> ListAllUserTypeDTOs()
        {
            IDataReader objIReader = null;
            string sProc = UserConstant.TYPE_LOAD_ALL_USERS_SP;
            List<UserTypeDTO> list = null;
            try
            {                
                QuivusLogger.Info("Loading ListAllUserTypeDTOs sProc::" + sProc);
                list = new List<UserTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        //M GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(UserTypeDTO));
                        list = DataReaderToDTOConverter.UserTypeDTOConverter(objIReader);
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<PrefixTypeDTO> ListAllPrefixTypeDTOs()
        {
            List<PrefixTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.TYPE_LOAD_ALL_PREFIX_SP;
            try
            {                
                QuivusLogger.Info("Loading ListAllPrefixTypeDTOs sProc::" + sProc);
                list = new List<PrefixTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(PrefixTypeDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<GenderTypeDTO> ListAllGenderTypeDTOs()
        {
            List<GenderTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.TYPE_LOAD_ALL_GENDER_SP;
            try
            {                
                QuivusLogger.Info("Loading ListAllGenderTypeDTOs sProc::" + sProc);
                list = new List<GenderTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(GenderTypeDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<MaritalStatusTypeDTO> ListAllMaritalStatusTypeDTOs()
        {
            List<MaritalStatusTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.TYPE_LOAD_ALL_MARITAL_STATUS_SP;
            try
            {               
                QuivusLogger.Info("Loading ListAllMaritalStatusTypeDTOs sProc::" + sProc);
                list = new List<MaritalStatusTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(MaritalStatusTypeDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<RaceTypeDTO> ListAllRaceTypeDTOs()
        {
            List<RaceTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.TYPE_LOAD_ALL_RACE_SP;
            try
            {               
                QuivusLogger.Info("Loading ListAllRaceTypeDTOs sProc::" + sProc);
                list = new List<RaceTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(RaceTypeDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<EthinicityTypeDTO> ListAllEthinicityTypeDTOs()
        {
            List<EthinicityTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.TYPE_LOAD_ALL_ETHINICITY_SP;
            try
            {               
                QuivusLogger.Info("Loading ListAllEthinicityTypeDTOs sProc::" + sProc);
                list = new List<EthinicityTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(EthinicityTypeDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<OccupationTypeDTO> ListAllOccupationTypeDTOs()
        {
            List<OccupationTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.TYPE_LOAD_ALL_OCCUPATION_SP;
            try
            {               
                QuivusLogger.Info("Loading ListAllOccupationTypeDTOs sProc::" + sProc);
                list = new List<OccupationTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(OccupationTypeDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;           
        }

        public List<LanguageTypeDTO> ListAllLanguageTypeDTOs()
        {
            List<LanguageTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.TYPE_LOAD_ALL_LANGUAGE_SP;
            try
            {               
                QuivusLogger.Info("Loading ListAllLanguageTypeDTOs sProc::" + sProc);
                list = new List<LanguageTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(LanguageTypeDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list; 
        }

        public List<LanguageAbilityModeDTO> ListAllLanguageAbilityModeTypeDTOs()
        {
            List<LanguageAbilityModeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.TYPE_LOAD_ALL_LANGUAGE_ABILITY_SP;
            try
            {               
                QuivusLogger.Info("Loading ListAllLanguageAbilityModeTypeDTOs sProc::" + sProc);
                list = new List<LanguageAbilityModeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(LanguageAbilityModeDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list; 
        }

        public List<RelationshipTypeDTO> ListAllRelationShipTypeDTOs()
        {
            List<RelationshipTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.TYPE_LOAD_ALL_RELATIONSHIP_SP;
            try
            {               
                QuivusLogger.Info("Loading ListAllRelationShipTypeDTOs sProc::" + sProc);
                list = new List<RelationshipTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(RelationshipTypeDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<SmokingTypeDTO> ListAllSmokingTypeDTOs()
        {
            List<SmokingTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.TYPE_LOAD_ALL_SMOKING_SP;
            try
            {               
                QuivusLogger.Info("Loading ListAllSmokingTypeDTOs sProc::" + sProc);
                list = new List<SmokingTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(SmokingTypeDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<InsuranceTypeDTO> ListAllInsuranceTypeDTOs()
        {
            List<InsuranceTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.TYPE_LOAD_ALL_INSURANCE_TYPE_SP;
            try
            {               
                QuivusLogger.Info("Loading ListAllInsuranceTypeDTOs sProc::" + sProc);
                list = new List<InsuranceTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(ProjectConstant.INSURANCE_DB); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(InsuranceTypeDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;

        }

        public List<InsuranceNatureDTO> ListAllInsuranceNatureDTOs()
        {
            List<InsuranceNatureDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.TYPE_LOAD_ALL_INSURANCE_NATURE_SP;
            try
            {               
                QuivusLogger.Info("Loading ListAllInsuranceNatureDTOs sProc::" + sProc);
                list = new List<InsuranceNatureDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(ProjectConstant.INSURANCE_DB); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(InsuranceNatureDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<InsuranceOrganizationDTO> ListAllInsuranceOrganizationDTOs()
        {
            List<InsuranceOrganizationDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.TYPE_LOAD_ALL_INSURANCE_ORGANIZATION_SP;
            try
            {
                QuivusLogger.Info("Loading ListAllInsuranceOrganizationDTOs sProc::" + sProc);
                list = new List<InsuranceOrganizationDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(ProjectConstant.INSURANCE_DB); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(InsuranceOrganizationDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
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
            IDataReader objIReader = null;
            string sProc = UserConstant.MODULE_TYPE_LOAD_ALL_SP;
            try
            {
                QuivusLogger.Info("Calling sProc::" + sProc);
                list = new List<ModuleTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(ModuleTypeDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }



        public List<QuickLinkTypeDTO> ListDefaultQuickLinkTypeDTOs()
        {
            List<QuickLinkTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.TYPE_LOAD_DEFAULT_QUICKLINKS;
            try
            {
                QuivusLogger.Info("Calling sProc::" + sProc);
                list = new List<QuickLinkTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(QuickLinkTypeDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }


        public List<SearchEngineTypeDTO> ListSystemGeneratedSearchEngineTypeDTOs()
        {
            List<SearchEngineTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.TYPE_LOAD_SEARCH_ENGINE_SYSTEM_GENERATED_SP;
            try
            {
                QuivusLogger.Info("Calling sProc::" + sProc);
                list = new List<SearchEngineTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(SearchEngineTypeDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
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
            IDataReader objIReader = null;
            string sProc = UserConstant.SPECIALITY_TYPE_LOAD_ALL_SP;
            try
            {
                QuivusLogger.Info("Calling sProc::" + sProc);
                list = new List<SpecialityTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(SpecialityTypeDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }      


        #endregion
    }
}
