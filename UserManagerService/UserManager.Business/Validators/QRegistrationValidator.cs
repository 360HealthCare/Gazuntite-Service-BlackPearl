using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Quivus.Utility.ProjectConstants;
using Quivus.Model.AddressBook;
using Quivus.Model.TypeDTOs;
using Quivus.Model.User.Patient;
using Quivus.Model.User;
using Quivus.Utility.Logger;
using UserManager.Business.Registration.Impl;
using Quivus.Utility;
using Quivus.Model.Image;


namespace UserManager.Business.Validators
{

    /// <summary>
    /// Author Muhammad ismail
    /// Created Date 19 jan 2011
    /// Class Used TO Validate Registration Input
    /// </summary>
    public class QRegistrationValidator : QValidator
    {
        /// <summary>
        /// Author Muhammad ismail 
        /// Date Update 14 Feb 2011
        /// Method Used To Verfiy User Input for registration
        /// </summary>
        /// <param name="userDTO"></param>
        /// <param name="sb"></param>
        /// <returns></returns>
        public static bool ValidateBaseUserInput(BaseUserDTO userDTO, ref StringBuilder sb)
        {
            bool flag = true;

            if (userDTO != null)
            {
                QuivusLogger.Info("Calling Validation On RegisterBaseUser userDTO:: " + userDTO);

                # region Basic Information Validation

                QuivusLogger.Info("Calling Validation On RegisterBaseUser Prefix:: " + userDTO.Prefix);
                flag = ValidatePrefix(userDTO.Prefix, sb, flag); // Optional 

                QuivusLogger.Info("Calling Validation On RegisterBaseUser FirstName:: " + userDTO.FirstName);
                flag = ValidateFirstName(userDTO.FirstName, sb, flag); // Required

                QuivusLogger.Info("Calling Validation On RegisterBaseUser LastName:: " + userDTO.LastName);
                flag = ValidateLastName(userDTO.LastName, sb, flag); //Required

                QuivusLogger.Info("Calling Validation On RegisterBaseUser MiddleName:: " + userDTO.MiddleName);
                flag = ValidateMiddleName(userDTO.MiddleName, sb, flag); // optional

                QuivusLogger.Info("Calling Validation On RegisterBaseUser Gender:: " + userDTO.Gender);
                flag = ValidateGender(userDTO.Gender, sb, flag);

                //  flag = ValidatePatientDOB(userDTO., sb, flag);

                QuivusLogger.Info("Calling Validation On RegisterBaseUser AccountStatus:: " + userDTO.AccountStatus);
                flag = ValidateAccountStatus(userDTO.AccountStatus, sb, flag);

                QuivusLogger.Info("Calling Validation On RegisterBaseUser ActiveUserType:: " + userDTO.ActiveUserType);
                flag = ValidateUserTypeId(userDTO.ActiveUserType, sb, flag);

                QuivusLogger.Info("Calling Validation On RegisterBaseUser ClinicBranhId:: " + userDTO.ClinicBranhId);
                flag = ValidateClinicBranch(userDTO.ClinicBranhId, sb, flag);

                QuivusLogger.Info("Calling Validation On RegisterBaseUser UserEmail:: " + userDTO.UserEmail);
                flag = ValidateEmail(userDTO.UserEmail, sb, flag);

                QuivusLogger.Info("Calling Validation On RegisterBaseUser SSN:: " + userDTO.SSN);
                flag = ValidateSSN(userDTO.SSN, sb, flag);

                if (userDTO.Image != null) // Validate Image Only when Provided
                {
                    QuivusLogger.Info("Calling Validation On Image:: " + userDTO.Image);
                    flag = ValidateImage(userDTO.Image, sb, flag);
                }

                if (string.IsNullOrEmpty(userDTO.UserPassword))
                {
                    userDTO.UserPassword = ProjectConstant.DEFUALT_PASSWORD;
                }
                //  flag = ValidatePatientAddress(registrationObj, sb, flag);           /* Validate State City Zip If Address Is Provided */

                ///  flag = ValidatePatientAddressByState(registrationObj, sb, flag);   /* Validate Address City Zip If State Is Provided */

                /// flag = ValidatePatientAddressByCity(registrationObj, sb, flag);     /* Validate Address State Zip If City Is Provided */


                // flag = ValidatePatientPhoneNo(registrationObj, sb, flag);

                //  flag = ValidatePatientPhoneExt(registrationObj, sb, flag);

                //   flag = ValidatePatientPhoneType(registrationObj, sb, flag);

                #endregion

                # region Other Information

                //flag = ValidateMedicareNumber(registrationObj, sb, flag);

                //flag = ValidateMotherMadienNumber(registrationObj, sb, flag);

                //flag = ValidateMotherPhone(registrationObj, sb, flag);

                //flag = ValidateMoherPhoneExt(registrationObj, sb, flag);



                //flag = ValidateFatherName(registrationObj, sb, flag);

                //flag = ValidateFatherPhoneNumber(registrationObj, sb, flag);

                //flag = ValidateFatherPhoneExt(registrationObj, sb, flag);

                //flag = ValidateEmployerName(registrationObj, sb, flag);



                # endregion

                # region Guarantor Validation

                //    flag = ValidateGuarantorInformation(registrationObj, sb, flag);

                # endregion

                # region Health Tab Validation

                //if (registrationObj.PatientSocialDrugs != null && registrationControl.PatientSocialDrugs.Value == true)
                //{
                //    // Validate Drugs Information
                //    if (string.IsNullOrEmpty(registrationObj.User.PatientDTO.DrugStatus))
                //    {
                //        sb.Append("\n" + UserRegistrationConstant.REGISTRATION_DRUGS_EMPTY);
                //        flag = false;
                //    }
                //}
                //if (registrationControl.PatientSocialAlcohol != null && registrationControl.PatientSocialDrugs.Value == true)
                //{
                //    // Validate Drugs Information
                //    if (string.IsNullOrEmpty(registrationObj.User.PatientDTO.DrugStatus))
                //    {
                //        sb.Append("\n" + UserRegistrationConstant.REGISTRATION_ALCOHOL_EMPTY
                //            );
                //        flag = false;
                //    }
                //}
                # endregion

                # region Insurance Validation
                //  flag = ValidateInsuranceInformation(registrationObj, sb, flag);
                # endregion

            }
            else
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_DTO_INVALID);
                flag = false;
            }
            return flag;
        }


        /// <summary>
        /// Author Muhammad ismail 
        /// Date Update 14 Feb 2011
        /// Method Used To Verfiy User Input for registration/update
        /// </summary>
        /// <param name="userDTO"></param>
        /// <param name="sb"></param>
        /// <returns></returns>
        public static bool ValidateBaseUserInput(BaseUserDTO userDTO, ref StringBuilder sb, bool IsUpdate)
        {
            bool flag = true;

            if (userDTO != null)
            {
                QuivusLogger.Info("Calling Validation On RegisterBaseUser userDTO:: " + userDTO);

                # region Basic Information Validation

                QuivusLogger.Info("Calling Validation On RegisterBaseUser Prefix:: " + userDTO.Prefix);
                flag = ValidatePrefix(userDTO.Prefix, sb, flag); // Optional 

                QuivusLogger.Info("Calling Validation On RegisterBaseUser FirstName:: " + userDTO.FirstName);
                flag = ValidateFirstName(userDTO.FirstName, sb, flag); // Required

                QuivusLogger.Info("Calling Validation On RegisterBaseUser LastName:: " + userDTO.LastName);
                flag = ValidateLastName(userDTO.LastName, sb, flag); //Required

                //QuivusLogger.Info("Calling Validation On RegisterBaseUser MiddleName:: " + userDTO.MiddleName);
               // flag = ValidateMiddleName(userDTO.MiddleName, sb, flag); // optional

                QuivusLogger.Info("Calling Validation On RegisterBaseUser Gender:: " + userDTO.Gender);
                flag = ValidateGender(userDTO.Gender, sb, flag);

                //  flag = ValidatePatientDOB(userDTO., sb, flag);

                if (!IsUpdate) // Required Only at time of registration
                {
                    QuivusLogger.Info("Calling Validation On RegisterBaseUser AccountStatus:: " + userDTO.AccountStatus);
                    flag = ValidateAccountStatus(userDTO.AccountStatus, sb, flag);

                    QuivusLogger.Info("Calling Validation On RegisterBaseUser ActiveUserType:: " + userDTO.ActiveUserType);
                    flag = ValidateUserTypeId(userDTO.ActiveUserType, sb, flag);


                    QuivusLogger.Info("Calling Validation On RegisterBaseUser ClinicBranhId:: " + userDTO.ClinicBranhId);
                    flag = ValidateClinicBranch(userDTO.ClinicBranhId, sb, flag);
                }

                QuivusLogger.Info("Calling Validation On RegisterBaseUser SSN:: " + userDTO.SSN);
                flag = ValidateSSN(userDTO.SSN, sb, flag);

                if (userDTO.Image != null) // Validate Image Only when Provided & Status Not Deleted
                {
                    QuivusLogger.Info("Calling Validation On Image:: " + userDTO.Image);
                    flag = ValidateImage(userDTO.Image, sb, flag);
                }

                if (string.IsNullOrEmpty(userDTO.UserPassword))
                {
                    userDTO.UserPassword = ProjectConstant.DEFUALT_PASSWORD;
                }

                if (!IsUpdate) // Save Call Verify email & Created by
                {
                    //M shd provide email or pin
                    if (string.IsNullOrEmpty(userDTO.UserEmail) && string.IsNullOrEmpty(userDTO.UserName))
                    {
                        flag = false;
                        sb.Append("\n" + UserRegistrationConstant.REGISTRATION_EMAIL_EMPTY);
                    }

                    if (!string.IsNullOrEmpty(userDTO.UserEmail))
                    {
                        QuivusLogger.Info("Calling Validation On RegisterBaseUser UserEmail:: " + userDTO.UserEmail);
                        flag = ValidateEmail(userDTO.UserEmail, sb, flag);
                    }

                    //M user can register by its self also
                    //M QuivusLogger.Info("Calling Validation On RegisterBaseUser Created By:: " + userDTO.CreatedBy);
                    //M flag = ValidateCreatedBy(userDTO.CreatedBy, sb, flag);
                }

                if (IsUpdate) // Update Case Verfiy Modified By Id 
                {
                    QuivusLogger.Info("Calling Validation On RegisterBaseUser Created By:: " + userDTO.CreatedBy);
                    flag = ValidateModifiedBy(userDTO.ModifiedBy, sb, flag);
                }

                //  flag = ValidatePatientAddress(registrationObj, sb, flag);           /* Validate State City Zip If Address Is Provided */

                ///  flag = ValidatePatientAddressByState(registrationObj, sb, flag);   /* Validate Address City Zip If State Is Provided */

                /// flag = ValidatePatientAddressByCity(registrationObj, sb, flag);     /* Validate Address State Zip If City Is Provided */


                // flag = ValidatePatientPhoneNo(registrationObj, sb, flag);

                //  flag = ValidatePatientPhoneExt(registrationObj, sb, flag);

                //   flag = ValidatePatientPhoneType(registrationObj, sb, flag);

                #endregion


            }
            else
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_DTO_INVALID);
                flag = false;
            }
            return flag;
        }



        /// <summary>
        /// Author Muhammad Ismail 
        /// Date Feb 14 2011
        /// Method Validate User Image Only When Provided & Status Not Deleted
        /// </summary>
        /// <param name="imageDTO"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>

        private static bool ValidateImage(ImageDTO imageDTO, StringBuilder sb, bool flag)
        {
            if (imageDTO != null && !imageDTO.IsDeleted)
            {

                if (imageDTO.UserTypeID <= 0)
                {
                    sb.Append("\n" + UserRegistrationConstant.IMAGE_USER_ID_EMPTY);
                    flag = false;
                }
                if (imageDTO.ImageTypeId <= 0)
                {
                    sb.Append("\n" + UserRegistrationConstant.IMAGE_TYPE_ID_EMPTY);
                    flag = false;
                }
                if (imageDTO.ImageData == null)
                {
                    sb.Append("\n" + UserRegistrationConstant.IMAGE_DATA_EMPTY);
                    flag = false;
                }
            }

            return flag;
        }


        public static bool ValidateRegistrationInput(RegistrationDTO registrationObj, ref StringBuilder sb)
        {

            bool flag = true;

            if (registrationObj != null && registrationObj.User != null && registrationObj.User.PatientDTO != null)
            {

                # region Basic Information Validation

                flag = ValidatePrefix(registrationObj.User.Prefix, sb, flag);

                //flag = ValidateFirstName(registrationObj.User.FirstName, sb, flag);

                //flag = ValidateLastName(registrationObj.User.LastName, sb, flag);

                //flag = ValidateMiddleName(registrationObj.User.MiddleName, sb, flag);

                flag = ValidateGender(registrationObj.User.Gender, sb, flag);

                flag = ValidatePatientDOB(registrationObj.User.PatientDTO.DateOfBirth, sb, flag);

                flag = ValidateAccountStatus(registrationObj.User.AccountStatus, sb, flag);

                flag = ValidateClinicBranch(registrationObj.ClinicBranhId, sb, flag);

                //  flag = ValidatePatientAddress(registrationObj, sb, flag);           /* Validate State City Zip If Address Is Provided */

                ///  flag = ValidatePatientAddressByState(registrationObj, sb, flag);   /* Validate Address City Zip If State Is Provided */

                /// flag = ValidatePatientAddressByCity(registrationObj, sb, flag);     /* Validate Address State Zip If City Is Provided */

                flag = ValidateEmail(registrationObj.User.UserEmail, sb, flag);

                // flag = ValidatePatientPhoneNo(registrationObj, sb, flag);

                //  flag = ValidatePatientPhoneExt(registrationObj, sb, flag);

                //   flag = ValidatePatientPhoneType(registrationObj, sb, flag);

                flag = ValidateSSN(registrationObj.User.SSN, sb, flag);



                #endregion

                # region Other Information

                //flag = ValidateMedicareNumber(registrationObj, sb, flag);

                //flag = ValidateMotherMadienNumber(registrationObj, sb, flag);

                //flag = ValidateMotherPhone(registrationObj, sb, flag);

                //flag = ValidateMoherPhoneExt(registrationObj, sb, flag);



                //flag = ValidateFatherName(registrationObj, sb, flag);

                //flag = ValidateFatherPhoneNumber(registrationObj, sb, flag);

                //flag = ValidateFatherPhoneExt(registrationObj, sb, flag);

                //flag = ValidateEmployerName(registrationObj, sb, flag);



                # endregion

                # region Guarantor Validation

                //    flag = ValidateGuarantorInformation(registrationObj, sb, flag);

                # endregion

                # region Health Tab Validation

                //if (registrationObj.PatientSocialDrugs != null && registrationControl.PatientSocialDrugs.Value == true)
                //{
                //    // Validate Drugs Information
                //    if (string.IsNullOrEmpty(registrationObj.User.PatientDTO.DrugStatus))
                //    {
                //        sb.Append("\n" + UserRegistrationConstant.REGISTRATION_DRUGS_EMPTY);
                //        flag = false;
                //    }
                //}
                //if (registrationControl.PatientSocialAlcohol != null && registrationControl.PatientSocialDrugs.Value == true)
                //{
                //    // Validate Drugs Information
                //    if (string.IsNullOrEmpty(registrationObj.User.PatientDTO.DrugStatus))
                //    {
                //        sb.Append("\n" + UserRegistrationConstant.REGISTRATION_ALCOHOL_EMPTY
                //            );
                //        flag = false;
                //    }
                //}
                # endregion

                # region Insurance Validation
                //  flag = ValidateInsuranceInformation(registrationObj, sb, flag);
                # endregion

            }
            else
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_DTO_INVALID);
                flag = false;
            }
            return flag;
        }


        private static bool ValidateFatherPhoneExt(RegistrationDTO registrationObj, StringBuilder sb, bool flag)
        {
            if (!string.IsNullOrEmpty(registrationObj.User.UserAddressBookDTO.PhoneDTOlist[2].Extension))
            {
                if (!Validations.ValidatePhoneExtension(registrationObj.User.UserAddressBookDTO.PhoneDTOlist[2].Extension))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_PHONE_EXT_FATHER_INVALID);
                    flag = false;
                }
            }
            return flag;
        }

        private static bool ValidateFatherPhoneNumber(RegistrationDTO registrationObj, StringBuilder sb, bool flag)
        {
            if (!string.IsNullOrEmpty(registrationObj.User.UserAddressBookDTO.PhoneDTOlist[2].PhoneNumber))
            {

                if (!Validations.ValidatePhoneNumber(registrationObj.User.UserAddressBookDTO.PhoneDTOlist[2].PhoneNumber))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_PHONE_FATHER_INVALID);
                    flag = false;
                }
                if (registrationObj.User.UserAddressBookDTO.PhoneDTOlist[2].NumberTypeID.Equals(0))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_MOTHER_PHONE_TYPE_EMPTY);
                    flag = false;
                }
            }
            return flag;
        }

        private static bool ValidateMoherPhoneExt(RegistrationDTO registrationObj, StringBuilder sb, bool flag)
        {
            if (!string.IsNullOrEmpty(registrationObj.User.UserAddressBookDTO.PhoneDTOlist[1].Extension))
            {
                if (!Validations.ValidatePhoneExtension(registrationObj.User.UserAddressBookDTO.PhoneDTOlist[1].Extension))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_PHONE_EXT_MOTHER_INVALID);
                    flag = false;
                }
            }
            return flag;
        }

        private static bool ValidateMotherPhone(RegistrationDTO registrationObj, StringBuilder sb, bool flag)
        {
            if (!string.IsNullOrEmpty(registrationObj.User.UserAddressBookDTO.PhoneDTOlist[1].PhoneNumber))
            {

                if (!Validations.ValidatePhoneNumber(registrationObj.User.UserAddressBookDTO.PhoneDTOlist[1].PhoneNumber))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_PHONE_MOTHER_INVALID);
                    flag = false;
                }

                if (registrationObj.User.UserAddressBookDTO.PhoneDTOlist[1].NumberTypeID.Equals(0))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_MOTHER_PHONE_TYPE_EMPTY);
                    flag = false;
                }
            }
            return flag;
        }



        private static bool ValidatePatientPhoneType(RegistrationDTO registrationObj, StringBuilder sb, bool flag)
        {
            if (registrationObj.User.UserAddressBookDTO != null && registrationObj.User.UserAddressBookDTO.PhoneDTOlist != null &&
                registrationObj.User.UserAddressBookDTO.PhoneDTOlist.Count > 0)
            {
                if (registrationObj.User.UserAddressBookDTO.PhoneDTOlist[0].NumberTypeID.Equals(0))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_PHONE_TYPE_EMPTY);
                    flag = false;
                }
            }
            else
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_PHONE_TYPE_EMPTY);
                flag = false;
            }
            return flag;
        }

        private static bool ValidatePatientPhoneExt(RegistrationDTO registrationObj, StringBuilder sb, bool flag)
        {
            if (!string.IsNullOrEmpty(registrationObj.User.UserAddressBookDTO.PhoneDTOlist[0].Extension))
            {
                if (!Validations.ValidatePhoneExtension(registrationObj.User.UserAddressBookDTO.PhoneDTOlist[0].Extension))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_PHONE_EXTENSION_INVALID);
                    flag = false;
                }
            }
            return flag;
        }

        private static bool ValidatePatientPhoneNo(RegistrationDTO registrationObj, StringBuilder sb, bool flag)
        {
            if (registrationObj.User.UserAddressBookDTO != null && registrationObj.User.UserAddressBookDTO.PhoneDTOlist != null &&
                registrationObj.User.UserAddressBookDTO.PhoneDTOlist.Count > 0)
            {
                if (string.IsNullOrEmpty(registrationObj.User.UserAddressBookDTO.PhoneDTOlist[0].PhoneNumber))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_PHONE_EMPTY);
                    flag = false;
                }
            }
            else
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_PHONE_EMPTY);
                flag = false;
            }

            return flag;
        }


        public static bool? NotEmptyAndAlphaNumeric(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (Validations.IsAlphaNumeric(value))
                {
                    return true;
                }
                else
                    return false;
            }
            return null;
        }

        private bool ValidatePatientAddressByCity(RegistrationDTO registrationObj, StringBuilder sb, bool flag)
        {
            if (ValidateCity(registrationObj.User.UserAddressBookDTO.AddressDTOlist[0]))
            {
                if (string.IsNullOrEmpty(registrationObj.User.UserAddressBookDTO.AddressDTOlist[0].AddressTypeName))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_ADDRESS_INVALID);
                    flag = false;
                }
                if (!ValidateState(registrationObj.User.UserAddressBookDTO.AddressDTOlist[0]))
                {
                    // Please Provide City Name
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_CITY_EMPTY);
                    flag = false;
                }

                if (!ValidateZipCode(registrationObj.User.UserAddressBookDTO.AddressDTOlist[0]))
                {
                    // Please Provide Zip Name
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_ZIP_EMPTY);
                    flag = false;
                }
            }
            return flag;
        }

        private static bool ValidatePatientAddressByState(RegistrationDTO registrationObj, StringBuilder sb, bool flag)
        {
            if (ValidateState(registrationObj.User.UserAddressBookDTO.AddressDTOlist[0]))
            {
                // State Is Selected Validate Rest Of Address Information 


                if (string.IsNullOrEmpty(registrationObj.User.UserAddressBookDTO.AddressDTOlist[0].AddressTypeName))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_ADDRESS_INVALID);
                    flag = false;
                }
                if (!ValidateCity(registrationObj.User.UserAddressBookDTO.AddressDTOlist[0]))
                {
                    // Please Provide City Name
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_CITY_EMPTY);
                    flag = false;
                }

                if (!ValidateZipCode(registrationObj.User.UserAddressBookDTO.AddressDTOlist[0]))
                {
                    // Please Provide Zip Name
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_ZIP_EMPTY);
                    flag = false;
                }
            }
            return flag;
        }

        private static bool ValidatePatientAddress(RegistrationDTO registrationObj, StringBuilder sb, bool flag)
        {
            if (registrationObj.User.UserAddressBookDTO != null && registrationObj.User.UserAddressBookDTO.AddressDTOlist.Count > 0)
            {
                if (!string.IsNullOrEmpty(registrationObj.User.UserAddressBookDTO.AddressDTOlist[0].StreetName))
                {

                    if (!ValidateState(registrationObj.User.UserAddressBookDTO.AddressDTOlist[0]))
                    {
                        // Please Provide State Name
                        sb.Append("\n" + UserRegistrationConstant.REGISTRATION_STATE_EMPTY);
                        flag = false;
                    }

                    if (!ValidateCity(registrationObj.User.UserAddressBookDTO.AddressDTOlist[0]))
                    {
                        // Please Provide City Name
                        sb.Append("\n" + UserRegistrationConstant.REGISTRATION_CITY_EMPTY);
                        flag = false;
                    }

                    if (!ValidateZipCode(registrationObj.User.UserAddressBookDTO.AddressDTOlist[0]))
                    {
                        // Please Provide Zip Name
                        sb.Append("\n" + UserRegistrationConstant.REGISTRATION_ZIP_EMPTY);
                        flag = false;
                    }
                    // Adress Is Provided State City Zip Code Are Compulsory
                }
                else
                {
                    flag = ValidatePatientAddressByState(registrationObj, sb, flag);
                }
            }
            return flag;
        }

        private static bool ValidateState(AddressDTO addressDTO)
        {
            if (addressDTO.StateID.Equals(0))
            {
                return false;
            }
            return true;

        }

        private static bool ValidateCity(AddressDTO addressDTO)
        {
            if (addressDTO.CityCode != null && addressDTO.CityCode.Equals("0"))
            {
                return false;
            }
            return true;


        }

        private static bool ValidateZipCode(AddressDTO addressDTO)
        {
            if (addressDTO.ZipCodeID.Equals(0))
            {
                return false;
            }
            return true;
        }

        private static bool ValidateGuarantorPhonenumber(RegistrationDTO registrationControl, StringBuilder sb, bool flag)
        {
            //if (!string.IsNullOrEmpty(registrationControl.User.PatientDTO.GuarantorDTO.AddressBook.PhoneDTOlist[0].PhoneNumber))
            //{

            //    if (!Validations.ValidatePhoneNumber(registrationControl.User.PatientDTO.GuarantorDTO.AddressBook.PhoneDTOlist[0].PhoneNumber))
            //    {
            //        sb.Append("\n" + UserRegistrationConstant.REGISTRATION_GUARANTOR_PHONE_INVALID);
            //        flag = false;
            //    }
            //    if (registrationControl.User.PatientDTO.GuarantorDTO.AddressBook.PhoneDTOlist[0].NumberTypeID.Equals(0))
            //    {
            //        sb.Append("\n" + UserRegistrationConstant.REGISTRATION_GUARANTOR_PHONE_TYPE);
            //        flag = false;
            //    }
            //}
            return flag;
        }

        private static bool ValidateGuarantorCity(RegistrationDTO registrationControl, StringBuilder sb, bool flag)
        {
            //if (registrationControl.User.PatientDTO.GuarantorDTO.AddressBook.AddressDTOlist[0].CityCode != null)
            //{
            //    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_GUARANTOR_CITY_EMPTY);
            //    flag = false;
            //}
            return flag;
        }

        private static bool ValidateGuarantorZipCode(RegistrationDTO registrationControl, StringBuilder sb, bool flag)
        {
            //if (registrationControl.User.PatientDTO.GuarantorDTO.AddressBook.AddressDTOlist[0].ZipCodeID.Equals(0))
            //{
            //    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_GUARANTOR_ZIPCODE_EMPTY);
            //    flag = false;
            //}
            return flag;
        }

        private static bool ValidateGuarantorState(RegistrationDTO registrationControl, StringBuilder sb, bool flag)
        {
            //if (registrationControl.User.PatientDTO.GuarantorDTO.AddressBook.AddressDTOlist[0].StateID.Equals(0))
            //{
            //    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_GUARANTOR_STATE_EMPTY);
            //    flag = false;
            //}
            return flag;
        }

        private static bool ValidateGuarantorAddressInformation(RegistrationDTO registrationControl, StringBuilder sb, bool flag)
        {
            //if (!string.IsNullOrEmpty(registrationControl.User.PatientDTO.GuarantorDTO.AddressBook.AddressDTOlist[0].StreetName))
            //{
            //    // Address is Provided Validate State City & Zip
            //    flag = ValidateGuarantorState(registrationControl, sb, flag);
            //    flag = ValidateGuarantorZipCode(registrationControl, sb, flag);
            //    ValidateGuarantorCity(registrationControl, sb, flag);
            //}
            //else
            //{
            //    if (!registrationControl.User.PatientDTO.GuarantorDTO.AddressBook.AddressDTOlist[0].StateID.Equals(0))
            //    {
            //        // State is Provided Validate State City & Zip
            //        ValidateGuarantorAddressField(registrationControl, sb, flag);
            //        ValidateGuarantorZipCode(registrationControl, sb, flag);
            //        ValidateGuarantorCity(registrationControl, sb, flag);
            //    }
            //}
            return flag;
        }

        private static bool ValidateGuarantorAddressField(RegistrationDTO registrationControl, StringBuilder sb, bool flag)
        {
            //if (string.IsNullOrEmpty(registrationControl.User.PatientDTO.GuarantorDTO.AddressBook.AddressDTOlist[0].AddressTypeName))
            //{
            //    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_GUARANTOR_ADRESS_EMPTY);
            //    flag = false;
            //}
            return flag;
        }
    }
}
