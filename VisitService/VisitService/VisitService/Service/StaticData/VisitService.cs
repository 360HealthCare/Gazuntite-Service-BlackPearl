using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Labs;
using Visit.Business.Common;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;
using QuivusUtility.Utilities;
using Quivus.Model.TypeDTOs;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.ROS;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.HPI;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.SocialHistory;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.Recommendation;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Physicals;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs;
using Quivus.Model.QuickInput.ROS;
using Quivus.Model.QuickInput.Physical;
using QuivusModel.TypeDTOs.QuickInputTypeDTOs.Physical.ROS;
using QuivusModel.TypeDTOs.QuickInputTypeDTOs.Physical.Physicals;

//using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.Recommendation;

namespace VisitService.Service
{
    public partial class VisitService
    {
        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab nature Types 
        /// </summary>
        /// <returns></returns>

        public List<LabNatureTypeDTO> LoadLabNatureTypes()
        {
            List<LabNatureTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }
                TypeList = objVisitSDManager.LoadLabNatureTypes();
                QuivusLogger.Info("LoadLabNatureTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList; 
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab priority Types 
        /// </summary>
        /// <returns></returns>
        public List<LabTestPriorityTypeDTO> LoadLabTestPriorityTypes()
        {
            List<LabTestPriorityTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }
                TypeList = objVisitSDManager.LoadLabTestPriorityTypes();
                QuivusLogger.Info("LoadLabTestPriorityTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList; 
        }


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab specimen Types 
        /// </summary>
        /// <returns></returns>
        public List<LabTestSpecimenTypeDTO> LoadLabTestSpecimenTypes()
        {
            List<LabTestSpecimenTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }
                TypeList = objVisitSDManager.LoadLabTestSpecimenTypes();
                QuivusLogger.Info("LoadLabTestPriorityTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList; 
        }


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab test Types 
        /// </summary>
        /// <returns></returns>
        public List<LabTestTypeDTO> LoadLabTestTypes()
        {
            List<LabTestTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }
                TypeList = objVisitSDManager.LoadLabTestTypes();
                QuivusLogger.Info("LoadLabTestTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList; 
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab  Types 
        /// </summary>
        /// <returns></returns>

        public List<LabTypeDTO> LoadLabTypes()
        {
            List<LabTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }
                TypeList = objVisitSDManager.LoadLabTypes();
                QuivusLogger.Info("LoadLabTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList; 
        }


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab test order method Types 
        /// </summary>
        /// <returns></returns>
        public List<LabTestOrderMethodTypeDTO> LoadLabTestOrderMethodTypes()
        {
            List<LabTestOrderMethodTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }
                TypeList = objVisitSDManager.LoadLabTestOrderMethodTypes();
                QuivusLogger.Info("LoadLabTestOrderMethodTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList; 
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load ICD Code by String 
        /// </summary>
        /// <returns></returns>
        public List<ICDCodeDTO> LoadICDCodeByName(string queryString)
        {
            List<ICDCodeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }
                TypeList = objVisitSDManager.LoadICDCodeByName(queryString);
                QuivusLogger.Info("LoadLabTestOrderMethodTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList; 
        }


        /// <summary>
        /// Author Baber waqas
        /// Date 1/24/2012
        /// This Method Will be used to load ICD Code by String and clinic branch id for custom icd code 
        /// </summary>
        /// <returns></returns>
        public List<ICDCodeDTO> LoadICDCodeByNameAndClinicBranch(string queryString,int clinicBranchID)
        {
            List<ICDCodeDTO> TypeList = null;    
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }
                TypeList = objVisitSDManager.LoadICDCodeByNameByBranchID(queryString, clinicBranchID);
                QuivusLogger.Info("LoadICDCodeByNameAndClinicBranch Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 March 2011
        /// This Method Will be used to load ROS Main Category
        /// </summary>
        /// <returns></returns>
        public List<ROSMainCategoryTypeDTO> LoadROSMainCategoryTypes(bool IsMale)
        {
            List<ROSMainCategoryTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }
                TypeList = objVisitSDManager.LoadROSMainCategoryTypes(IsMale);
                QuivusLogger.Info("ROSMainCategoryTypeDTO Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList; 
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 March 2011
        /// This Method Will be used to load ROS Sub Category
        /// </summary>
        /// <returns></returns>
        public List<ROSSubCategoryTypeDTO> LoadROSSubCategoryTypes()
        {
            List<ROSSubCategoryTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }
                TypeList = objVisitSDManager.LoadROSSubCategoryTypes();
                QuivusLogger.Info("LoadROSSubCategoryTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList; 
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
        //    List<HPIDataControlTypeDTO> TypeList = null;
        //    try
        //    {
        //        if (objVisitSDManager == null)
        //        {
        //            objVisitSDManager = new VisitStaticDataManager();
        //        }
        //        TypeList = objVisitSDManager.LoadHPIDataControlTypes();

        //        QuivusLogger.Info("LoadHPIDataControlTypes Return List ::" + TypeList);
        //    }
        //    catch (InvalidArgumentException exp)
        //    {
        //        QuivusLogger.Error(exp.Message);
        //        QuivusLogger.Error(exp.StackTrace);
        //        HandleFault.HandleFautException(exp,exp.ErrorDetail);
        //    }
        //    catch (Exception exp)
        //    {
        //        QuivusLogger.Error(exp.Message);
        //        QuivusLogger.Error(exp.StackTrace);
        //        HandleFault.HandleFautException(exp);
        //    }
        //    return TypeList; 
        //}


        /// <summary>
        /// Author Mansoor 
        /// Date 21 Mar 2011
        /// This Method Will be used to load Social Addication types 
        /// </summary>
        /// <returns></returns>
        public List<SocialHistoryAddictionTypeDTO> LoadSocialHistoryAddictionTypes()
        {
            List<SocialHistoryAddictionTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }
                TypeList = objVisitSDManager.SocialHistoryAddictionTypeDTO();

                QuivusLogger.Info("SocialHistoryAddictionTypeDTO Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList; 
        }


        /// <summary>
        /// Author Mansoor 
        /// Date 21 Mar 2011
        /// This Method Will be used to load Social Marital Status types 
        /// </summary>
        /// <returns></returns>
        public List<SocialMaritalStatusTypeDTO> LoadMaritalStatusTypes()
        {
            List<SocialMaritalStatusTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }
                TypeList = objVisitSDManager.LoadMaritalStatusTypes();

                QuivusLogger.Info("LoadMaritalStatusTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList; 
        }


        /// <summary>
        /// Author Ismail 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Social Marital Status types 
        /// </summary>
        /// <returns></returns>
        public List<SocialHistoryUsageAmountTypeDTO> LoadSocialHistoryUsageAmountTypes()
        {
            List<SocialHistoryUsageAmountTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }
                TypeList = objVisitSDManager.LoadSocialHistoryUsageAmountTypes();

                QuivusLogger.Info("LoadSocialHistoryUsageAmountTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList; 
        }

        /// <summary>
        /// Author Ismail 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Social Marital Status types 
        /// </summary>
        /// <returns></returns>
        public List<SocialHistoryMediumTypeDTO> LoadSocialHistoryMediumTypes()
        {
            List<SocialHistoryMediumTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }
                TypeList = objVisitSDManager.LoadSocialHistoryMediumTypes();

                QuivusLogger.Info("LoadSocialHistoryMediumTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                TypeList = objVisitSDManager.LoadAllRecommendationPlanTypes();

                QuivusLogger.Info("LoadAllTreatmentPlanTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                TypeList = objVisitSDManager.LoadAllRecommendationDurationTypes();

                QuivusLogger.Info("LoadAllTreatmentDurationTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList;
        }


        ///// <summary>
        ///// Author Mansoor 
        ///// Date 22 Mar 2011
        ///// This Method Will be used to load Duration types 
        ///// </summary>
        ///// <returns></returns>
        //public List<RecommendationLinksTypeDTO> LoadRecommendationLinksAutoCompleteByClinicId(int clinicBranchId, string searchCriteria)
        //{
        //    List<RecommendationLinksTypeDTO> TypeList = null;
        //    try
        //    {
        //        if (objVisitSDManager == null)
        //        {
        //            objVisitSDManager = new VisitStaticDataManager();
        //        }

        //        TypeList = objVisitSDManager.LoadRecommendationLinksAutoCompleteByClinicId(clinicBranchId, searchCriteria);

        //        QuivusLogger.Info("LoadRecommendationLinksAutoCompleteByClinicId Return List ::" + TypeList);
        //    }
        //    catch (InvalidArgumentException exp)
        //    {
        //        QuivusLogger.Error(exp.Message);
        //        QuivusLogger.Error(exp.StackTrace);
        //        HandleFault.HandleFautException(exp,exp.ErrorDetail);
        //    }
        //    catch (Exception exp)
        //    {
        //        QuivusLogger.Error(exp.Message);
        //        QuivusLogger.Error(exp.StackTrace);
        //        HandleFault.HandleFautException(exp);
        //    }
        //    return TypeList;
        //}
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
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                TypeList = objVisitSDManager.LoadAllRecommendationMethodTypes();

                QuivusLogger.Info("LoadAllTreatmentMethodTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 22 March 2011
        /// This Method Will be used to load Physical Main Category
        /// </summary>
        /// <returns></returns>
        public List<PhysicalMainCategoryTypeDTO> LoadPhysicalMainCategoryTypes(bool IsMale)
        {
            List<PhysicalMainCategoryTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }
                TypeList = objVisitSDManager.LoadPhysicalMainCategoryTypes(IsMale);
                QuivusLogger.Info("LoadPhysicalMainCategoryTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList; 
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 July 2011
        /// This Method Will be used to load Physical Sub Category
        /// </summary>
        /// <returns></returns>
        public List<PhysicalCategoryTypeDTO> LoadPhysicalCategory()
        {
            List<PhysicalCategoryTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }
                TypeList = objVisitSDManager.LoadPhysicalCategoryTypes();
                QuivusLogger.Info("LoadPhysicalCategoryTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList; 
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 July 2011
        /// This Method Will be used to load Physical Sub Category
        /// </summary>
        /// <returns></returns>
        public List<PhysicalCategoryTypeDTO> LoadPhysicalCategoryTypesByGender(bool LoadUniversal, bool LoadMaleOnly, bool LoadFemaleOnly)
        {
            List<PhysicalCategoryTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }
                TypeList = objVisitSDManager.LoadPhysicalCategoryTypesByGender(LoadUniversal, LoadMaleOnly, LoadFemaleOnly);
                QuivusLogger.Info("LoadPhysicalCategoryTypesByGender Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 July 2011
        /// This Method Will be used to load Physical Sub 3 Category
        /// </summary>
        /// <returns></returns>
        public List<PhysicalLevel3CategoryTypeDTO> LoadPhysical3LevelCategoryTypes(int PhysicalSubCategoryTypeID)
        {
            List<PhysicalLevel3CategoryTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }
                TypeList = objVisitSDManager.LoadPhysical3LevelCategoryTypes(PhysicalSubCategoryTypeID);
                QuivusLogger.Info("LoadPhysical3CategoryTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList;
        }


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 22 March 2011
        /// This Method Will be used to load Physical Sub Category
        /// </summary>
        /// <returns></returns>
        public List<PhysicalSubCategoryTypeDTO> LoadPhysicalSubCategoryTypes()
        {
            List<PhysicalSubCategoryTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                TypeList = objVisitSDManager.LoadPhysicalSubCategoryTypes();

                QuivusLogger.Info("LoadPhysicalSubCategoryTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 22 March 2011
        /// This Method Will be used to load lab LIONIC Test Types
        /// /// </summary>
        /// <returns></returns>
        public List<LIONICLabTestTypeDTO> LoadLIONICLabTestType(int LabtestTypeID, string LabTestCodeOrName)
       // public List<LIONICLabTestTypeDTO> LoadLIONICLabTestType()
        {
            List<LIONICLabTestTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                TypeList = objVisitSDManager.LoadLIONICLabTestType(LabtestTypeID,LabTestCodeOrName);
             //   TypeList = objVisitSDManager.LoadLIONICLabTestType();
                QuivusLogger.Info("LoadLIONICLabTestType Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 25 March 2011
        /// This Method Will be used to load FollowUp Method Type
        /// /// </summary>
        /// <returns></returns>       
        public List<FollowUpMethodTypeDTO> LoadFollowUpMethodTypes()
        {
            List<FollowUpMethodTypeDTO> typeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                typeList = objVisitSDManager.LoadFollowUpMethodTypes();

                QuivusLogger.Info("LoadFollowUpMethodTypes Return List ::" + typeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return typeList;
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
            List<InHouseLabTestTypeDTO> typeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                typeList = objVisitSDManager.LoadInHouseLabTestTypeByClinicBranchId(ClinicBranchId);

                QuivusLogger.Info("LoadFollowUpMethodTypes Return List ::" + typeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return typeList;
        }

        /// <summary>
        /// Author Ismail 
        /// Date 06 april 2011
        /// Method Load CPT Medical Types
        /// </summary>
        /// <returns></returns>
        public List<CPTMedicalTypeDTO> LoadCPTMedicalTypes(String CPTName)
        {
            List<CPTMedicalTypeDTO> typeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                typeList = objVisitSDManager.LoadCPTMedicalTypes(CPTName);

                QuivusLogger.Info("LoadFollowUpMethodTypes Return List ::" + typeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return typeList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 March 2011
        /// This Method Will be used to load ROS Main Category
        /// </summary>
        /// <returns></returns>
        public List<ROSDTO> LoadROSCategoryTypes(bool IsMale)
        {
            List<ROSDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }
                TypeList = objVisitSDManager.LoadROSCategoryTypes(IsMale);
                QuivusLogger.Info("LoadROSCategoryTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 07 April 2011
        /// This Method Will be used to load ROS  Categories :)
        /// </summary>
        /// <returns></returns>
        public List<PhysicalsDTO> LoadPhysicalCategoryTypes(bool IsMale)
        {
            List<PhysicalsDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }
                TypeList = objVisitSDManager.LoadPhysicalCategoryTypes(IsMale);
                QuivusLogger.Info("LoadPhysicalCategoryTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Duration types 
        /// </summary>
        /// <returns></returns>
        public List<string> LoadRecommendationLinksAutoCompleteByClinicId(int clinicBranchId, string searchCriteria)
        {
            List<string> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                TypeList = objVisitSDManager.LoadRecommendationLinksAutoCompleteByClinicId(clinicBranchId, searchCriteria);

                QuivusLogger.Info("LoadRecommendationLinksAutoCompleteByClinicId Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList;
        }


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 15 July 2011
        /// This Method Will be used to load ROS Sub Level Categories
        /// </summary>
        /// <returns></returns>
        public List<ROSSubLevelCategoryTypeDTO> LoadROSSubLevelCategoryTypes(int ROSSubCategoryTypeID)
        {
            List<ROSSubLevelCategoryTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                TypeList = objVisitSDManager.LoadROSSubLevelCategoryTypes(ROSSubCategoryTypeID);

                QuivusLogger.Info("LoadROSSubLevelCategoryTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 July 2011
        /// This Method Will be used to load ROS Sub Level Categories By Gender
        /// </summary>
        /// <returns></returns>
        public List<ROSSubLevelCategoryTypeDTO> LoadROSSubLevelCategoryTypesByGender(bool LoadUniversal, bool LoadMaleOnly, bool LoadFemaleOnly, int ROSSubCategoryTypeID)
        {
            List<ROSSubLevelCategoryTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                TypeList = objVisitSDManager.LoadROSSubLevelCategoryTypesByGender(LoadUniversal, LoadMaleOnly, LoadFemaleOnly, ROSSubCategoryTypeID);

                QuivusLogger.Info("LoadROSSubLevelCategoryTypesByGender Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList;
        }
        
    }
}

 