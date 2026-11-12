using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Quivus.Utility.CustomException;
using UserManagerService.DataAccess;
using Quivus.Utility.Logger;
using Quivus.Utility.Factory;
using Quivus.Utility;

using Quivus.Model.Common;

using UserManager.Business.Utility;

namespace UserManagerService.Business.ICDCode.Impl
{
    /// <summary>
    /// Author Mansoor
    /// Date 24 Jan 2011    
    /// </summary>
    public class ICDCodeManagerImpl : IICDCodeManager
    {
        ICDCodeDAO icdCodeDAO = null;

        /// <summary>
        /// Author Mansoor
        /// Date 21 Jan 2011
        /// search the matching icd codes and gets the description, on the bases of icd code provided
        /// </summary>
        /// <param name="icdCode"></param>
        /// <returns></returns>
        public List<ICDCodeSearchDTO> searchICDCODEByCode(string icdCode)
        {
            List<ICDCodeSearchDTO> response = null;
            try
            {
                QuivusLogger.Info("searchICDCODEByCode PARAM icdCode:: " + icdCode);

                if (String.IsNullOrEmpty(icdCode))
                {
                    throw new InvalidArgumentException(ProjectConstant.ICDCODE_CODE_INVALID_ERROR_MSG);
                }

                if (icdCodeDAO == null)
                {
                    icdCodeDAO = (ICDCodeDAO)DAOFactory.GetDataAccessObject(typeof(ICDCodeDAO));
                }

                response = icdCodeDAO.SearchICDCODEByCode(icdCode);
            }
            catch
            {
                throw;
            }
            return response;
        }
    }
}
