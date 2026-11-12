using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

using QuivusManager.ServiceResponse.ServiceCustomResponse.Visit;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User;
using QuivusManager.IntegratedServies.VisitService;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Notes;
using QuivusManager.ServiceResponse.ServiceCustomSession;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Medical;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Family;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Vital;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Labs;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Immunization;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.ROS;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.HPI;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Social;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Recommendation;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Physicals;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.FollowUp;
using QuivusManager.ServiceResponse.ServiceCustomResponse.StaticData;

namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    public partial interface IQuivusManagerService
    {
        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the encounter types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        EncounterTypeResponse LoadEncounterTypes(UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the template types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        SoapTemplateResponse LoadSoapTemplateByClinicBranchId(int clinicBranchId, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used to load Medical History Types Used 
        /// Under Comprehensive History Section
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        MedicalHistoryTypeResponse LoadMedicalHistoryTypes(UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to load Family History Types Used 
        /// Under Comprehensive History Section
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        FamilyHistoryTypeResponse LoadFamilyHistoryTypes(UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization types static data
        /// </summary>        
        /// <returns></returns>     
        [OperationContract]
        ImmunizationTypesResponse LoadImmunizationTypes(UserSessionDTO userSession);


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 March 2011
        /// This Method Will be used to load ROS Main Category
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ROSMainCategoryTypeResponse LoadROSMainCategoryTypes(bool IsMale,UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 March 2011
        /// This Method Will be used to load ROS Sub Category
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ROSSubCategoryTypeResponse LoadROSSubCategoryTypes(UserSessionDTO userSession);


        //Commented by Mansoor
        ///// <summary>
        ///// Author Muhammad Ismail 
        ///// Date 21 March 2011
        ///// This Method Will be used to load HPI Data Control Types
        ///// </summary>
        ///// <returns></returns>
        //[OperationContract]
        //HPIDataControlTypeResponse LoadHPIDataControlTypes(UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 21 March 2011
        /// This Method Will be used to load HPI detail types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        HPIDetailTypeResponse LoadHPIDetailTypes(UserSessionDTO userSession);


        /// <summary>
        /// Author Ismail 
        /// Date 21 Mar 2011
        /// This Method Will be used to load Social Addication types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SocialHistoryAddictionTypeResponse LoadSocialHistoryAddictionTypes(UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail 
        /// Date 21 Mar 2011
        /// This Method Will be used to load Social Marital Status types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SocialMaritalStatusTypeResponse LoadMaritalStatusTypes(UserSessionDTO userSession);


        /// <summary>
        /// Author Ismail 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Social Marital Status types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SocialHistoryUsageAmountTypeResponse LoadSocialHistoryUsageAmountTypes(UserSessionDTO userSession);


        /// <summary>
        /// Author Ismail 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Social Marital Status types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SocialHistoryMediumTypeResponse LoadSocialHistoryMediumTypes(UserSessionDTO userSession);


        /// <summary>
        /// Author Mansoor
        /// Date 22 Mar 2011
        /// This Method Will be used to load Plan types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PlanTypeResponse LoadAllRecommendationPlanTypes(UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Duration types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DurationTypeResponse LoadAllRecommendationDurationTypes(UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Method types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        MethodTypeResponse LoadAllRecommendationMethodTypes(UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 22 March 2011
        /// This Method Will be used to load Physical Main Category
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PhysicalMainCategoryTypeResponse LoadPhysicalMainCategoryTypes(bool IsMale, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 22 March 2011
        /// This Method Will be used to load Physical Sub Category
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PhysicalSubCategoryTypeResponse LoadPhysicalSubCategoryTypes(UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 22 March 2011
        /// This Method Will be used to load lab LIONIC Test Types
        /// /// </summary>
        /// <returns></returns>
        [OperationContract]
        LIONICLabTestTypeResponse LoadLIONICLabTestType(int LabtestTypeID, string LabTestCodeOrName, UserSessionDTO userSession);


        /// <summary>
        /// Author Mansoor 
        /// Date 25 March 2011
        /// This Method Will be used to load Follow Up Method
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        FollowUpMethodTypesResponse LoadFollowUpMethodType(UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// Method Load In House Lab Types By Clinic branch Id
        /// </summary>
        /// <param name="ClinicBranchId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        InHouseLabTestTypeResponse LoadInHouseLabTestTypeByClinicBranchId(int ClinicBranchId, UserSessionDTO userSession);


        /// <summary>
        /// Author Ismail 
        /// Date 06 april 2011
        /// Method Load CPT Medical Types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        CPTMedicalTypeResponse LoadCPTMedicalTypes(String CPTName, UserSessionDTO userSession);


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 07 April 2011
        /// This Method Will be used to load ROS  Categories :)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientROSResponse LoadROSCategoryTypes(bool IsMale, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 07 April 2011
        /// This Method Will be used to load ROS  Categories :)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientPhysicalResponse LoadPhysicalCategoryTypes(bool IsMale, UserSessionDTO userSession);

         /// <summary>
        /// Author Atta
        /// Date 14 April 2011        
        /// </summary>
        /// <returns></returns>
        //[OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        ServiceTypeResponse LoadServiceType(int ClinicBRanchID, UserSessionDTO userSession);

        /// <summary>
        /// Author Atta
        /// Date 14 April 2011
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        InstructionDurationTypeResponse LoadInstructionDurationType(UserSessionDTO userSession);

        /// <summary>
        /// Author Atta
        /// Date 14 April 2011
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        InstructionAgeTypeResponse LoadInstructionAgeType(UserSessionDTO userSession);

         /// <summary>
        /// Author Atta
        /// Date 14 April 2011
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        InstructionAgeValueTypeResponse LoadInstructionAgeValueType(UserSessionDTO userSession);


        /// <summary>
        /// Author Ismail 
        /// Date 4 may 2011
        /// This Method Will be used to load module types Types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ModuleTypeResponse LoadAllModuleTypes(UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 July 2011
        /// This Method Will be used to load Physical  Category
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PhysicalCategoryTypeResponse LoadPhysicalCategoryType(UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 July 2011
        /// This Method Will be used to load Physical  Category
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PhysicalCategoryTypeResponse LoadPhysicalCategoryTypesByGender(bool LoadUniversal, bool LoadMaleOnly, bool LoadFemaleOnly, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 15 July 2011
        /// This Method Will be used to load ROS Sub Level Categories
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ROSSubLevelCategoryTypeResponse LoadROSSubLevelCategoryTypes(int SubCategoryId,UserSessionDTO userSession);


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 July 2011
        /// This Method Will be used to load ROS Sub Level Categories By Gender
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        ROSSubLevelCategoryTypeResponse LoadROSSubLevelCategoryTypesByGender(bool LoadUniversal, bool LoadMaleOnly, bool LoadFemaleOnly, int ROSSubCategoryTypeID, UserSessionDTO userSession);


        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 08/04/2011
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        FollowupDataControlTypeResponse LoadFollowupDataControlType(UserSessionDTO userSession);

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 08/04/2011
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        ServiceTypeResponse LoadPatientFollowupAutoCompleteByTypeIdAndClinicId(int followupDataControlTypeID, int clinicBranchId, UserSessionDTO userSession);

    }
}