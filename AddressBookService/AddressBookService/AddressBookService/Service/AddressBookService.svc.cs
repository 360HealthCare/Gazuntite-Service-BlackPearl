using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Security.Permissions;
using System.Data;
using System.Data.Common;

using Quivus.Model.Common;
// using AddressBookService.Fault;
using Quivus.Utility.Logger;
using AddressBookService.Business.Common;
using Quivus.Model.AddressBook;
using AddressBookService.Business.AddressBook;
using AddressBookService.Business.AddressBook.Impl;

using Quivus.Utility.CustomException;
using QuivusUtility.Utilities;
using QuivusModel.AddressBook;

namespace AddressBookService.Service
{
    //[ExceptionShielding("AddressBookServicePolicy")]
    public class AddressBookService : QuivusBaseService, IAddressBookService
    {

        IAddressBookManager objABManager = null;

        // to cache the static data
        private static AddressBookStaticDataDTO objCacheAddressBookStaticDataDTO = null;

        //IAuthentication UserAuthenticationImpl = null;

        #region Default Implementation of GetServiceVersion
        public override string GetServiceVersion()
        {
            string strVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return strVersion;
        }
        #endregion

        /// <summary>
        /// Author Mansoor
        /// Date 03 Feb 2011
        /// Get user primary phone by guid
        /// </summary>        
        /// <param name="stateId"></param>
        /// <returns></returns>
        public PhoneDTO GetPartyPrimaryPhoneNoByRegGuid(Guid guid)
        {
            PhoneDTO response = null;
            try
            {
                if (objABManager == null)
                {
                    objABManager = new AddressBookManagerImpl();
                }
                response = objABManager.GetPartyPrimaryPhoneNoByRegGuid(guid);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (DTOConversionException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.InnerException.ToString(),exp.Code);
            }   
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }            
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 20 Jan 2011
        /// return address book static data used in registeration
        /// </summary>
        /// <returns></returns>
        public AddressBookStaticDataDTO GetAddressBookStaticData()
        {
            AddressBookStaticDataDTO objAddressBookStaticData = null;
            try
            {
                AddressBookStaticDataManager objABManager = new AddressBookStaticDataManager();

                //if (UserAuthenticationImpl == null)
                //{
                //    UserAuthenticationImpl = new AuthenticationImpl();
                //}

                if (objCacheAddressBookStaticDataDTO != null)
                {
                    objAddressBookStaticData = objCacheAddressBookStaticDataDTO;
                }
                else
                {
                    objAddressBookStaticData = objABManager.GetAllAddressBookStaticData();

                    if (objAddressBookStaticData != null)
                    {
                        objCacheAddressBookStaticDataDTO = objAddressBookStaticData;
                    }
                }
            }           
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }   
            return objAddressBookStaticData;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 20 Jan 2011
        /// return list of cities by state id
        /// </summary>        
        /// <param name="stateId"></param>
        /// <returns></returns>
        public List<CityDTO> ListCities(int stateId)
        {
            List<CityDTO> response = null;
            try
            {
                if (objABManager == null)
                {
                    objABManager = new AddressBookManagerImpl();
                }
                response = objABManager.ListCities(stateId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }   
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 20 Jan 2011
        /// return list of zip codes by state id and state name
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public List<ZipCodeDTO> ListZipCodesByStateIdAndCityName(int stateId, string cityName)
        {
            List<ZipCodeDTO> response = null;
            try
            {
                if (objABManager == null)
                {
                    objABManager = new AddressBookManagerImpl();
                }
                response = objABManager.ListZipCodesByStateAndCity(stateId, cityName);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }   
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 20 Jan 2011
        /// Save the address book
        /// <param name="objAddBookDTO"></param>
        /// <returns></returns>       
        public int SaveUserAddressBook(int managedByUserId, AddressBookDTO objAddBookDTO)
        {
            int response = 0;
            try
            {
                if (objABManager == null)
                {
                    objABManager = new AddressBookManagerImpl();
                }
                response = objABManager.SaveUserAddressBook(managedByUserId, objAddBookDTO);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }   
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 09 Feb 2011
        /// Get user address book by guid
        /// </summary>        
        /// <param name="stateId"></param>
        /// <returns></returns>
        public AddressBookDTO LoadAddressBookByPartyRegGuid(Guid guid)
        {
            AddressBookDTO response = null;
            try
            {
                if (objABManager == null)
                {
                    objABManager = new AddressBookManagerImpl();
                }
                response = objABManager.GetAddressBookByPartyRegGuid(guid);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }   
            return response;
        }

        /// <summary>
        /// NOT IN USE
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>

        private int GetStateIdByZIPCode(string zipCode)
        {
            int response = 0;
            try
            {
                if (objABManager == null)
                {
                    objABManager = new AddressBookManagerImpl();
                }
                response = objABManager.GetStateIdByZIPCode(zipCode);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }      
            return response;
        }

        /// <summary>
        /// NOT IN USE
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        private List<ZipCodeDTO> ListZipCodes(string city)
        {
            List<ZipCodeDTO> response = null;
            try
            {
                if (objABManager == null)
                {
                    objABManager = new AddressBookManagerImpl();
                }
                response = objABManager.ListZipCodes(city);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }     
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 28 Feb 2011
        /// load users address primary fields for tagging/customization
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="settingTypeId"></param>
        /// <returns></returns>        
        public string LoadUserProfileSettingsAddressByRegGuidAndType(Guid guid, int settingTypeId)
        {
            string response = null;
            try
            {
                if (objABManager == null)
                {
                    objABManager = new AddressBookManagerImpl();
                }
                response = objABManager.LoadUserProfileSettingsAddressByRegGuidAndType(guid, settingTypeId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 28 Feb 2011
        /// load users phone fields for tagging/customization
        /// </summary>       
        /// <param name="guid"></param>
        /// <param name="partyTypeId"></param>
        /// <param name="settingTypeId"></param>
        /// <returns></returns>                
        public string LoadPartyProfileSettingsPhoneByRegGuidAndType(Guid guid, int ownerTypeId)
        {
            string response = null;
            try
            {
                if (objABManager == null)
                {
                    objABManager = new AddressBookManagerImpl();
                }
                response = objABManager.LoadPartyProfileSettingsPhoneByRegGuidAndType(guid, ownerTypeId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        public List<PatientAddressBookInfoDTO> LoadPatientAddressBookByPartyRegGuid(List<Guid> GuidList)
        {
            List<PatientAddressBookInfoDTO> response = null;
            try
            {
                if (objABManager == null)
                {
                    objABManager = new AddressBookManagerImpl();
                }
                response = objABManager.LoadPatientAddressBookByPartyRegGuid(GuidList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }
    }
}
