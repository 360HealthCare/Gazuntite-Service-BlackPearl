using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.AddressBookService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.AdderssBook;

namespace QuivusManager.Buisness.AddressBookBL
{
    public interface IAddressBookBL
    {
        int SaveUserAddressBook( int managedByUserId, AddressBookDTO objAddBookDTO);

        /// <summary>
        /// Author Mansoor
        /// Date 2 Feb 2011
        /// this method call the user manager to get party guid, calle din case when to save patient/ user AB
        /// </summary>        
        /// <param name="roleProfileID"></param>
        /// <param name="managedByUserId"></param>
        /// <param name="objAddBookDTO"></param>
        /// <returns></returns>
        int SaveUserAddressBook(int roleProfileID, int managedByUserId, AddressBookDTO objAddBookDTO);

        AddressBookPhoneResponse GetPartyPrimaryPhoneNoByRegGuid(Guid guid);

        AddressBookAddressResponse LoadAddressBookByPartyRegGuid(Guid guid);

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
        //[FaultContract(typeof(QuivusFault))]
        string LoadPartyProfileSettingsPhoneByRegGuidAndType(Guid guid, int ownerTypeId);

        List<PatientAddressBookInfoDTO> LoadPatientAddressBookByRegGuidAndType(List<Guid> guidList);
    }
}
