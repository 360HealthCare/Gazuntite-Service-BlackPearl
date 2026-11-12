using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Model;
using Quivus.Model.AddressBook;

namespace AddressBookService.Business.AddressBook
{
    /// <summary>
    /// Author Mansoor
    /// Date 18 Jan 2011   
    /// </summary>
    public interface IAddressBookManager
    {
        //int SaveAddressBook(AddressBookDTO objAddressBookDTO);
        //List<AddressPartyTypeDTO> GetAllAddressPartyTypes();
        //List<AddressTypeDTO> GetAllAddressTypes();
        //List<PhoneNumberTypeDTO> GetAllPhoneNumberTypes();
        //List<OwnerTypeDTO> GetAllOwnerPartyTypes();
        //List<PhoneTypeDTO> GetAllPhoneTypes();
        //AddressPartyTypeDTO GetAddressPartyTypeByName(string addressPartyTypeName);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Jan 2011 
        /// get the list of cites by state id
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        List<CityDTO> ListCities(int stateId);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Jan 2011 
        /// get state id by zip code
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        int GetStateIdByZIPCode(string zipCode);        

        /// <summary>
        /// Author Mansoor
        /// Date 18 Jan 2011
        /// get the zip codes by city name
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        List<ZipCodeDTO> ListZipCodes(string cityName);


        /// <summary>
        /// Author Mansoor
        /// Date 18 Jan 2011
        /// get list of zip codes by sateid and city name
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        List<ZipCodeDTO> ListZipCodesByStateAndCity(int stateId, string cityName);


        /// <summary>
        /// Author Mansoor
        /// Date 20 Jan 2011
        /// Save the address book
        /// <param name="objAddBookDTO"></param>
        /// <returns></returns>
        int SaveUserAddressBook(int managedByUserId, AddressBookDTO objAddBookDTO);

        /// <summary>
        /// Author Mansoor
        /// Date 03 Feb 2011
        /// Get user primary phone by guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        PhoneDTO GetPartyPrimaryPhoneNoByRegGuid(Guid guid);

        /// <summary>
        /// Author Mansoor
        /// Date 09 Feb 2011
        /// Get user address book by guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        AddressBookDTO GetAddressBookByPartyRegGuid(Guid guid);


        /// <summary>
        /// Author Mansoor
        /// Date 28 Feb 2011
        /// load users address primary fields for tagging/customization
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="settingTypeId"></param>
        /// <returns></returns>
        string LoadUserProfileSettingsAddressByRegGuidAndType(Guid guid, int settingTypeId);

        /// <summary>
        /// Author Mansoor
        /// Date 28 Feb 2011
        /// load users phone fields for tagging/customization
        /// </summary>       
        /// <param name="guid"></param>
        /// <param name="partyTypeId"></param>
        /// <param name="settingTypeId"></param>
        /// <returns></returns>
        string LoadPartyProfileSettingsPhoneByRegGuidAndType(Guid guid, int ownerTypeId);

        List<QuivusModel.AddressBook.PatientAddressBookInfoDTO> LoadPatientAddressBookByPartyRegGuid(List<Guid> GuidList);
    }
}
