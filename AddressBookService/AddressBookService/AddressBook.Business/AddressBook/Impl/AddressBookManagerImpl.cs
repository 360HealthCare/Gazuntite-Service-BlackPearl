using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Model.AddressBook;
using Quivus.Utility.Logger;
using AddressBookService.DataAccess;
using Quivus.Utility.Factory;
using Quivus.Utility.CustomException;
using Quivus.Utility;
using AddressBook.Business.Validator;
using QuivusModel.AddressBook;


namespace AddressBookService.Business.AddressBook.Impl
{
    /// <summary>
    /// Author Mansoor
    /// Date 18 Jan 2011   
    /// </summary>
    public class AddressBookManagerImpl : IAddressBookManager
    {
        AddressBookDAO objAddressBookDAO;

        //AddressPartyTypeDAO objAddressPartyDAO;
        //AddressBookDAO objAddressBookDAO;
        //AddressTypeDAO objAddressTypeDAO;
        //ZipCodeDAO objZipCodeDAO;

        public AddressBookManagerImpl()
        {
            objAddressBookDAO = (AddressBookDAO)DAOFactory.GetDataAccessObject(typeof(AddressBookDAO));

            //objAddressPartyDAO = new AddressPartyTypeDAO();
            //objAddressBookDAO = new AddressBookDAO();
            //objAddressTypeDAO = new AddressTypeDAO();
            //objZipCodeDAO = new ZipCodeDAO();
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Jan 2011 
        /// get the list of cites by state id
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        public List<CityDTO> ListCities(int stateId)
        {
            List<CityDTO> listofCities = null;
            try
            {
                QuivusLogger.Info("ListCities Called PARAM stateId:: " + stateId);
                if (stateId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.STATE_ID_INVALID_ERROR_MSG);
                }
                listofCities = objAddressBookDAO.ListCities(stateId);
            }            
            catch
            {                
                throw;
            }
            return listofCities;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Jan 2011 
        /// get the zip codes by city name and state id
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        public List<ZipCodeDTO> ListZipCodesByStateAndCity(int stateId, string city)
        {
            List<ZipCodeDTO> listofCities = null;
            try
            {
                QuivusLogger.Info("ListZipCodesByStateAndCity Called PARAM stateId:: " + stateId + "city:: " + city);
                if (stateId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.STATE_ID_INVALID_ERROR_MSG);
                }
                else if (string.IsNullOrEmpty(city))
                    throw new InvalidArgumentException(ProjectConstant.CITY_NAME_INVALID_ERROR_MSG);

                listofCities = objAddressBookDAO.ListZipCodesByStateAndCity(stateId, city);
            }
            catch
            {
                throw;
            }
            return listofCities;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Jan 2011
        /// get state id by zip code
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public int GetStateIdByZIPCode(string zipCode)
        {
            int StateID = 0;
            try
            {
                QuivusLogger.Info("GetStateIdByZIPCode Called PARAM zipCode:: " + zipCode);
                if (string.IsNullOrEmpty(zipCode))
                {
                    throw new InvalidArgumentException(ProjectConstant.ZIP_CODE_INVALID_ERROR_MSG);
                }
                //TODO:
                //M StateID = objAddressBookDAO.GetStateIdByZIPCode(zipCode);
            }
            catch
            {
                throw;
            }
            return StateID;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Jan 2011 
        /// get the zip codes by city name
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public List<ZipCodeDTO> ListZipCodes(string city)
        {
            List<ZipCodeDTO> listofZipCodes = null;
            try
            {
                QuivusLogger.Info("ListZipCodesByCityAndState Called PARAM city:: " + city);
                if (string.IsNullOrEmpty(city))
                {
                    throw new InvalidArgumentException(ProjectConstant.CITY_NAME_INVALID_ERROR_MSG);
                }
                //TODO:
                //M listofZipCodes = objAddressBookDAO.ListZipCodes(city);
            }
            catch
            {
                throw;
            }
            return listofZipCodes;
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
                QuivusLogger.Info("SaveUserAddressBook Called PARAM :: managedByUserId " + managedByUserId);

                StringBuilder sb = new StringBuilder();

                if (managedByUserId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }
                else if (QAddressBookValidator.ValidateAddressBook(objAddBookDTO, ref sb))
                {
                    response = objAddressBookDAO.ManageAddressBook(managedByUserId, objAddBookDTO);//SaveAddressBook(managedByUserId, objAddBookDTO);
                }
                else
                {
                    // Validation Gets Failed Thorw Exception
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /////

        //public int SaveAddressBook(AddressBookDTO objAddressBookDTO)
        //{
        //    return objAddressBookDAO.SaveAddressBookToDB(objAddressBookDTO);
        //}

        //public List<AddressPartyTypeDTO> GetAllAddressPartyTypes()
        //{

        //    AddressPartyTypeDAO objAddressPartyTypeDAO = new AddressPartyTypeDAO();
        //    List<AddressPartyTypeDTO> listAddressPartyTypeDTO = null;
        //    try
        //    {
        //        List<AddressPartyType> listAddressPartyType = objAddressPartyTypeDAO.GetAllAddressPartyTypes();

        //        if (listAddressPartyType != null && listAddressPartyType.Count > 0)
        //        {
        //            listAddressPartyTypeDTO = new List<AddressPartyTypeDTO>();
        //            AddressPartyTypeDTO objAddressPartyTypeDTO = null;
        //            foreach (AddressPartyType currObj in listAddressPartyType)
        //            {
        //                objAddressPartyTypeDTO = new AddressPartyTypeDTO();
        //                objAddressPartyTypeDTO.AddressPartyDescription = currObj.AddressPartyDescription;
        //                objAddressPartyTypeDTO.AddressPartyName = currObj.AddressPartyName;
        //                objAddressPartyTypeDTO.AddressPartyTypeID = currObj.AddressPartyTypeID;
        //                listAddressPartyTypeDTO.Add(objAddressPartyTypeDTO);
        //            }
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        _logger.HandleException(exp, exp.Message, true);
        //    }

        //    return listAddressPartyTypeDTO;
        //}

        //public AddressPartyTypeDTO GetAddressPartyTypeByName(string addressPartyTypeName)
        //{

        //    AddressPartyTypeDAO objAddressPartyTypeDAO = new AddressPartyTypeDAO();
        //    AddressPartyTypeDTO objAddressPartyTypeDTO = null;

        //    try
        //    {
        //        AddressPartyType objAddressPartyType = objAddressPartyTypeDAO.GetAddressPartyTypeByName(addressPartyTypeName);

        //        if (objAddressPartyType != null)
        //        {
        //            objAddressPartyTypeDTO = new AddressPartyTypeDTO();
        //            objAddressPartyTypeDTO.AddressPartyDescription = objAddressPartyType.AddressPartyDescription;
        //            objAddressPartyTypeDTO.AddressPartyName = objAddressPartyType.AddressPartyName;
        //            objAddressPartyTypeDTO.AddressPartyTypeID = objAddressPartyType.AddressPartyTypeID;
        //        }

        //    }
        //    catch (Exception exp)
        //    {
        //        _logger.HandleException(exp, exp.Message, true);
        //    }
        //    return objAddressPartyTypeDTO;
        //}

        //public List<AddressTypeDTO> GetAllAddressTypes()
        //{
        //    AddressTypeDAO objAddressTypeDAO = new AddressTypeDAO();
        //    List<AddressTypeDTO> listAddressTypeDTO = null;
        //    try
        //    {
        //        List<AddressType> listAddressType = objAddressTypeDAO.GetAllAddressTypes();

        //        if (listAddressType != null && listAddressType.Count > 0)
        //        {
        //            listAddressTypeDTO = new List<AddressTypeDTO>();
        //            AddressTypeDTO objAddressTypeDTO = null;
        //            foreach (AddressType currObj in listAddressType)
        //            {
        //                objAddressTypeDTO = new AddressTypeDTO();
        //                objAddressTypeDTO.AddressTypeDescription = currObj.AddressTypeDescription;
        //                objAddressTypeDTO.AddressTypeID = currObj.AddressTypeID;
        //                objAddressTypeDTO.AddressTypeName = currObj.AddressTypeName;
        //                listAddressTypeDTO.Add(objAddressTypeDTO);
        //            }
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        _logger.HandleException(exp, exp.Message, true);
        //    }

        //    return listAddressTypeDTO;
        //}

        //public List<PhoneNumberTypeDTO> GetAllPhoneNumberTypes()
        //{
        //    PhoneNumberTypeDAO objPhoneNumberTypeDAO = new PhoneNumberTypeDAO();
        //    List<PhoneNumberTypeDTO> listPhoneNumberTypeDTO = null;

        //    try
        //    {
        //        List<NumberType> listPhoneNumberType = objPhoneNumberTypeDAO.GetAllPhoneNumberTypes();

        //        if (listPhoneNumberType != null && listPhoneNumberType.Count > 0)
        //        {
        //            listPhoneNumberTypeDTO = new List<PhoneNumberTypeDTO>();
        //            PhoneNumberTypeDTO objPhoneNumberTypeDTO = null;
        //            foreach (NumberType currObj in listPhoneNumberType)
        //            {
        //                objPhoneNumberTypeDTO = new PhoneNumberTypeDTO();
        //                objPhoneNumberTypeDTO.Description = currObj.NumberTypeDescription;
        //                objPhoneNumberTypeDTO.Name = currObj.NumberTypeName;
        //                objPhoneNumberTypeDTO.PhoneNumberTypeID = currObj.NumberTypeID;
        //                objPhoneNumberTypeDTO.TypeCode = currObj.NumberTypeCode;
        //                listPhoneNumberTypeDTO.Add(objPhoneNumberTypeDTO);

        //            }
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        _logger.HandleException(exp, exp.Message, true);
        //    }
        //    return listPhoneNumberTypeDTO;
        //}

        //public List<OwnerTypeDTO> GetAllOwnerPartyTypes()
        //{
        //    List<OwnerTypeDTO> listOwnerTypeDTO = null;
        //    OwnerTypeDAO objOwnerTypeDAO = new OwnerTypeDAO();
        //    try
        //    {
        //        List<OwnerType> listOwnerType = objOwnerTypeDAO.GetAllOwnerTypes();

        //        if (listOwnerType != null && listOwnerType.Count > 0)
        //        {
        //            listOwnerTypeDTO = new List<OwnerTypeDTO>();
        //            OwnerTypeDTO objOwnerTypeDTO = null;
        //            foreach (OwnerType currObj in listOwnerType)
        //            {
        //                objOwnerTypeDTO = new OwnerTypeDTO();
        //                objOwnerTypeDTO.OwnerTypeDescription = currObj.OwnerTypeDescription;
        //                objOwnerTypeDTO.OwnerTypeID = currObj.OwnerTypeID;
        //                objOwnerTypeDTO.OwnerTypeName = currObj.OwnerTypeName;
        //                listOwnerTypeDTO.Add(objOwnerTypeDTO);
        //            }
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        _logger.HandleException(exp, exp.Message, true);
        //    }

        //    return listOwnerTypeDTO;
        //}

        //public List<PhoneTypeDTO> GetAllPhoneTypes()
        //{
        //    List<PhoneTypeDTO> listPhoneTypeDTO = null;
        //    PhoneTypeDAO objPhoneTypeDAO = new PhoneTypeDAO();
        //    try
        //    {
        //        List<PhoneType> listPhoneType = objPhoneTypeDAO.GetAllPhoneTypes();

        //        if (listPhoneType != null && listPhoneType.Count > 0)
        //        {
        //            listPhoneTypeDTO = new List<PhoneTypeDTO>();
        //            PhoneTypeDTO objPhoneTypeDTO = null;
        //            foreach (PhoneType currObj in listPhoneType)
        //            {
        //                objPhoneTypeDTO = new PhoneTypeDTO();
        //                objPhoneTypeDTO.Description = currObj.PhoneTypeDescription;
        //                objPhoneTypeDTO.Name = currObj.PhoneTypeName;
        //                objPhoneTypeDTO.PhoneTypeID = currObj.PhoneTypeID;
        //                objPhoneTypeDTO.TypeCode = currObj.PhoneTypeCode;
        //                listPhoneTypeDTO.Add(objPhoneTypeDTO);
        //            }
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        _logger.HandleException(exp, exp.Message, true);
        //    }

        //    return listPhoneTypeDTO;
        //}


        //#region Changes By Baber
        ///// <summary>
        /////  To Get List List of all list
        ///// </summary>
        ///// <returns></returns>
        //public List<CountryDTO> GetAllCountriesList()
        //{
        //    CountryDAO countryDAO = (CountryDAO)DAOFactory.GetDataAccessObject(DAOFactory.CountryDAO);
        //    List<CountryDTO> listofCountries = null;

        //    try
        //    {
        //        listofCountries = countryDAO.GetAllCountriesList();
        //    }
        //    catch (Exception exp)
        //    {
        //        _logger.HandleException(exp, exp.Message, true);
        //    }


        //    return listofCountries;


        //}
        ///// <summary>
        /////  For loading all zipcodes
        ///// </summary>
        ///// <returns></returns>
        //public List<ZipCodeDTO> GetAllZipCodesList()
        //{
        //    var zipCodeDAO = (ZipCodeDAO)DAOFactory.GetDataAccessObject(DAOFactory.ZipCodeDAO);
        //    List<ZipCodeDTO> listofZipCodes = null;

        //    try
        //    {
        //        listofZipCodes = zipCodeDAO.GetAllZipcodesList();
        //    }
        //    catch (Exception exp)
        //    {
        //        _logger.HandleException(exp, exp.Message, true);
        //    }

        //    return listofZipCodes;
        //}

        ///// <summary>
        ///// For loading all states
        ///// </summary>
        ///// <returns></returns>
        //public List<StateDTO> GetAllStatesList()
        //{
        //    var stateDAO = (StateDAO)DAOFactory.GetDataAccessObject(DAOFactory.StatesDAO);
        //    List<StateDTO> listofStates = null;

        //    try
        //    {
        //        listofStates = stateDAO.ListStates();
        //    }
        //    catch (Exception exp)
        //    {
        //        _logger.HandleException(exp, exp.Message, true);
        //    }

        //    return listofStates;
        //}

        //#endregion

        /// <summary>
        /// Author Mansoor
        /// Date 03 Feb 2011
        /// Get user primary phone by guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public PhoneDTO GetPartyPrimaryPhoneNoByRegGuid(Guid guid)
        {
            PhoneDTO userPrimaryPhone = null;
            try
            {
                QuivusLogger.Info("GetUserPrimaryPhoneNoByRegGuid Called PARAM guid:: " + guid);
                if (System.Guid.Empty ==  guid)
                {
                    throw new InvalidArgumentException(ProjectConstant.PARTY_GUID_EMPTY_ERROR_MSG);
                }
                userPrimaryPhone = objAddressBookDAO.GetPartyPrimaryPhoneNoByRegGuid(guid);
            }
            catch
            {
                throw;
            }
            return userPrimaryPhone;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 03 Feb 2011
        /// Get user primary phone by guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public AddressBookDTO GetAddressBookByPartyRegGuid(Guid guid)
        {
            AddressBookDTO userAddressBook = null;
            try
            {
                QuivusLogger.Info("LoadAddressBookByPartyRegGuid Called PARAM guid:: " + guid);
                if (System.Guid.Empty == guid)
                {
                    throw new InvalidArgumentException(ProjectConstant.PARTY_GUID_EMPTY_ERROR_MSG);
                }
                userAddressBook = objAddressBookDAO.LoadAddressBookByPartyRegGuid(guid);
            }
            catch
            {
                throw;
            }
            return userAddressBook;
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
            string settingFieldData = null;
            try
            {
                QuivusLogger.Info("LoadUserProfileSettingsAddressByRegGuidAndType Called PARAM guid:: " + guid + " settingTypeId " + settingTypeId);
                if (System.Guid.Empty == guid)
                {
                    throw new InvalidArgumentException(ProjectConstant.PARTY_GUID_EMPTY_ERROR_MSG);
                }
                else if (settingTypeId <= 0)
                {
                    //M throw new InvalidArgumentException(ProjectConstant.U);
                }

                if (objAddressBookDAO == null)
                {
                     objAddressBookDAO = (AddressBookDAO)DAOFactory.GetDataAccessObject(typeof(AddressBookDAO));
                }
                settingFieldData = objAddressBookDAO.LoadUserProfileSettingsAddressByRegGuidAndType(guid, settingTypeId);
            }
            catch
            {
                throw;
            }
            return settingFieldData;
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
            string settingFieldData = null;
            try
            {
                QuivusLogger.Info("LoadPartyProfileSettingsPhoneByRegGuidAndType Called PARAM guid:: " + guid + " ownerTypeId " + ownerTypeId);
                if (System.Guid.Empty == guid)
                {
                    throw new InvalidArgumentException(ProjectConstant.PARTY_GUID_EMPTY_ERROR_MSG);
                }
                else if (ownerTypeId <= 0)
                {
                    //M throw new InvalidArgumentException(ProjectConstant.U);
                }

                if (objAddressBookDAO == null)
                {
                    objAddressBookDAO = (AddressBookDAO)DAOFactory.GetDataAccessObject(typeof(AddressBookDAO));
                }
                settingFieldData = objAddressBookDAO.LoadPartyProfileSettingsPhoneByRegGuidAndType(guid, ownerTypeId);
            }
            catch
            {
                throw;
            }
            return settingFieldData;
        }

        public List<PatientAddressBookInfoDTO> LoadPatientAddressBookByPartyRegGuid(List<Guid> GuidList)
        {
            List<PatientAddressBookInfoDTO> LstPatientAB = null;
            try
            {
                QuivusLogger.Info("LoadPatientAddressBookByPartyRegGuid Called PARAM GuidList:: " + GuidList);

                LstPatientAB = objAddressBookDAO.LoadPatientAddressBookByPartyRegGuid(GuidList);
            }
            catch
            {
                throw;
            }
            return LstPatientAB;
        }
    }
}
