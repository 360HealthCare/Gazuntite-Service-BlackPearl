using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.AddressBook;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;
using UserManager.DataAccess.AddressBook;
using Quivus.Utility;
using Quivus.Utility.Factory;

namespace UserManager.Business.AddressBook.Impl
{
    public class AddressBookManagerImpl : IAddressBookManager
    {
        AddressBookDAO objAddressBookDAO;

        public AddressBookManagerImpl()
        {
            objAddressBookDAO = (AddressBookDAO)DAOFactory.GetDataAccessObject(typeof(AddressBookDAO));
        }

        public PhoneDTO GetPartyPrimaryPhoneNoByRegGuid(Guid guid)
        {
            PhoneDTO userPrimaryPhone = null;
            try
            {
                QuivusLogger.Info("GetUserPrimaryPhoneNoByRegGuid Called PARAM guid:: " + guid);
                if (System.Guid.Empty == guid)
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
    }
}
