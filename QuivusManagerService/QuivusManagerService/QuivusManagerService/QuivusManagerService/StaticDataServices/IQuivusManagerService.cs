using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.StaticData;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.AddressBook;

using QuivusManager.ServiceResponse.ServiceCustomResponse.Image;
using QuivusManager.ServiceResponse.ServiceCustomResponse.ICDCode;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Notes;
using QuivusManager.ServiceResponse.ServiceCustomSession;
using QuivusManager.IntegratedServies.VisitService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Labs;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Allergy;


namespace QuivusManagerService.QuivusManagerService.ManagerService
{

    public partial interface IQuivusManagerService
    {
        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 17 Jan 2011
        /// This Method Is Used To Load All Search Engine List
        /// </summary>
        [OperationContract]         
        SearchEngineResponse LoadAllSearchEngines(UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 19 Jan 2011
        /// Return all static data required for registeration
        /// </summary>
        /// <returns></returns>
        [OperationContract]         
        RegistrationStaticDataResponse GetRegisterationStaticData(UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 20 Jan 2011
        /// </summary>
        /// <returns></returns>
        [OperationContract]         
        AddressBookStaticDataResponse GetAddressBookStaticData(UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 19 JAn 2011
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        [OperationContract]
        CityResponse GetCityListByState(int stateId, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 20 Jan 2011
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        [OperationContract]
        ZipCodeResponse GetZipCodesByStateIdAndCityName(int stateId, string cityName, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 21 JAn 2011
        /// To fetch the list of 
        /// </summary>
        /// <param name="allergyName"></param>
        /// <returns></returns>
        [OperationContract]
        AllergyTypeResponse SearchAllergyTypeByName(string allergyName, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Jan 2011
        /// search the matching icd codes and gets the description, on the bases of icd code provided
        /// </summary>
        /// <param name="icdCode"></param>
        /// <returns></returns>
        [OperationContract]
        ICDCodeResponse SearchICDCodeByCode(string icdCode, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 19 Jan 2011
        /// Return static data required for registeration, based on type provided
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        RegistrationStaticDataResponse GetRegisterationStaticDataByType(List<string> typeList, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// Method Used To Load Profile Static Information
        /// </summary>
        /// <returns></returns>
        [OperationContract]         
        ProfileStaticDataResponse LoadProfileStaticData(UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad ismail
        /// Date Feb 11 2011
        /// Method Used To load Image Types
        /// </summary>
        /// <returns></returns>
        [OperationContract]         
        ImageTypeResponse LoadImageTypes(UserSessionDTO userSession);


        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get the module types for notes etc
        /// </summary>
      //  [OperationContract]
     //   ModuleTypeResponse LoadModuleTypes(UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to load Disease Types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DiseaseTypeResponse LoadDiseaseTypes(int ClinicBranchID, int FamilyHistoryTypeID, UserSessionDTO userSession);


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab nature Types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        LabNatureTypeResponse LoadLabNatureTypes(UserSessionDTO userSession);




        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab priority Types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        LabTestPriorityTypeResponse LoadLabTestPriorityTypes(UserSessionDTO userSession);


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab specimen Types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        LabTestSpecimenTypeResponse LoadLabTestSpecimenTypes(UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab test Types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        LabTestTypeResponse LoadLabTestTypes(UserSessionDTO userSession);


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab  Types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        LabTypeResponse LoadLabTypes(UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab test order method Types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        LabTestOrderMethodTypeResponse LoadLabTestOrderMethodTypes(UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load ICD Code by String 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ICDCodeResponse LoadICDCodeByName(UserSessionDTO userSession,string queryString);
    }
}
