using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

using Quivus.Model.Common;
using Quivus.Utility;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;
using UserManagerService.DataAccess;

namespace UserManagerService.DataAccess
{
    public class ICDCodeDAO : BaseDAO
    {
        public List<ICDCodeSearchDTO> SearchICDCODEByCode(string icdCode)
        {
            List<ICDCodeSearchDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.ICD_CODE_SEARCH_BY_CODE_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<ICDCodeSearchDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ICDCodeName", DbType.String, icdCode);   

                using (objIReader = db.ExecuteReader(command))
                {
                    GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(ICDCodeSearchDTO));
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }
    }
}
