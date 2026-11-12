using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuivusManager.Buisness.UserBL;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.Buisness.UserBL.Impl;
using System.ServiceModel;

using Quivus.Utility.Logger;
using QuivusManager.Buisness.Utility;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.StaticData;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Image;
using QuivusManager.Buisness.StaticDataBL;
using QuivusManager.Buisness.StaticDataBL.Impl;
using QuivusManager.ServiceResponse.ServiceCustomResponse.AddressBook;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Allergy;
using QuivusManager.ServiceResponse.ServiceCustomResponse.ICDCode;

namespace QuivusManagerWebService
{

    public partial class QuivusManagerWebService
    {
        IStaticDataBL StaticDataBL = null;

        /// <summary>
        /// Author Mansoor
        /// Date 23 Aug 2011
        /// Return all static data required for registeration
        /// </summary>
        /// <returns></returns>
        public RegistrationStaticDataResponse GetRegisterationStaticData()
        {
            RegistrationStaticDataResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetRegisterationStaticData Called");

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.GetRegisterationStaticData();

                    if (response != null && response.ResponseData != null)
                    {
                        QApplicationDataCache.RELATIONSHIP_INFO_CACHE_DATA = response.ResponseData.RelationshipTypes;
                        QApplicationDataCache.GENDER_INFO_CACHE_DATA = response.ResponseData.GenderTypes;
                    }
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new RegistrationStaticDataResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
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
        /// Date 23 Aug 2011
        /// </summary>
        /// <returns></returns>
        public AddressBookStaticDataResponse GetAddressBookStaticData()
        {
            AddressBookStaticDataResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetAddressBookStaticData Called");

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.GetAddressBookStaticData();
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new AddressBookStaticDataResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
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
        /// Date 23 Aug 2011
        /// Method Used To load Image Types
        /// </summary>
        /// <returns></returns>
        public ImageTypeResponse LoadImageTypes()
        {
            ImageTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImageTypes Called");
                // QuivusLogger.Info("GetRegisterationStaticDataByType PARAM typeList::" + typeList);

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadImageTypes();
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ImageTypeResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
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
        /// Date 7 Sep 2011
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        public CityResponse GetCityListByState(int stateId)
        {
            CityResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetCityListByState Called");
                QuivusLogger.Info("GetCityListByState PARAM stateId::" + stateId);

                // if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.GetCityListByState(stateId);
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new CityResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
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
        /// Date 7 Sep 2011
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public ZipCodeResponse GetZipCodesByStateIdAndCityName(int stateId, string cityName)
        {
            ZipCodeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetZipCodesByStateIdAndCityName Called");
                QuivusLogger.Info("GetZipCodesByStateIdAndCityName PARAM stateId::" + stateId + "cityName::" + cityName);

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.GetZipCodesByStateIdAndCityName(stateId, cityName);
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ZipCodeResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
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
        /// Date 7 Sep 2011
        /// To fetch the list of 
        /// </summary>
        /// <param name="allergyName"></param>
        /// <returns></returns>
        public AllergyTypeResponse SearchAllergyTypeByName(string allergyName)
        {
            AllergyTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SearchAllergyTypeByName Called");
                QuivusLogger.Info("SearchAllergyTypeByName PARAM allergyName::" + allergyName);

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.SearchAllergyTypeByName(allergyName);
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new AllergyTypeResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
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
        /// Date 7 Sep 2011
        /// search the matching icd codes and gets the description, on the bases of icd code provided
        /// </summary>
        /// <param name="icdCode"></param>
        /// <returns></returns>
        public ICDCodeResponse SearchICDCodeByCode(string icdCode)
        {
            ICDCodeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SearchICDCodeByCode Called");
                QuivusLogger.Info("SearchICDCodeByCode PARAM icdCode::" + icdCode);

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.SearchICDCodeByCode(icdCode);
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ICDCodeResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.UserManagerService.QuivusFault> exp)
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