using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.StaticData;

using QuivusManager.ServiceResponse.ServiceCustomResponse.AddressBook;
using QuivusManager.ServiceResponse.ServiceCustomResponse.ICDCode;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Notes;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Labs;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Allergy;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Visit;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Immunization;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Medical;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Family;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.ROS;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.HPI;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Social;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Recommendation;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Physicals;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.FollowUp;

namespace QuivusManager.Buisness.StaticDataBL
{
    public interface IStaticDataBL
    {
        SearchEngineResponse LoadAllSearchEngines();


        RegistrationStaticDataResponse GetRegisterationStaticData();

        AllergyTypeResponse SearchAllergyTypeByName(string allergyName);

       CityResponse GetCityListByState(int stateId);

       AddressBookStaticDataResponse GetAddressBookStaticData();

       ZipCodeResponse GetZipCodesByStateIdAndCityName(int stateId, string cityName);

       ICDCodeResponse SearchICDCodeByCode(string icdCode);

       RegistrationStaticDataResponse GetRegisterationStaticDataByType(List<string> typeList);

       ProfileStaticDataResponse LoadProfileStaticData();

       ServiceResponse.ServiceCustomResponse.Image.ImageTypeResponse LoadImageTypes();

       /// <summary>
       /// Author Mansoor
       /// Date 22 Feb 2011
       /// get the module types for notes etc
       /// </summary> 
     //  ModuleTypeResponse LoadAllModuleTypes();

       DiseaseTypeResponse LoadDiseaseTypes(int ClinicBranchID,int FamilyHistoryTypeID);

       LabTestTypeResponse LoadLabTestTypes();

       LabTestPriorityTypeResponse LoadLabTestPriorityTypes();

       LabNatureTypeResponse LoadLabNatureTypes();

       LabTestSpecimenTypeResponse LoadLabTestSpecimenTypes();

       LabTypeResponse LoadLabTypes();

       LabTestOrderMethodTypeResponse LoadLabTestOrderMethodTypes();

       ICDCodeResponse LoadICDCodeByName(string queryString);

       /// <summary>
       /// Author Mansoor
       /// Date 17 Feb 2011
       /// List all the encounter types
       /// </summary>
       /// <returns></returns>   
       EncounterTypeResponse LoadEncounterTypes();

       /// <summary>
       /// Author Mansoor
       /// Date 18 Feb 2011
       /// get immunization types static data
       /// </summary>        
       /// <returns></returns>     
       ImmunizationTypesResponse LoadImmunizationTypes();

       /// <summary>
       /// Author Muhammad Ismail 
       /// Date 11 March 2011
       /// This Method Will be used to load Medical History Types Used 
       /// Under Comprehensive History Section
       /// </summary>
       /// <returns></returns>
       MedicalHistoryTypeResponse LoadMedicalHistoryTypes();

       FamilyHistoryTypeResponse LoadFamilyHistoryTypes();

       ROSMainCategoryTypeResponse LoadROSMainCategoryTypes(bool IsMale);

       ROSSubCategoryTypeResponse LoadROSSubCategoryTypes();

      //Commented by Mansoor
      //HPIDataControlTypeResponse LoadHPIDataControlTypes();



       /// <summary>
       /// Author Mansoor
       /// Date 21 March 2011
       /// This Method Will be used to load HPI detail types 
       /// </summary>
       /// <returns></returns>  
       HPIDetailTypeResponse LoadHPIDetailTypes();


       SocialHistoryAddictionTypeResponse LoadSocialHistoryAddictionTypes();

       SocialMaritalStatusTypeResponse LoadMaritalStatusTypes();


       SocialHistoryUsageAmountTypeResponse LoadSocialHistoryUsageAmountTypes();

       SocialHistoryMediumTypeResponse LoadSocialHistoryMediumTypes();


       /// <summary>
       /// Author Mansoor
       /// Date 22 Mar 2011
       /// This Method Will be used to load Plan types 
       /// </summary>
       /// <returns></returns>     
       PlanTypeResponse LoadAllRecommendationPlanTypes();

       /// <summary>
       /// Author Mansoor 
       /// Date 22 Mar 2011
       /// This Method Will be used to load Duration types 
       /// </summary>
       /// <returns></returns>    
       DurationTypeResponse LoadAllRecommendationDurationTypes();

       /// <summary>
       /// Author Mansoor 
       /// Date 22 Mar 2011
       /// This Method Will be used to load Method types 
       /// </summary>
       /// <returns></returns>      
       MethodTypeResponse LoadAllRecommendationMethodTypes();


       PhysicalMainCategoryTypeResponse LoadPhysicalMainCategoryTypes(bool IsMale);

       PhysicalSubCategoryTypeResponse LoadPhysicalSubCategoryTypes();

       LIONICLabTestTypeResponse LoadLIONICLabTestType(int LabtestTypeID, string LabTestCodeOrName);

       /// <summary>
       /// Author Mansoor 
       /// Date 25 March 2011
       /// This Method Will be used to load Follow Up Method
       /// </summary>
       /// <returns></returns>  
       FollowUpMethodTypesResponse LoadFollowUpMethodType();

       InHouseLabTestTypeResponse LoadInHouseLabTestTypeByClinicBranchId(int ClinicBranchId);

       CPTMedicalTypeResponse LoadCPTMedicalTypes(String CPTName);

       PatientROSResponse LoadROSCategoryTypes(bool IsMale);

       PatientPhysicalResponse LoadPhysicalCategoryTypes(bool IsMale);

       InstructionAgeTypeResponse LoadInstructionAgeType();

       InstructionDurationTypeResponse LoadInstructionDurationType();

       ServiceTypeResponse LoadServiceType(int ClinicBranchID);

       InstructionAgeValueTypeResponse LoadInstructionAgeValueType();

       ModuleTypeResponse LoadModuleTypes();

       PhysicalCategoryTypeResponse LoadPhysicalCategoryType();

       PhysicalCategoryTypeResponse LoadPhysicalCategoryTypesByGender(bool LoadUniversal, bool LoadMaleOnly, bool LoadFemaleOnly);

       ROSSubLevelCategoryTypeResponse LoadROSSubLevelCategoryTypes(int SubCategoryId);

       ROSSubLevelCategoryTypeResponse LoadROSSubLevelCategoryTypesByGender(bool LoadUniversal, bool LoadMaleOnly, bool LoadFemaleOnly, int ROSSubCategoryTypeID);

       FollowupDataControlTypeResponse LoadFollowupDataControlType();

       ServiceTypeResponse LoadPatientFollowupAutoCompleteByTypeIdAndClinicId(int followupDataControlTypeID, int clinicBranchId);
        ICDCodeResponse LoadICDCodeByName(string queryString, int clinicBranchID);
    }
}
