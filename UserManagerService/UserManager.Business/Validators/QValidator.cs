using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Utility.ProjectConstants;
using Quivus.Model.TypeDTOs;
using UserManager.Business.Registration.Impl;
using Quivus.Utility.Logger;

namespace UserManager.Business.Validators
{
    public class QValidator
    {
        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 24 jaan 2011
        /// Validate USerPrefix
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected static bool ValidatePrefix(PrefixTypeDTO prefix, StringBuilder sb, bool flag)
        {
            if (prefix != null)
            {
                if (prefix.PrefixId <= 0)
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_PREFIX_EMPTY);
                    flag = false;
                }
            }

            return flag;
        }


        /// <summary>
        /// Author Baber waqas 
        /// created 24 jun 2011
        /// for a quick patch for emergency contact
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected static bool ValidateEmergencyContactName(String value , StringBuilder sb,bool flag)
        {
            if (string.IsNullOrEmpty(value))
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_EMERGENCY_FIRST_NAME_INVALID);
                flag = false;
            }
            else if (Validations.IsAlphaNumericWithSpace(value))
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_EMERGENCY_FIRST_NAME_INVALID);
                flag = false;
            }
            return flag;

        }

        /// <summary>
        /// Author Muhmmad Ismail
        /// Created Date 24 Jan 2011
        /// Validate user First Name
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected static bool ValidateFirstName(String FirstName, StringBuilder sb, bool flag)
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_FIRST_NAME_EMPTY);
                flag = false;
            }
            //else if (Validations.IsAlphaNumeric(FirstName))
            //{
            //    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_FIRST_NAME_INVALID);
            //    flag = false;
            //}
            return flag;
        }

        /// <summary>
        /// Author Muhmmad Ismail
        /// Created Date 24 Jan 2011
        /// Validate User Gender
        /// </summary>
        /// <param name="Gender"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected static bool ValidateGender(GenderTypeDTO Gender, StringBuilder sb, bool flag)
        {
            if (Gender == null || Gender.GenderId.Equals(0))
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_GENEDER_EMPTY);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Author Muhmmad Ismail
        /// Created Date 24 Jan 2011
        /// Validate Patient DOB
        /// </summary>
        /// <param name="DateOfBirth"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected static bool ValidatePatientDOB(DateTime DateOfBirth, StringBuilder sb, bool flag)
        {
            QuivusLogger.Info("Patient Universal Date Time ::"+DateOfBirth.ToUniversalTime());

            if (DateOfBirth == null)
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_DOB_EMPTY);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Author Muhmmad Ismail
        /// Created Date 24 Jan 2011
        /// Validate User Middle Name
        /// </summary>
        /// <param name="MiddleName"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected static bool ValidateMiddleName(String MiddleName, StringBuilder sb, bool flag)
        {
            if (!string.IsNullOrEmpty(MiddleName)  /*&& Validations.IsAlphaNumeric(MiddleName)*/)
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_MIDDLE_NAME_INVALID);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Author Muhmmad Ismail
        /// Created Date 24 Jan 2011
        /// Validate User Last Name
        /// </summary>
        /// <param name="LastName"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected static bool ValidateLastName(String LastName, StringBuilder sb, bool flag)
        {
            if (string.IsNullOrEmpty(LastName))
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_LAST_NAME_EMPTY);
                flag = false;
            }
           // else if (Validations.IsAlphaNumeric(LastName))
            //{
            //    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_LAST_NAME_INVALID);
            //    flag = false;
            //}
            return flag;
        }


        /// <summary>
        /// Author Muhmmad Ismail
        /// Created Date 24 Jan 2011
        /// Validate User Email & Check It should Not Already Exist
        /// </summary>
        /// <param name="UserEmail"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected static bool ValidateEmail(String UserEmail, StringBuilder sb, bool flag)
        {
            if (string.IsNullOrEmpty(UserEmail))
            {
                //M sb.Append("\n" + UserRegistrationConstant.REGISTRATION_EMAIL_EMPTY);
                //M flag = false;
            }
            else
            {
                if (!Validations.ValidateEmailAddress(UserEmail))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_EMAIL_INVALID);
                    flag = false;
                }
                else
                {
                    // Verify email sHOULD NOT ALREADY EXIST

                    QuivusRegistartionManagerImpl quivusRegistartionImpl = new QuivusRegistartionManagerImpl();
                    bool IsValid = quivusRegistartionImpl.VerifyUserEmail(UserEmail);

                    if (!IsValid)
                    {
                        sb.Append("\n" + UserRegistrationConstant.REGISTRATION_EMAIL_ALREADY_EXIST);
                        flag = false;
                    }

                }
            }
            return flag;
        }

        /// <summary>
        /// Author Muhmmad Ismail
        /// Created Date 24 Jan 2011
        /// Validate User SSN
        /// </summary>
        /// <param name="SSN"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected static bool ValidateSSN(String SSN, StringBuilder sb, bool flag)
        {
            if (SSN != null && string.IsNullOrEmpty(SSN) == false)
            {
                //if (!Validations.ValidateSSN(SSN))
                //{
                //    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_SOCIALSECURITYNUMBER_ERROR);
                //    flag = false;
                //}
            }
            return flag;
        }

        /// <summary>
        /// Author Muhmmad Ismail
        /// Created Date 24 Jan 2011
        /// Validate Provided Clinic Branch Id In Case Clinical User is doing Registration for Patient
        /// </summary>
        /// <param name="ClinicBranhId"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected static bool ValidateClinicBranch(int ClinicBranhId, StringBuilder sb, bool flag)
        {
            if (ClinicBranhId <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_CLINIC_BRANCH_EMPTY);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Author Muhmmad Ismail
        /// Created Date 24 Jan 2011
        /// Validate User Account status
        /// </summary>
        /// <param name="AccountStatus"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected static bool ValidateAccountStatus(StatusTypeDTO AccountStatus, StringBuilder sb, bool flag)
        {
            if (AccountStatus == null || AccountStatus.StatusID <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_ACCOUNT_STATUS_EMPTY);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Author Muhmmad Ismail
        /// Created Date 24 Jan 2011
        /// Validate User Type
        /// </summary>
        /// <param name="userTypeDTO"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected static bool ValidateUserTypeId(UserTypeDTO userTypeDTO, StringBuilder sb, bool flag)
        {
            if (userTypeDTO == null || userTypeDTO.UserTypeId <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_USER_TYPE_EMPTY);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Author Muhmmad Ismail
        /// Created Date 24 Jan 2011
        /// Validate Provided Id 
        /// </summary>
        /// <param name="ClinicBranhId"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected static bool ValidateProviderId(int providerId, StringBuilder sb, bool flag)
        {
            if (providerId <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_PROVIDER_ID_EMPTY);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Author Muhmmad Ismail
        /// Created Date 2 Feb 2011
        /// Validate SectionId
        /// </summary>
        /// <param name="SectionId"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected static bool ValidateSectionNo(int SectionId, StringBuilder sb, bool flag)
        {
            if (SectionId <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_SECTION_ID_EMPTY);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Author Muhmmad Ismail
        /// Created Date 2 Feb 2011
        /// Validate SectionId
        /// </summary>
        /// <param name="SectionId"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected static bool ValidateUserProfileCategoryID(int UserProfileCategoryID, StringBuilder sb, bool flag)
        {
            if (UserProfileCategoryID <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_USER_PROFILE_CATEGORY_ID_EMPTY);
                flag = false;
            }
            return flag;
        }


        protected static bool ValidateModifiedBy(int modifiedBy, StringBuilder sb, bool flag)
        {
            if (modifiedBy <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_MODIFIED_BY_ERROR);
                flag = false;
            }
            return flag;
        }

        protected static bool ValidateCreatedBy(int CreatedBY, StringBuilder sb, bool flag)
        {
            if (CreatedBY <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_CREATED_BY_ERROR);
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
            if (userId <= 0)
            {
                sb.Append("\n" + ErrorMessage);
                flag = false;
            }
            return flag;
        }

        protected static bool ValidateDate(DateTime date, StringBuilder sb, bool flag, string ErrorMsg)
        {
            if (date == null)
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
