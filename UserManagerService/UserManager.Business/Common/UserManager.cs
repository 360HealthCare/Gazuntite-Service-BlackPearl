using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Utility;
using Quivus.Utility.Logger;
using Quivus.Utility.Factory;

using UserManagerService.DataAccess;

namespace UserManagerService.Business.Common
{
    /// <summary>
    /// Auhtor Mansoor
    /// Date 26 Jan 2011
    /// Managers the misc functionality related to user
    /// </summary>

    public class UserManager
    {
        UserDAO userDAO = null;


        //M move to user manager, now its not a internal call to user manager but its is called by Quivus Manager
        [Obsolete]
        public Guid GetPartyGuid(int roleProfileID, int addressPartyTypeID)
        {
            Guid guid = System.Guid.Empty;

            try
            {
                QuivusLogger.Info("calling GetPartyGuid PARAM :: roleProfileID " + roleProfileID + " addressPartyTypeID " + addressPartyTypeID);

                if (userDAO == null)
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));

                if(addressPartyTypeID == (int)ProjectConstant.ADDRESS_PARTY_TYPE.Patient)
                {
                    guid = userDAO.GetPatientGuidByPatientId(roleProfileID);
                }
                else if (addressPartyTypeID == (int)ProjectConstant.ADDRESS_PARTY_TYPE.PatientGuarantor)
                {
                    guid = userDAO.GetGurantorGuidByGurantorId(roleProfileID);                    
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;
            }
            return guid;
        }
    }
}
