using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Quivus.Utility;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Quivus.Utility.Logger;
using Quivus.Model.AddressBook;
using Quivus.Utility.CustomException;

namespace UserManager.DataAccess.AddressBook
{
    public class AddressBookDAO
    {
        public PhoneDTO GetPartyPrimaryPhoneNoByRegGuid(Guid guid)
        {
            PhoneDTO phone = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.USER_PRIMARY_PHONE_LOAD_BY_GUID_ADDRESS_BOOK_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(ProjectConstant.ADDRESS_BOOK_DB); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PartyGuid", DbType.Guid, guid);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null && objIReader.Read())
                    {
                        phone = new PhoneDTO();

                        phone.PhoneNumber = objIReader["PhoneNumber"].ToString();

                        if (objIReader["PhoneExtension"] != DBNull.Value)
                        {
                            phone.Extension = objIReader["PhoneExtension"].ToString();
                        }

                        if (objIReader["NumberTypeID"] != DBNull.Value)
                        {
                            phone.NumberTypeID = int.Parse(objIReader["NumberTypeID"].ToString());
                        }

                        if (objIReader["NumberTypeName"] != DBNull.Value)
                        {
                            phone.NumberTypeName = objIReader["NumberTypeName"].ToString();
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new DTOConversionException(exp.Message, exp.InnerException.ToString());
            }
            return phone;
        }
    }
}
