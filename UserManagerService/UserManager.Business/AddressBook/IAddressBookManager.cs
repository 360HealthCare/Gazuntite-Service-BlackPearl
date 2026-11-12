using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.AddressBook;

namespace UserManager.Business.AddressBook
{
    public interface IAddressBookManager
    {
        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 12/7/2011
        /// This method will return patient primary phone number.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        PhoneDTO GetPartyPrimaryPhoneNoByRegGuid(Guid guid);
    }
}
