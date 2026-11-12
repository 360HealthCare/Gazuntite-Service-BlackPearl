using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UserManagerService.DataAccess;
using Quivus.Model.Search;
using Quivus.Utility.Factory;
using System.Configuration;
using Quivus.Utility.Logger;
using Quivus.Utility;
using Quivus.Utility.CustomException;
 
using System.Reflection;

using UserManager.Business.Utility;

namespace UserManagerService.Business.Search.Impl
{
   public class ContactSearchManagerImpl : IContactSearchManager
    {
       UserDAO userDAO = null; 

       /// <summary>
       /// Author Muhammad ismail
       /// Created Date 17 Jan 2011
       /// Method Used to load Clinic Users
       /// </summary>
       /// <param name="ClinicBranchId"></param>
       /// <returns></returns>
       public List<ContactSearchDTO> LoadUsersByClinicBranchID(int ClinicBranchId) 
       {
           List<ContactSearchDTO> contactSearchList = null;
           try
           {
               if (ClinicBranchId <= 0)
               {
                   throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
               }

               if (userDAO == null)
               {
                   userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
               }

               contactSearchList = userDAO.LoadUserDefaultQuickLinksByLocation(ClinicBranchId);
           }
           catch
           {
               throw;
           }
           return contactSearchList;
       }


    }

}
