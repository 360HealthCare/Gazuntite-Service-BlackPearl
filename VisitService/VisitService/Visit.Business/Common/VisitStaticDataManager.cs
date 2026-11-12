using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Utility.Logger;
using Quivus.Utility.Factory;
using Visit.DataAccess;

using Quivus.Model.TypeDTOs;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.MedicalHistory;
using Quivus.Model.QuickInput.ComprehensiveHistory.Medical;
using Visit.DataAccess.DAO;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.FamilyHistory;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Vital;
using Quivus.Utility.CustomException;
using Quivus.Utility;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Labs;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.ROS;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.HPI;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.SocialHistory;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.Recommendation;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Physicals;
//using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.Recommendation;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs;
using Quivus.Model.QuickInput.ROS;
using Quivus.Model.QuickInput.Physical;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp;
using QuivusModel.TypeDTOs.QuickInputTypeDTOs.Physical.ROS;
using QuivusModel.TypeDTOs.QuickInputTypeDTOs.Physical.Physicals;

namespace Visit.Business.Common
{
    /// <summary>
    /// Author Mansoor
    /// Date 17 Feb 2011
    /// List all the static data related to visit
    /// </summary>
    public class VisitStaticDataManager
    {
        private VisitDAO visitDAO;

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// get the Encounter types
        /// </summary>
        /// <returns></returns>
        public List<EncounterTypeDTO> LoadEncounterTypes()
        {
            List<EncounterTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListEncounterTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.GetAllEncounterTypes();
            }
            catch 
            {
                throw;
            }
            return list;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization types static data
        /// </summary>        
        /// <returns></returns>
        public List<ImmunizationTypeDTO> LoadImmunizationTypes()
        {
            List<ImmunizationTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading LoadImmunizationTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.GetAllImmunizationTypes();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used to load Medical History Types Used 
        /// Under Comprehensive History Section
        /// </summary>
        /// <returns></returns>
        public List<MedicalHistoryTypeDTO> LoadMedicalHistoryTypes()
        {
            List<MedicalHistoryTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading LoadMedicalHistoryTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadMedicalHistoryTypes();
            }
            catch
            {
                throw;
            }
            return list;
        }


        public List<FamilyHistoryTypeDTO> LoadFamilyHistoryTypes()
        {
            List<FamilyHistoryTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading LoadFamilyHistoryTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadFamilyHistoryTypes();
            }
            catch
            {
                throw;
            }
            return list;
        }

        public List<DiseaseTypeDTO> LoadDiseaseTypes(int ClinicBranchID, int FamilyHistoryTypeID)
        {
            List<DiseaseTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading LoadDiseaseTypes");

                if (ClinicBranchID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                }
                if (FamilyHistoryTypeID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.FAMILY_HISTORY_ID_INVALID_ERROR_MSG);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadDiseaseTypes(ClinicBranchID, FamilyHistoryTypeID);
            }
            catch
            {
                throw;
            }
            return list;
        }


        /// <summary>
        /// Author Mansoor 
        /// Date 14 March 2011
        /// This Method Will be used to load Vital Types 
        /// </summary>
        /// <returns></returns> 
        public List<VitalTypeDTO> LoadVitalTypes()
        {
            List<VitalTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading LoadVitalTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadVitalTypes();
            }
            catch
            {
                throw;
            }
            return list;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab nature Types 
        /// </summary>
        /// <returns></returns>
        public List<LabNatureTypeDTO> LoadLabNatureTypes()
        {
            List<LabNatureTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading LoadLabNatureTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadLabNatureTypes();
            }
            catch
            {
                throw;
            }
            return list;
        }

        public List<LabTestPriorityTypeDTO> LoadLabTestPriorityTypes()
        {
            List<LabTestPriorityTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading LoadLabTestPriorityTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadLabTestPriorityTypes();
            }
            catch
            {
                throw;
            }
            return list;
        }

        public List<LabTestSpecimenTypeDTO> LoadLabTestSpecimenTypes()
        {
            List<LabTestSpecimenTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading LoadLabTestSpecimenTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadLabTestSpecimenTypes();
            }
            catch
            {
                throw;
            }
            return list;
        }

        public List<LabTestTypeDTO> LoadLabTestTypes()
        {
            List<LabTestTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading LoadLabTestTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadLabTestTypes();
            }
            catch
            {
                throw;
            }
            return list;
        }

        public List<LabTypeDTO> LoadLabTypes()
        {
            List<LabTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading LoadLabTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadLabTypes();
            }
            catch
            {
                throw;
            }
            return list;
        }

        public List<LabTestOrderMethodTypeDTO> LoadLabTestOrderMethodTypes()
        {
            List<LabTestOrderMethodTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading LoadLabTestOrderMethodTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadLabTestOrderMethodTypes();
            }
            catch
            {
                throw;
            }
            return list;
        }

        public List<ICDCodeDTO> LoadICDCodeByName(string queryString)
        {
            List<ICDCodeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading LoadICDCodeByName");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadICDCodeByName(queryString);
            }
            catch
            {
                throw;
            }
            return list;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 March 2011
        /// This Method Will be used to load ROS Main Category
        /// </summary>
        /// <returns></returns>
        public List<ROSMainCategoryTypeDTO> LoadROSMainCategoryTypes(bool IsMale)
        {
            List<ROSMainCategoryTypeDTO> MainList = new List<ROSMainCategoryTypeDTO>();
            try
            {
                QuivusLogger.Info("Loading LoadROSMainCategoryTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                List<ROSMainCategoryTypeDTO> list = visitDAO.LoadROSMainCategoryTypes();

                List<ROSSubCategoryTypeDTO> subList = LoadROSSubCategoryTypes();

                if (list != null && list.Count > 0)
                {

                    var queryMain = IsMale ? from d in list where d.IsMaleApplicable.Equals(true) select d : from d in list where d.IsFemaleApplicable.Equals(true) select d;

                    if (queryMain != null && queryMain.ToArray().Length > 0)
                    {
                        MainList = queryMain.ToList();

                        if (MainList != null)
                        {
                            foreach (ROSMainCategoryTypeDTO obj in MainList)
                            {
                                var query = from d in subList
                                            where d.ROSMainCategoryTypeID.Equals(obj.ROSMainCategoryTypeID) &&
                                                d.IsMaleApplicable.Equals(IsMale)
                                            select d;
                                if (query != null && query.ToArray().Length > 0)
                                {
                                    obj.children = query.ToList();
                                }
                            }
                        }
                    }
                } // IF List Not NUll
            }
            catch
            {
                throw;
            }
            return MainList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 March 2011
        /// This Method Will be used to load ROS Sub Category
        /// </summary>
        /// <returns></returns>
        public List<ROSSubCategoryTypeDTO> LoadROSSubCategoryTypes()
        {
            List<ROSSubCategoryTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading LoadROSSubCategoryTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadROSSubCategoryTypes();
            }
            catch
            {
                throw;
            }
            return list;
        }


        //Commented by Mansoor
        ///// <summary>
        ///// Author Muhammad Ismail 
        ///// Date 21 March 2011
        ///// This Method Will be used to load HPI Data Control Types
        ///// </summary>
        ///// <returns></returns>
        //public List<HPIDataControlTypeDTO> LoadHPIDataControlTypes()
        //{
        //    List<HPIDataControlTypeDTO> list = null;
        //    try
        //    {
        //        QuivusLogger.Info("Loading LoadHPIDataControlTypes");

        //        if (visitDAO == null)
        //        {
        //            visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
        //        }

        //        list = visitDAO.LoadHPIDataControlTypes();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    return list;
        //}


        /// <summary>
        /// Author Mansoor 
        /// Date 21 Mar 2011
        /// This Method Will be used to HPI detail types 
        /// </summary>
        /// <returns></returns>  
        public List<HPITypeDTO> LoadHPIDetailTypes()
        {
            List<HPITypeDTO> hpiTypeList = null;
            try
            {
                QuivusLogger.Info("Loading LoadHPIDetailTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                hpiTypeList = visitDAO.LoadHPIDetailTypes();
            }
            catch
            {
                throw;
            }
            return hpiTypeList;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 21 Mar 2011
        /// This Method Will be used to load Social Addication types 
        /// </summary>
        /// <returns></returns>
        public List<SocialHistoryAddictionTypeDTO> SocialHistoryAddictionTypeDTO()
        {
            List<SocialHistoryAddictionTypeDTO> TypeList = null;
            try
            {
                QuivusLogger.Info("Loading SocialHistoryAddictionTypeDTO");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                TypeList = visitDAO.SocialHistoryAddictionTypeDTO();
            }
            catch
            {
                throw;
            }
            return TypeList;
        }

        public List<SocialMaritalStatusTypeDTO> LoadMaritalStatusTypes()
        {
            List<SocialMaritalStatusTypeDTO> TypeList = null;
            try
            {
                QuivusLogger.Info("Loading LoadMaritalStatusTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                TypeList = visitDAO.LoadMaritalStatusTypes();
            }
            catch
            {
                throw;
            }
            return TypeList;
        }


        public List<SocialHistoryUsageAmountTypeDTO> LoadSocialHistoryUsageAmountTypes()
        {
            List<SocialHistoryUsageAmountTypeDTO> TypeList = null;
            try
            {
                QuivusLogger.Info("Loading LoadSocialHistoryUsageAmountTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                TypeList = visitDAO.LoadSocialHistoryUsageAmountTypes();
            }
            catch
            {
                throw;
            }
            return TypeList;
        }

        public List<SocialHistoryMediumTypeDTO> LoadSocialHistoryMediumTypes()
        {
            List<SocialHistoryMediumTypeDTO> TypeList = null;
            try
            {
                QuivusLogger.Info("Loading LoadSocialHistoryMediumTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                TypeList = visitDAO.LoadSocialHistoryMediumTypes();
            }
            catch
            {
                throw;
            }
            return TypeList;
        }



        /// <summary>
        /// Author Mansoor
        /// Date 22 Mar 2011
        /// This Method Will be used to load Plan types 
        /// </summary>
        /// <returns></returns>       
        public List<PlanTypeDTO> LoadAllRecommendationPlanTypes()
        {
            List<PlanTypeDTO> TypeList = null;
            try
            {
                QuivusLogger.Info("Loading LoadAllRecommendationPlanTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                TypeList = visitDAO.LoadAllRecommendationPlanTypes();
            }
            catch
            {
                throw;
            }
            return TypeList;
        }


        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Duration types 
        /// </summary>
        /// <returns></returns>       
        public List<DurationTypeDTO> LoadAllRecommendationDurationTypes()
        {
            List<DurationTypeDTO> TypeList = null;
            try
            {
                QuivusLogger.Info("Loading LoadAllRecommendationDurationTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                TypeList = visitDAO.LoadAllRecommendationDurationTypes();
            }
            catch
            {
                throw;
            }
            return TypeList;
        }


        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Method types 
        /// </summary>
        /// <returns></returns>       
        public List<MethodTypeDTO> LoadAllRecommendationMethodTypes()
        {
            List<MethodTypeDTO> TypeList = null;
            try
            {
                QuivusLogger.Info("Loading LoadAllRecommendationMethodTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                TypeList = visitDAO.LoadAllRecommendationMethodTypes();
            }
            catch
            {
                throw;
            }
            return TypeList;
        }


        public List<PhysicalSubCategoryTypeDTO> LoadPhysicalSubCategoryTypes()
        {
            List<PhysicalSubCategoryTypeDTO> TypeList = null;
            try
            {
                QuivusLogger.Info("Loading LoadPhysicalSubCategoryTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                TypeList = visitDAO.LoadPhysicalSubCategoryTypes();
            }
            catch
            {
                throw;
            }
            return TypeList;
        }

        public List<PhysicalMainCategoryTypeDTO> LoadPhysicalMainCategoryTypes(bool IsMale)
        {
            List<PhysicalMainCategoryTypeDTO> MainList = new List<PhysicalMainCategoryTypeDTO>();
            try
            {
                QuivusLogger.Info("Loading LoadPhysicalMainCategoryTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                List<PhysicalMainCategoryTypeDTO> list = visitDAO.LoadPhysicalMainCategoryTypes();

                List<PhysicalSubCategoryTypeDTO> subList = LoadPhysicalSubCategoryTypes();

                if (list != null && list.Count > 0)
                {
                    //   var queryMain = from d in list where d.IsMaleApplicable.Equals(IsMale) select d;

                    var queryMain = IsMale ? from d in list where d.IsMaleApplicable.Equals(true) select d : from d in list where d.IsFemaleApplicable.Equals(true) select d;

                    if (queryMain != null && queryMain.ToArray().Length > 0)
                    {
                        MainList = queryMain.ToList();

                        if (MainList != null)
                        {
                            foreach (PhysicalMainCategoryTypeDTO obj in MainList)
                            {
                                var query = IsMale ? from d in subList
                                            where d.PhysicalMainCategoryTypeID.Equals(obj.PhysicalMainCategoryTypeID) &&
                                                d.IsMaleApplicable.Equals(true)
                                            select d:from d in subList
                                            where d.PhysicalMainCategoryTypeID.Equals(obj.PhysicalMainCategoryTypeID) &&
                                                d.IsFemaleApplicable.Equals(true)
                                                     select d;
                                if (query != null && query.ToArray().Length > 0)
                                {
                                    obj.children = query.ToList();
                                }
                            }
                        }
                    }
                } // IF List Not NUll
            }
            catch
            {
                throw;
            }
            return MainList;
        }

        public List<LIONICLabTestTypeDTO> LoadLIONICLabTestType(int LabtestTypeID, string LabTestCodeOrName)
        // public List<LIONICLabTestTypeDTO> LoadLIONICLabTestType()
        {
            List<LIONICLabTestTypeDTO> TypeList = null;
            try
            {
                QuivusLogger.Info("Loading LoadLIONICLabTestType");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                TypeList = visitDAO.LoadLIONICLabTestType(LabtestTypeID, LabTestCodeOrName);
                //    TypeList = visitDAO.LoadLIONICLabTestType();
            }
            catch
            {
                throw;
            }
            return TypeList;
        }

        ///// <summary>
        ///// Author Mansoor
        ///// Date 22 Mar 2011
        ///// get Recommendation auto complete suggestion box, by type id and clinic branch
        ///// </summary>        
        ///// <param name="searchCriteria"></param>
        ///// <param name="clinicBranchId"></param>
        ///// <returns></returns>      
        //public List<RecommendationLinksTypeDTO> LoadRecommendationLinksAutoCompleteByClinicId(int clinicBranchId, string searchCriteria)
        //{
        //    List<RecommendationLinksTypeDTO> list = null;
        //    try
        //    {
        //        QuivusLogger.Info("Calling LoadRecommendationLinksAutoCompleteByClinicId PARAM :: searchCriteria " + searchCriteria + " clinicBranchId " + clinicBranchId);

        //        if (clinicBranchId <= 0)
        //        {
        //            throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
        //        }

        //        if (visitDAO == null)
        //        {
        //            visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
        //        }

        //        list = visitDAO.LoadRecommendationLinksAutoCompleteByClinicId(clinicBranchId, searchCriteria);
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }
        //    return list;
        //}

        /// <summary>
        /// Author Mansoor
        /// Date 25 March 2011
        /// This Method Will be used to load FollowUp Method Type
        /// /// </summary>
        /// <returns></returns>  
        public List<FollowUpMethodTypeDTO> LoadFollowUpMethodTypes()
        {
            List<FollowUpMethodTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadFollowUpMethodType");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadFollowUpMethodTypes();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// Method Load In House Lab Types By Clinic branch Id
        /// </summary>
        /// <param name="ClinicBranchId"></param>
        /// <returns></returns>
        public List<InHouseLabTestTypeDTO> LoadInHouseLabTestTypeByClinicBranchId(int ClinicBranchId)
        {
            List<InHouseLabTestTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadFollowUpMethodType");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadInHouseLabTestTypeByClinicBranchId(ClinicBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }


        /// <summary>
        /// Author Ismail 
        /// Date 06 april 2011
        /// Method Load CPT Medical Types
        /// </summary>
        /// <returns></returns>
        public List<CPTMedicalTypeDTO> LoadCPTMedicalTypes(String CPTName)
        {
            List<CPTMedicalTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadFollowUpMethodType");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadCPTMedicalTypes(CPTName);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        public List<ROSDTO> LoadROSCategoryTypes(bool IsMale)
        {
            List<ROSDTO> MainList = new List<ROSDTO>();
            try
            {
                QuivusLogger.Info("Loading LoadROSCategoryTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                List<ROSMainCategoryTypeDTO> list = visitDAO.LoadROSMainCategoryTypes();

                List<ROSSubCategoryTypeDTO> subList = LoadROSSubCategoryTypes();

               
                if (list != null && list.Count > 0)
                {
                    var queryMain = IsMale ? from d in list where d.IsMaleApplicable.Equals(true) select d : from d in list where d.IsFemaleApplicable.Equals(true) select d;

                    if (queryMain != null && queryMain.ToArray().Length > 0)
                    {
                        List<ROSMainCategoryTypeDTO> MainSelectedList = queryMain.ToList();

                        if (MainSelectedList != null && MainSelectedList.Count > 0)
                        {
                            // MainList Populate Main List 
                            foreach (ROSMainCategoryTypeDTO MainObj in MainSelectedList)
                            {
                                ROSDTO RosParentObj = new ROSDTO();

                                RosParentObj.ROSMainCategoryTypeID = MainObj.ROSMainCategoryTypeID;
                                RosParentObj.MainCategoryName = MainObj.MainCategoryName;
                                RosParentObj.label = MainObj.MainCategoryName;
                                
                                // Populate Children Now i.e SUb category which are also of type ROS DTO On Request of front end

                                PopulateRosSubCategoryList(IsMale, subList, MainObj, RosParentObj);

                                MainList.Add(RosParentObj);

                            } //foreach main list iterate

                        } // Selected List Not Null
                    }
                } // IF List Not NUll
            }
            catch
            {
                throw;
            }
            return MainList;
        }

        /// <summary>
        /// Author Ismail
        /// </summary>
        /// <param name="IsMale"></param>
        /// <param name="subList"></param>
        /// <param name="MainObj"></param>
        /// <param name="RosParentObj"></param>
        private void PopulateRosSubCategoryList(bool IsMale, List<ROSSubCategoryTypeDTO> subList, ROSMainCategoryTypeDTO MainObj, ROSDTO RosParentObj)
        {
            var subquery = IsMale ? from d in subList
                                    where d.ROSMainCategoryTypeID.Equals(MainObj.ROSMainCategoryTypeID) &&
                                        d.IsMaleApplicable.Equals(true)
                                    select d : from d in subList
                                               where d.ROSMainCategoryTypeID.Equals(MainObj.ROSMainCategoryTypeID) &&
                                                   d.IsFemaleApplicable.Equals(true)
                                               select d;

            if (subquery != null && subquery.ToArray().Length > 0)
            {
                List<ROSDTO> ChildrenList = new List<ROSDTO>(); // Do Not define this list outside otherwise you will get sweet logical bug :)

                foreach (ROSSubCategoryTypeDTO SubObj in subquery.ToList())
                {
                    // Now Add these Object As Children In ROS DTO :)
                    ROSDTO RosChildObj = new ROSDTO();

                    RosChildObj.ROSMainCategoryTypeID = MainObj.ROSMainCategoryTypeID;
                    RosChildObj.MainCategoryName = MainObj.MainCategoryName;

                    RosChildObj.ROSSubCategoryTypeID = SubObj.ROSSubCategoryTypeID;
                    RosChildObj.SubCategoryName = SubObj.SubCategoryName;
                    RosChildObj.label = SubObj.SubCategoryName;

                    // Now Load the Sub Level Categories of each sub category i.e thier children 3rd Level

                    PopulateSubLevele3Category(IsMale, MainObj, SubObj, RosChildObj);

                    ChildrenList.Add(RosChildObj);
                }
                RosParentObj.children = ChildrenList;

            } // if sub list not null
        }

        /// <summary>
        /// Author Ismail
        /// </summary>
        /// <param name="IsMale"></param>
        /// <param name="MainObj"></param>
        /// <param name="SubObj"></param>
        /// <param name="RosChildObj"></param>
        private void PopulateSubLevele3Category(bool IsMale, ROSMainCategoryTypeDTO MainObj, ROSSubCategoryTypeDTO SubObj, ROSDTO RosChildObj)
        {
            List<ROSSubLevelCategoryTypeDTO> subLevelList = null;

         //   subLevelList = LoadROSSubLevelCategoryTypesByGender(false, IsMale, !IsMale, SubObj.ROSSubCategoryTypeID);

         subLevelList=  LoadROSSubLevelCategoryTypes(SubObj.ROSSubCategoryTypeID);
            // Now You have the list of 3rd level set it in ROS DTO 
            if (subLevelList != null && subLevelList.Count > 0)
            {
                List<ROSDTO> Level3ChildrenList = new List<ROSDTO>(); // Do Not define this list outside otherwise you will get sweet logical bug :)

                foreach (ROSSubLevelCategoryTypeDTO obj3rdLevel in subLevelList)
                {
                    ROSDTO RosChild3Obj = new ROSDTO();

                    RosChild3Obj.ROSMainCategoryTypeID = MainObj.ROSMainCategoryTypeID;
                    RosChild3Obj.MainCategoryName = MainObj.MainCategoryName;

                    RosChild3Obj.ROSSubCategoryTypeID = SubObj.ROSSubCategoryTypeID;
                    RosChild3Obj.SubCategoryName = SubObj.SubCategoryName;

                    RosChild3Obj.ROSSubLevel3CategoryName = obj3rdLevel.SubLevel3CategoryName;
                    RosChild3Obj.ROSSubLevel3CategoryTypeID = obj3rdLevel.ROSSubLevel3CategoryTypeID;
                    RosChild3Obj.label = RosChild3Obj.ROSSubLevel3CategoryName = obj3rdLevel.SubLevel3CategoryName;

                    Level3ChildrenList.Add(RosChild3Obj);
                }
                RosChildObj.children = Level3ChildrenList;
            }
        }

        public List<PhysicalsDTO> LoadPhysicalCategoryTypes(bool IsMale)
        {
            List<PhysicalsDTO> MainList = new List<PhysicalsDTO>();
            try
            {
                QuivusLogger.Info("Loading LoadPhysicalCategoryTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                List<PhysicalMainCategoryTypeDTO> list = visitDAO.LoadPhysicalMainCategoryTypes();

                List<PhysicalSubCategoryTypeDTO> subList = LoadPhysicalSubCategoryTypes();

                if (list != null && list.Count > 0)
                {
                    var queryMain = IsMale ? from d in list where d.IsMaleApplicable.Equals(true) select d : from d in list where d.IsFemaleApplicable.Equals(true) select d;

                    if (queryMain != null && queryMain.ToArray().Length > 0)
                    {
                        List<PhysicalMainCategoryTypeDTO> MainSelectedList = queryMain.ToList();

                        if (MainSelectedList != null && MainSelectedList.Count > 0)
                        {
                            // MainList Populate Main List 
                            foreach (PhysicalMainCategoryTypeDTO MainObj in MainSelectedList)
                            {
                                PhysicalsDTO PhysicalParentObj = new PhysicalsDTO();

                                PhysicalParentObj.PhysicalMainCategoryTypeID = MainObj.PhysicalMainCategoryTypeID;
                                PhysicalParentObj.MainCategoryName = MainObj.MainCategoryName;
                                PhysicalParentObj.label = MainObj.MainCategoryName;

                                // Populate Children Now i.e SUb category which are also of type Physical DTO On Request of front end

                                PopulatePhysicalSubCategoryList(IsMale, subList, MainObj, PhysicalParentObj);

                                MainList.Add(PhysicalParentObj);

                            } //foreach main list iterate

                        } // Selected List Not Null
                    }
                } // IF List Not NUll
            }
            catch
            {
                throw;
            }
            return MainList;
        }

        /// <summary>
        /// Author Ismail
        /// </summary>
        /// <param name="IsMale"></param>
        /// <param name="subList"></param>
        /// <param name="MainObj"></param>
        /// <param name="PhysicalParentObj"></param>
        private void PopulatePhysicalSubCategoryList(bool IsMale, List<PhysicalSubCategoryTypeDTO> subList, PhysicalMainCategoryTypeDTO MainObj, PhysicalsDTO PhysicalParentObj)
        {
            var subquery = IsMale ? from d in subList
                                    where d.PhysicalMainCategoryTypeID.Equals(MainObj.PhysicalMainCategoryTypeID) &&
                                        d.IsMaleApplicable.Equals(true)
                                    select d : from d in subList
                                               where d.PhysicalMainCategoryTypeID.Equals(MainObj.PhysicalMainCategoryTypeID) &&
                                                   d.IsFemaleApplicable.Equals(true)
                                               select d;

            if (subquery != null && subquery.ToArray().Length > 0)
            {
                List<PhysicalsDTO> ChildrenList = new List<PhysicalsDTO>(); // Do Not define this list outside otherwise you will get sweet logical bug :)

                foreach (PhysicalSubCategoryTypeDTO SubObj in subquery.ToList())
                {
                    // Now Add these Object As Children In ROS DTO :)
                    PhysicalsDTO RosChildObj = new PhysicalsDTO();

                    RosChildObj.PhysicalMainCategoryTypeID = MainObj.PhysicalMainCategoryTypeID;
                    RosChildObj.MainCategoryName = MainObj.MainCategoryName;

                    RosChildObj.PhysicalSubCategoryTypeID = SubObj.PhysicalSubCategoryTypeID;
                    RosChildObj.SubCategoryName = SubObj.SubCategoryName;
                    RosChildObj.label = SubObj.SubCategoryName;
                    
                    // Load Level 3 Category Items 
                    List<PhysicalLevel3CategoryTypeDTO> levelChild = LoadPhysical3LevelCategoryTypes(SubObj.PhysicalSubCategoryTypeID);

                    if (levelChild != null && levelChild.Count > 0)
                    {
                        List<PhysicalsDTO> Children3List = new List<PhysicalsDTO>(); // Do Not define this list outside otherwise you will get sweet logical bug :)

                        foreach (PhysicalLevel3CategoryTypeDTO childObj in levelChild)
                        {
                            PhysicalsDTO RosLevel3ChildObj = new PhysicalsDTO();

                            // Special Check By Ismail That is Chappy i.e Set Level 3 Category Name is Main Obj//
                          //  PhysicalParentObj.PhysicalSubLevel3CategoryTypeID = childObj.PhysicalSubLevel3CategoryTypeID; // Chapi Removed By Ismail :)
                            // End Chapi//
                            RosLevel3ChildObj.PhysicalMainCategoryTypeID = MainObj.PhysicalMainCategoryTypeID;
                            RosLevel3ChildObj.MainCategoryName = MainObj.MainCategoryName;

                            RosLevel3ChildObj.PhysicalSubCategoryTypeID = SubObj.PhysicalSubCategoryTypeID;
                            RosLevel3ChildObj.SubCategoryName = SubObj.SubCategoryName;

                            RosLevel3ChildObj.PhysicalSubLevel3CategoryTypeID = childObj.PhysicalSubLevel3CategoryTypeID;
                            RosLevel3ChildObj.SubLevel3CategoryName = childObj.SubLevel3CategoryName;
                            RosLevel3ChildObj.label = childObj.SubLevel3CategoryName;

                            Children3List.Add(RosLevel3ChildObj);
                        }
                        RosChildObj.children = Children3List;
                    }

                    ChildrenList.Add(RosChildObj);
                }
                PhysicalParentObj.children = ChildrenList;

            } // if sub list not null
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Mar 2011
        /// get Recommendation auto complete suggestion box, by type id and clinic branch
        /// </summary>        
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>      
        public List<string> LoadRecommendationLinksAutoCompleteByClinicId(int clinicBranchId, string searchCriteria)
        {
            List<string> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadRecommendationLinksAutoCompleteByClinicId PARAM :: searchCriteria " + searchCriteria + " clinicBranchId " + clinicBranchId);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadRecommendationLinksAutoCompleteByClinicId(clinicBranchId, searchCriteria);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }


        /// <summary>
        /// Author Atta
        /// Date 14 April 2011
        /// /// </summary>
        /// <returns></returns>  
        public List<InstructionAgeTypeDTO> LoadFollowUpInstructionAgeTypes()
        {
            List<InstructionAgeTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadFollowUpInstructionAgeTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadFollowUpInstructionAgeTypes();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        /// <summary>
        /// Author Atta
        /// Date 14 April 2011
        /// /// </summary>
        /// <returns></returns>  
        public List<InstructionDurationTypeDTO> LoadFollowUpInstructionDurationTypes()
        {
            List<InstructionDurationTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadFollowUpMethodType");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadFollowUpInstructionDurationTypes();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        /// <summary>
        /// Author Atta
        /// Date 14 April 2011
        /// /// </summary>
        /// <returns></returns>  
        public List<ServiceTypeDTO> LoadFollowUpServiceTypes(int ClinicBranchID)
        {
            List<ServiceTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadFollowUpServiceTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadFollowUpServiceTypes(ClinicBranchID);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

         /// <summary>
        /// Author Atta
        /// Date 14 April 2011
        /// /// </summary>
        /// <returns></returns>  
        public List<InstructionAgeValueTypeDTO> LoadFollowUpInstructionAgeValueTypes()
        {
            List<InstructionAgeValueTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadFollowUpServiceTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadFollowUpInstructionAgeValueTypes();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }


        public List<ModuleTypeDTO> LoadModuleTypes()
        {
            List<ModuleTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadModuleTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadModuleTypes();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        public List<ROSSubLevelCategoryTypeDTO> LoadROSSubLevelCategoryTypes(int LoadROSSubLevelCategoryTypes)
        {
            List<ROSSubLevelCategoryTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadFollowUpServiceTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadROSSubLevelCategoryTypes(LoadROSSubLevelCategoryTypes);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        public List<PhysicalCategoryTypeDTO> LoadPhysicalCategoryTypes()
        {
            List<PhysicalCategoryTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadPhysicalCategoryTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadPhysicalCategoryTypes();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        public List<PhysicalCategoryTypeDTO> LoadPhysicalCategoryTypesByGender(bool LoadUniversal, bool LoadMaleOnly, bool LoadFemaleOnly)
        {
            List<PhysicalCategoryTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadPhysicalCategoryTypesByGender");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadPhysicalCategoryTypesByGender( LoadUniversal,  LoadMaleOnly,  LoadFemaleOnly);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        public List<ROSSubLevelCategoryTypeDTO> LoadROSSubLevelCategoryTypesByGender(bool LoadUniversal, bool LoadMaleOnly, bool LoadFemaleOnly, int ROSSubCategoryTypeID)
        {
            List<ROSSubLevelCategoryTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadROSSubLevelCategoryTypesByGender");

               
                if (ROSSubCategoryTypeID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.ROS_SUB_CATEGORY_TYPE_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadROSSubLevelCategoryTypesByGender(LoadUniversal, LoadMaleOnly, LoadFemaleOnly, ROSSubCategoryTypeID);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }


        public List<PhysicalLevel3CategoryTypeDTO> LoadPhysical3LevelCategoryTypes(int PhysicalSubCategoryTypeID)
        {
            List<PhysicalLevel3CategoryTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadPhysical3LevelCategoryTypes");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadPhysical3LevelCategoryTypes(PhysicalSubCategoryTypeID);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }
        
        
        /// <summary>
        /// AUTHOR Baber waqas at 1/24/2012
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="clinicBranchID"></param>
        /// <returns></returns>
        public List<ICDCodeDTO> LoadICDCodeByNameByBranchID(string queryString, int clinicBranchID)
        {
            List<ICDCodeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading LoadICDCodeByName");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }
                //TODO: remove this on production
                if (clinicBranchID == 0)
                    clinicBranchID = 7;
                list = visitDAO.LoadICDCodeByName(queryString,clinicBranchID);
            }
            catch
            {
                throw;
            }
            return list;
        }
    }
}
