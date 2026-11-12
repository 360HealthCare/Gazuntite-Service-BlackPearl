using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Quivus.Utility.Logger;

namespace UserManager.Business.Validators
{
    public static class Validations
    {
        public static bool ValidateSSN(string value)
        {
            string SSN = value;
            //SSN = SSN.Replace("-", string.Empty);

            // For xxx-xxx-xxxx format use \d{3}[-]\d{3}[-]\d{4}
            //  if (!Regex.IsMatch(SSN, @"^\d{9}$"))
            if (Regex.IsMatch(SSN, @"^\d{3}[-]\d{2}[-]\d{4}$"))
            {
                return true;
            }
            return false;
        }

        public static bool ValidateEmailAddress(string emailAddress)
        {
            string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"
                        + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                        + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                        + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                        + @"[a-zA-Z]{2,}))$";
            Regex reStrict = new Regex(patternStrict);

            bool isStrictMatch = reStrict.IsMatch(emailAddress);
            return isStrictMatch;

        }

        public static bool ValidatePhoneNumber(string value)
        {
            string PhoneNumber = value;
            PhoneNumber = PhoneNumber.Replace("(", string.Empty);
            PhoneNumber = PhoneNumber.Replace(")", string.Empty);
            PhoneNumber = PhoneNumber.Replace("-", string.Empty);

            if (Regex.IsMatch(PhoneNumber, @"^([0-9]{10,20})$"))
            {
                return true;
            }
            return false;
        }

        public static bool ValidatePhoneExtension(string value)
        {
            if (!Regex.IsMatch(value, @"^\d{4}$"))
            {
                return false;
            }
            return true;
        }
        //^[0-9a-zA-Z]+$
        public static bool IsAlphaNumeric(string strToCheck)
        {

            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");
            return objAlphaNumericPattern.IsMatch(strToCheck);
        }

        /// <summary>
        /// Validatoin for alphanumeric with space fields
        /// </summary>
        /// <param name="strToCheck"></param>
        /// <author> Waqas Baber</author>
        /// <returns></returns>
        public static bool IsAlphaNumericWithSpace(string strToCheck)
        {

            Regex objAlphaNumericPattern = new Regex(@"[^a-zA-Z0-9\s]");
            return objAlphaNumericPattern.IsMatch(strToCheck);
        }
        public static bool IsValidateDateTime(DateTime dt)
        {
            QuivusLogger.Info("Calling Method IsValidateDateTime dt:: " + dt);
            if (dt != null) // Added By Ismail to Be check by mansoor
            {
                DateTime result = DateTime.Now;

                if (DateTime.Compare(dt, Quivus.Utility.ProjectConstant.DATE_LIMIT) < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
                return true;
        }

        public static bool IsValidSearchDate(string value, out DateTime date)
        {
            bool flag = false;
            int yearToAdd;
            int yearToSearch;

            date = new DateTime();

            try
            {
                if (Regex.IsMatch(value, @"^[0-9]{8}$")) // MMDDYYYY
                {
                    date = new DateTime(int.Parse(value.Substring(4, 4)), int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(2, 2)));
                    flag = true;
                }
                else if (Regex.IsMatch(value, @"^[0-9]{6}$")) // MMDDYY
                {
                    flag = true;

                    yearToSearch = int.Parse(value.Substring(4, 2));

                    if (DateTime.Now.Year >= (2000 + yearToSearch))
                        yearToAdd = 2000;
                    else
                        yearToAdd = 1900;

                    date = new DateTime(yearToAdd + yearToSearch, int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(2, 2)));

                }
                else if (Regex.IsMatch(value, @"^[0-9]{2}-[0-9]{2}-[0-9]{2}$") || Regex.IsMatch(value, @"^[0-9]{2}/[0-9]{2}/[0-9]{2}$")) // MM-DD-YY, MM/DD/YY
                {
                    flag = true;

                    yearToSearch = int.Parse(value.Substring(6, 2));

                    if (DateTime.Now.Year >= (2000 + yearToSearch))
                        yearToAdd = 2000;
                    else
                        yearToAdd = 1900;

                    date = new DateTime(yearToAdd + yearToSearch, int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(3, 2)));
                }
                else if (Regex.IsMatch(value, @"^[0-9]{2}/[0-9]{2}/[0-9]{4}$") || Regex.IsMatch(value, @"^[0-9]{2}-[0-9]{2}-[0-9]{4}$")) // MM/DD/YYYY, MM-DD-YYYY   
                {
                    flag = true;
                    date = new DateTime(int.Parse(value.Substring(6, 4)), int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(3, 2)));
                }                
            }
            catch
            {
                flag = false;
            }
            return flag;
        }


        /// <summary>
        /// Mansoor
        /// it is used in Patient Search
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>

        public static bool IsValidateSearchSSN(string value, out string SSN, out string dashSSN)
        {
            SSN = value;
            dashSSN = value;

            if (Regex.IsMatch(value, @"^\d{3}[-]\d{2}[-]\d{4}$"))
            {
                SSN = value.Replace("-", string.Empty);
                return true;
            }
            else if (Regex.IsMatch(value, @"^[0-9]{9}$")) // 123 12 1324
            {
                dashSSN = value.Substring(0, 3) +  "-" + value.Substring(3, 2) +  "-" + value.Substring(5, 4); 
                return true;
            }

            return false;
        }
    }
}
