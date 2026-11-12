using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.StaticData;
using QuivusManager.ServiceResponse.ServiceCustomResponse.AddressBook;

using QuivusManager.ServiceResponse.ServiceCustomResponse.ICDCode;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Image;
using QuivusManager.Buisness.StaticDataBL;
using QuivusManager.Buisness.StaticDataBL.Impl;
using System.ServiceModel;
using QuivusManager.Buisness.Utility;
using Quivus.Utility.Logger;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Notes;

using QuivusManager.ServiceResponse.ServiceCustomSession;
using Quivus.Utility.CustomException;
using QuivusManager.Buisness.ApplicationSession;
using Quivus.Utility;
using QuivusManager.IntegratedServies.VisitService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Labs;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Allergy;


namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    public partial class QuivusManagerService
    {
        IStaticDataBL StaticDataBL = null;
        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 17 Jan 2011
        /// This Method Is Used To Load All Search Engine List
        /// </summary>
        public SearchEngineResponse LoadAllSearchEngines(UserSessionDTO userSession)
        {
            SearchEngineResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadAllSearchEngines Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadAllSearchEngines();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new SearchEngineResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.UserManagerService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new SearchEngineResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 19 Jan 2011
        /// Return all static data required for registeration
        /// </summary>
        /// <returns></returns>
        public RegistrationStaticDataResponse GetRegisterationStaticData(UserSessionDTO userSession)
        {
            RegistrationStaticDataResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetRegisterationStaticData Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.GetRegisterationStaticData();

                    if (response != null && response.ResponseData !=null)
                    {
                        QApplicationDataCache.RELATIONSHIP_INFO_CACHE_DATA = response.ResponseData.RelationshipTypes;
                        QApplicationDataCache.GENDER_INFO_CACHE_DATA = response.ResponseData.GenderTypes;
                    }
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new RegistrationStaticDataResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.UserManagerService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new RegistrationStaticDataResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 20 Jan 2011
        /// </summary>
        /// <returns></returns>
        public AddressBookStaticDataResponse GetAddressBookStaticData(UserSessionDTO userSession)
        {
            AddressBookStaticDataResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetAddressBookStaticData Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.GetAddressBookStaticData();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new AddressBookStaticDataResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.AddressBookService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new AddressBookStaticDataResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 19 JAn 2011
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        public CityResponse GetCityListByState(int stateId, UserSessionDTO userSession)
        {
            CityResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetCityListByState Called");
                QuivusLogger.Info("GetCityListByState PARAM stateId::" + stateId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.GetCityListByState(stateId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new CityResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.AddressBookService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new CityResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 20 Jan 2011
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public ZipCodeResponse GetZipCodesByStateIdAndCityName(int stateId, string cityName, UserSessionDTO userSession)
        {
            ZipCodeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetZipCodesByStateIdAndCityName Called");
                QuivusLogger.Info("GetZipCodesByStateIdAndCityName PARAM stateId::" + stateId + "cityName::" + cityName);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.GetZipCodesByStateIdAndCityName(stateId, cityName);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ZipCodeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.UserManagerService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ZipCodeResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 21 JAn 2011
        /// To fetch the list of 
        /// </summary>
        /// <param name="allergyName"></param>
        /// <returns></returns>
        public AllergyTypeResponse SearchAllergyTypeByName(string allergyName, UserSessionDTO userSession)
        {
            AllergyTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SearchAllergyTypeByName Called");
                QuivusLogger.Info("SearchAllergyTypeByName PARAM allergyName::" + allergyName);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.SearchAllergyTypeByName(allergyName);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new AllergyTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.UserManagerService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new AllergyTypeResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 21 Jan 2011
        /// search the matching icd codes and gets the description, on the bases of icd code provided
        /// </summary>
        /// <param name="icdCode"></param>
        /// <returns></returns>
        public ICDCodeResponse SearchICDCodeByCode(string icdCode, UserSessionDTO userSession)
        {
            ICDCodeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SearchICDCodeByCode Called");
                QuivusLogger.Info("SearchICDCodeByCode PARAM icdCode::" + icdCode);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.SearchICDCodeByCode(icdCode);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ICDCodeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.UserManagerService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ICDCodeResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 19 Jan 2011
        /// Return static data required for registeration, based on type provided
        /// </summary>
        /// <returns></returns>
        public RegistrationStaticDataResponse GetRegisterationStaticDataByType(List<string> typeList, UserSessionDTO userSession)
        {
            RegistrationStaticDataResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetRegisterationStaticDataByType Called");
                QuivusLogger.Info("GetRegisterationStaticDataByType PARAM typeList::" + typeList);

                if (typeList != null)
                    QuivusLogger.Info("List Count PARAM typeList::" + typeList.Count);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.GetRegisterationStaticDataByType(typeList);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new RegistrationStaticDataResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.UserManagerService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new RegistrationStaticDataResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// Method Used To Load Profile Static Information
        /// </summary>
        /// <returns></returns>
        public ProfileStaticDataResponse LoadProfileStaticData(UserSessionDTO userSession)
        {
            ProfileStaticDataResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadProfileStaticData Called");
               // QuivusLogger.Info("GetRegisterationStaticDataByType PARAM typeList::" + typeList);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadProfileStaticData();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ProfileStaticDataResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.UserManagerService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ProfileStaticDataResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;           
        }

        /// <summary>
        /// Author Muhammad ismail
        /// Date Feb 11 2011
        /// Method Used To load Image Types
        /// </summary>
        /// <returns></returns>
        public ImageTypeResponse LoadImageTypes(UserSessionDTO userSession)
        {
            ImageTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImageTypes Called");
                // QuivusLogger.Info("GetRegisterationStaticDataByType PARAM typeList::" + typeList);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadImageTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ImageTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.UserManagerService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ImageTypeResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get the module types for notes etc
        /// </summary>      
        //public ModuleTypeResponse LoadModuleTypes(UserSessionDTO userSession)
        //{
        //    ModuleTypeResponse response = null;
        //    try
        //    {
        //        QuivusLogger.Info("In QuivusManagerService LoadAllModuleTypes Called");
        //        // QuivusLogger.Info("GetRegisterationStaticDataByType PARAM typeList::" + typeList);

        //        //TEMP
        //        //throw new InvalidSessionException(ProjectConstant.EXPIRED_USER_SESSION_ID, (int)ProjectConstant.ERROR_CODE.InvalidSession);
               
        //        if (ApplicationSessionHandler.IsValidUserSession(userSession))
        //        {
        //            if (StaticDataBL == null)
        //            {
        //                StaticDataBL = new StaticDataBLImpl();
        //            }
        //            response = StaticDataBL.LoadAllModuleTypes();
        //        }
        //    }
        //    catch (InvalidSessionException exp)
        //    {
        //        QuivusLogger.Error(exp);
        //        if (response == null)
        //            response = new ModuleTypeResponse();

        //        ResponseErrorHandler.HandleException(response, exp, exp.Code);
        //    }
        //    catch (FaultException<QuivusManager.IntegratedServies.UserManagerService.QuivusFault> exp)
        //    {
        //        QuivusLogger.Error(exp);

        //        if (response == null)
        //            response = new ModuleTypeResponse();
        //        ResponseErrorHandler.HandleException(response, exp);
        //    }
        //    return response;
        //}

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to load Disease Types 
        /// </summary>
        /// <returns></returns>
        public DiseaseTypeResponse LoadDiseaseTypes(int ClinicBranchID, int FamilyHistoryTypeID, UserSessionDTO userSession)
        {
            DiseaseTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadDiseaseTypes Called");
                
                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadDiseaseTypes(ClinicBranchID, FamilyHistoryTypeID);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new DiseaseTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new DiseaseTypeResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab nature Types 
        /// </summary>
        /// <returns></returns>
        public LabNatureTypeResponse LoadLabNatureTypes(UserSessionDTO userSession)
        {
            LabNatureTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadLabNatureTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadLabNatureTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new LabNatureTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new LabNatureTypeResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }




        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab priority Types 
        /// </summary>
        /// <returns></returns>
        public LabTestPriorityTypeResponse LoadLabTestPriorityTypes(UserSessionDTO userSession)
        {
            LabTestPriorityTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadLabTestPriorityTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadLabTestPriorityTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new LabTestPriorityTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new LabTestPriorityTypeResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab specimen Types 
        /// </summary>
        /// <returns></returns>
        public LabTestSpecimenTypeResponse LoadLabTestSpecimenTypes(UserSessionDTO userSession)
        {
            LabTestSpecimenTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadLabTestSpecimenTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadLabTestSpecimenTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new LabTestSpecimenTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new LabTestSpecimenTypeResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab test Types 
        /// </summary>
        /// <returns></returns>
        public LabTestTypeResponse LoadLabTestTypes(UserSessionDTO userSession)
        {
            LabTestTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadLabTestTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadLabTestTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new LabTestTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new LabTestTypeResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab  Types 
        /// </summary>
        /// <returns></returns>
        public LabTypeResponse LoadLabTypes(UserSessionDTO userSession)
        {
            LabTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadLabTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadLabTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new LabTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new LabTypeResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab test order method Types 
        /// </summary>
        /// <returns></returns>
        public LabTestOrderMethodTypeResponse LoadLabTestOrderMethodTypes(UserSessionDTO userSession)
        {
            LabTestOrderMethodTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadLabTestOrderMethodTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadLabTestOrderMethodTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new LabTestOrderMethodTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new LabTestOrderMethodTypeResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load ICD Code by String 
        /// </summary>
        /// <returns></returns>
        public ICDCodeResponse LoadICDCodeByName(UserSessionDTO userSession,string queryString)
        {
            ICDCodeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadICDCodeByName Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadICDCodeByName(queryString,userSession.ClinicBranchID);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ICDCodeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ICDCodeResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }
    }
}