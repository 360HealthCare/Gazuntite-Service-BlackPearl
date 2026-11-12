using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Utility;
using QuivusUtility.ProjectConstants;
using Quivus.Utility.Logger;

namespace Visit.Business.Validators
{
    public class QBaseValidator
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
            if (date == null || date <= ProjectConstant.DATE_MINIMUM)
            {
                sb.Append("\n" + ErrorMsg);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used Validate Primary/Foriegn Keys
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        protected static bool ValidateKeyId(int userId, StringBuilder sb, bool flag, String ErrorMessage)
        {
            QuivusLogger.Info("Validating Key Id :: " + userId + " ErrorMessage Is To Display in case of invalid:: " + ErrorMessage);

            if (userId == null || userId <= 0)
            {
                sb.Append("\n" + ErrorMessage);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
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
