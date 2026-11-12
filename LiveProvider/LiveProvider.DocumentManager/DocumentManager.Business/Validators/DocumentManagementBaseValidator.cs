using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Utility.Constant.ProjectConstant;
using LiveProvider.Utility.Logger;
using LiveProvider.Utility.Constant.Errors;


namespace DocumentManager.Business.Validators
{
    public class DocumentManagementBaseValidator
    {
        protected static bool ValidateDate(DateTime date, StringBuilder sb, bool flag)
        {
            if (date == null)
            {
                sb.Append("\n" + ErrorMessages.VISIT_DATE_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        protected static bool ValidateDate(DateTime date, StringBuilder sb, bool flag, string ErrorMsg)
        {
            if (date == null || date <= DocumentManagerContants.DATE_MINIMUM)
            {
                sb.Append("\n" + ErrorMsg);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Author Sahibzada Sami ud din
        /// Date 05/07/2011
        /// This Method Will be used Validate Primary/Foriegn Keys
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        protected static bool ValidateKeyId(int userId, StringBuilder sb, bool flag, String ErrorMessage)
        {
            LiveProviderLogger.Info("Validating Key Id :: " + userId + " ErrorMessage Is In Error " + ErrorMessage);

            if (userId <= 0)
            {
                sb.Append("\n" + ErrorMessage);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Author Sahibzad Sami ud din 
        /// Date 05/07/2011
        /// This Method Will be used Validate Primary/Foriegn Keys
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        protected static bool ValidateStringInfo(String value, StringBuilder sb, bool flag, String ErrorMessage)
        {
            if (String.IsNullOrEmpty(value))
            {
                sb.Append("\n" + ErrorMessage);
                flag = false;
            }
            return flag;
        }
    }
}
