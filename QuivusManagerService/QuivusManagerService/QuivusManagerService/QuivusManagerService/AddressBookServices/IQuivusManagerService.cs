using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using QuivusManager.IntegratedServies.AddressBookService;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.AdderssBook;
using QuivusManager.ServiceResponse.ServiceCustomSession;

namespace QuivusManagerService.QuivusManagerService.ManagerService
{

    public partial interface IQuivusManagerService
    {
        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011        
        /// save the user address book, specified in the Party Type
        /// </summary>
        /// <param name="addressBookDTO"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse SaveAddressBook(int roleProfileID, int managedByUserID, AddressBookDTO addressBookDTO, UserSessionDTO userSession);

        [OperationContract]
        AddressBookPhoneResponse GetPartyPrimaryPhoneNoByRegGuid(Guid guid, UserSessionDTO userSession);

        [OperationContract]
        AddressBookAddressResponse LoadAddressBookByPartyRegGuid(Guid guid, UserSessionDTO userSession);
    }
}
