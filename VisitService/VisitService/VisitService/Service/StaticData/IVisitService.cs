using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Quivus.Utility.Fault;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Labs;
using Quivus.Model.TypeDTOs;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.ROS;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.HPI;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.SocialHistory;
using Quivus.Model.QuickInput.ComprehensiveHistory.Social;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.Recommendation;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Physicals;
//using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.Recommendation;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs;
using Quivus.Model.QuickInput.ROS;
using Quivus.Model.QuickInput.MainQuickInput.Physical;
using Quivus.Model.QuickInput.Physical;
using QuivusModel.TypeDTOs.QuickInputTypeDTOs.Physical.ROS;
using QuivusModel.TypeDTOs.QuickInputTypeDTOs.Physical.Physicals;

namespace VisitService.Service
{

    public partial interface IVisitService
    {
        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab nature Types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<LabNatureTypeDTO> LoadLabNatureTypes();




        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab priority Types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<LabTestPriorityTypeDTO> LoadLabTestPriorityTypes();


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab specimen Types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<LabTestSpecimenTypeDTO> LoadLabTestSpecimenTypes();

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab test Types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<LabTestTypeDTO> LoadLabTestTypes();


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab  Types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<LabTypeDTO> LoadLabTypes();

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab test order method Types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<LabTestOrderMethodTypeDTO> LoadLabTestOrderMethodTypes();

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load ICD Code by String 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ICDCodeDTO> LoadICDCodeByName(string queryString);


        /// <summary>
        /// Author BABER WAQAS
        /// Date 1/24/2012
        /// This Method Will be used to load CUSTOM ICD Code by String 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ICDCodeDTO> LoadICDCodeByNameAndClinicBranch(string queryString,int clinicBranchID);


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 March 2011
        /// This Method Will be used to load ROS Main Category
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ROSMainCategoryTypeDTO> LoadROSMainCategoryTypes(bool IsMale);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 March 2011
        /// This Method Will be used to load ROS Sub Category
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ROSSubCategoryTypeDTO> LoadROSSubCategoryTypes();

        //Commented by Mansoor
        ///// <summary>
        ///// Author Muhammad Ismail 
        ///// Date 21 March 2011
        ///// This Method Will be used to load HPI Data Control Types
        ///// </summary>
        ///// <returns></returns>
        //[OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        //List<HPIDataControlTypeDTO> LoadHPIDataControlTypes();

        /// <summary>
        /// Author Mansoor 
        /// Date 21 Mar 2011
        /// This Method Will be used to load HPI detail types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<HPITypeDTO> LoadHPIDetailTypes();

        /// <summary>
        /// Author Ismail 
        /// Date 21 Mar 2011
        /// This Method Will be used to load Social Addication types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<SocialHistoryAddictionTypeDTO> LoadSocialHistoryAddictionTypes();

        /// <summary>
        /// Author Ismail 
        /// Date 21 Mar 2011
        /// This Method Will be used to load Social Marital Status types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<SocialMaritalStatusTypeDTO> LoadMaritalStatusTypes();

        /// <summary>
        /// Author Mansoor
        /// Date 22 Mar 2011
        /// This Method Will be used to load Plan types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PlanTypeDTO> LoadAllRecommendationPlanTypes();


        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Duration types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<DurationTypeDTO> LoadAllRecommendationDurationTypes();

        ///// <summary>
        ///// Author Mansoor 
        ///// Date 22 Mar 2011
        ///// This Method Will be used to load Duration types 
        ///// </summary>
        ///// <returns></returns>
        //[OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        //List<RecommendationLinksTypeDTO> LoadRecommendationLinksAutoCompleteByClinicId(int clinicBranchId, string searchCriteria);

        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Method types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<MethodTypeDTO> LoadAllRecommendationMethodTypes();



        /// <summary>
        /// Author Ismail 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Social Marital Status types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<SocialHistoryUsageAmountTypeDTO> LoadSocialHistoryUsageAmountTypes();


        /// <summary>
        /// Author Ismail 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Social Marital Status types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<SocialHistoryMediumTypeDTO> LoadSocialHistoryMediumTypes();

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 22 March 2011
        /// This Method Will be used to load Physical Main Category
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PhysicalMainCategoryTypeDTO> LoadPhysicalMainCategoryTypes(bool IsMale);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 22 March 2011
        /// This Method Will be used to load Physical Sub Category
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PhysicalSubCategoryTypeDTO> LoadPhysicalSubCategoryTypes();


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 July 2011
        /// This Method Will be used to load Physical Sub Category
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PhysicalCategoryTypeDTO> LoadPhysicalCategory();

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 July 2011
        /// This Method Will be used to load Physical Sub Category
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]		
        List<PhysicalCategoryTypeDTO> LoadPhysicalCategoryTypesByGender(bool LoadUniversal,bool LoadMaleOnly,bool LoadFemaleOnly);


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 22 March 2011
        /// This Method Will be used to load lab LIONIC Test Types
        /// /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<LIONICLabTestTypeDTO> LoadLIONICLabTestType(int LabtestTypeID,string LabTestCodeOrName);
        //List<LIONICLabTestTypeDTO> LoadLIONICLabTestType();

        /// <summary>
        /// Author Mansoor
        /// Date 25 March 2011
        /// This Method Will be used to load FollowUp Method Type
        /// /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<FollowUpMethodTypeDTO> LoadFollowUpMethodTypes();


        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// Method Load In House Lab Types By Clinic branch Id
        /// </summary>
        /// <param name="ClinicBranchId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<InHouseLabTestTypeDTO> LoadInHouseLabTestTypeByClinicBranchId(int ClinicBranchId);

        /// <summary>
        /// Author Ismail 
        /// Date 06 april 2011
        /// Method Load CPT Medical Types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<CPTMedicalTypeDTO> LoadCPTMedicalTypes(String CPTName);


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 07 April 2011
        /// This Method Will be used to load ROS  Categories :)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ROSDTO> LoadROSCategoryTypes(bool IsMale);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 07 April 2011
        /// This Method Will be used to load ROS  Categories :)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PhysicalsDTO> LoadPhysicalCategoryTypes(bool IsMale);

        /// <summary>
        /// Atta Baloch 
        /// Date 13 April 2011
        /// This Method Will be used to load Duration types (Strings)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<string> LoadRecommendationLinksAutoCompleteByClinicId(int clinicBranchId, string searchCriteria);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 15 July 2011
        /// This Method Will be used to load ROS Sub Level Categories
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ROSSubLevelCategoryTypeDTO> LoadROSSubLevelCategoryTypes(int LoadROSSubLevelCategoryTypes);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 July 2011
        /// This Method Will be used to load ROS Sub Level Categories By Gender
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ROSSubLevelCategoryTypeDTO> LoadROSSubLevelCategoryTypesByGender(bool LoadUniversal, bool LoadMaleOnly, bool LoadFemaleOnly, int ROSSubCategoryTypeID);

         /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 July 2011
        /// This Method Will be used to load Physical Sub Category
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]		
        List<PhysicalLevel3CategoryTypeDTO> LoadPhysical3LevelCategoryTypes(int PhysicalSubCategoryTypeID);
    }
}
