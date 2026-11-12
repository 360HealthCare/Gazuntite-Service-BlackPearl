using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Utility.Logger;
using Quivus.Utility.Factory;
using Quivus.Model.AddressBook;
using Quivus.Model.Common;

using AddressBookService.DataAccess;

namespace AddressBookService.Business.Common
{
    /// <summary>
    /// Author Mansoor
    /// Date 18 Jan 2011
    /// List all the Addrss Book DTOs used for Registeration
    /// </summary>
    public class AddressBookStaticDataManager
    {
        private AddressBookDAO addressBookDAO;

        public AddressBookStaticDataManager()
        {
            addressBookDAO = (AddressBookDAO)DAOFactory.GetDataAccessObject(typeof(AddressBookDAO));
        }

        public AddressBookStaticDataDTO GetAllAddressBookStaticData()
        {
            AddressBookStaticDataDTO aBookSD = null;
            try
            {
                aBookSD = new AddressBookStaticDataDTO();

                aBookSD.PhoneTypes = ListAllPhoneType();
                aBookSD.PhoneNumberTypes = ListAllPhoneNumberType();
                //M aBookSD.AllZipCodesList = ListAllZipCodes();
                aBookSD.AllAddressPartyTypeList = ListAllAddressPartyType();
                aBookSD.AllStatesList = ListAllStates();
                aBookSD.AllCountryList = ListAllCountries();

            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);                
            }
            return aBookSD;
        }

        /// <summary>
        /// get the list of all phone number types
        /// </summary>
        /// <returns></returns>
        private List<PhoneTypeDTO> ListAllPhoneType()
        {
            List<PhoneTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllPhoneType");
                list = addressBookDAO.GetAllPhoneTypes();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw exp;
            }
            return list;
        }

        /// <summary>
        ///  get the list of all phone number types
        /// </summary>
        /// <returns></returns>
        private List<PhoneNumberTypeDTO> ListAllPhoneNumberType()
        {
            List<PhoneNumberTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllPhoneNumberType");
                list = addressBookDAO.GetAllPhoneNumberTypes();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw exp;
            }
            return list;
        }

        /// <summary>
        /// Get all zipcode  list along with cities
        /// </summary>
        /// <returns></returns>
        private List<ZipCodeDTO> ListAllZipCodes()
        {
            List<ZipCodeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllZipCodes");
                list = addressBookDAO.GetAllZipCodesList();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw exp;
            }
            return list;
        }

        /// <summary>
        /// Get All address party types
        /// </summary>
        /// <returns></returns>
        private List<AddressPartyTypeDTO> ListAllAddressPartyType()
        {
            List<AddressPartyTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllAddressPartyType");
                list = addressBookDAO.GetAllAddressPartyTypes();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw exp;
            }
            return list;
        }

        /// <summary>
        /// Get All states 
        /// </summary>
        /// <returns></returns>
        private List<StateDTO> ListAllStates()
        {
            List<StateDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllStates");
                list = addressBookDAO.GetAllStatesList();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw exp;
            }
            return list;
        }

        /// <summary>
        /// Get All Countries List
        /// </summary>
        /// <returns></returns>
        private List<CountryDTO> ListAllCountries()
        {
            List<CountryDTO> list = null;
            try
            {
                QuivusLogger.Info("Loading ListAllCountries");
                list = addressBookDAO.GetAllCountriesList();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw exp;
            }
            return list;
        }
    }
}
