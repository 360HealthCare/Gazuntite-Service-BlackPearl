using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Model.AddressBook;
using Quivus.Utility;
using Quivus.Utility.ProjectConstants;
using System.Text.RegularExpressions;
using Quivus.Utility.Logger;

namespace UserManager.Business.Validators
{
    /// <summary>
    /// Author Mansoor
    /// Date 26 JAn 2011
    /// have custom validators to validate user addresss book info
    /// </summary>

    public class QAddressBookValidator
    {
        
        public static bool ValidateAddressBook(AddressBookDTO addressBookObj, ref StringBuilder sb)
        {
            bool flag = true;

            if (addressBookObj != null)
            {
                if (addressBookObj.AddressPartyTypeID <= 0)
                {
                    sb.Append("\n" + UserRegistrationConstant.ADDRESS_BOOK_PARTY_ID_INVALID);
                    flag = false;
                }
                else
                {
                    //Emergency Conatct will have onlu phone no.
                    if (addressBookObj.AddressPartyTypeID != (int)ProjectConstant.ADDRESS_PARTY_TYPE.EmergencyContact)
                        flag = ValidateAddressDTO(addressBookObj, sb, flag);

                    flag = ValidatePhoneDTO(addressBookObj, sb, flag);
                }
            }
            else
            {
                sb.Append("\n" + UserRegistrationConstant.ADDRESS_BOOK_DTO_INVALID);
                flag = false;
            }
            return flag;
        }

        private static bool ValidatePhoneDTO(AddressBookDTO addressBookObj, StringBuilder sb, bool flag)
        {
            if (addressBookObj.PhoneDTOlist != null && addressBookObj.PhoneDTOlist.Count > 0)
            {
                QuivusLogger.Info("Calling Validation On ValidatePhoneDTOList addressBookObj.PhoneDTOlist.Count:: " + addressBookObj.PhoneDTOlist.Count);

                foreach (PhoneDTO obj in addressBookObj.PhoneDTOlist)
                {
                    flag = ValidatePhoneDTOObject(sb, flag, obj);
                }
            }
            else /* Comment Added By Ismail If User Did Not provide Address DTO LIST it is ok as it is not compulsory*/
            {
             //   sb.Append("\n" + UserRegistrationConstant.REGISTRATION_PHONE_EMPTY); // Commented By ismail Feb 10 2011 6:27 PM
             //   flag = false; // Commented By ismail Feb 10 2011 6:27 PM
            }
            return flag;
        }

        public static bool ValidatePhoneDTOObject(StringBuilder sb, bool flag, PhoneDTO obj)
        {
            if (obj != null)
            {
                flag = ValidatePhoneNo(obj, sb, flag);

                flag = ValidatePhoneExt(obj, sb, flag);

                flag = ValidatePhoneType(obj, sb, flag);

                flag = ValidatePhoneOwnerType(obj, sb, flag);
            }
            else
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_PHONE_EMPTY);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateAddressDTO(AddressBookDTO addressBookObj, StringBuilder sb, bool flag)
        {
            if (addressBookObj.AddressDTOlist != null && addressBookObj.AddressDTOlist.Count > 0)
            {
                foreach (AddressDTO obj in addressBookObj.AddressDTOlist)
                {
                    if (addressBookObj != null)
                    {
                        flag = ValidateAddress(obj, sb, flag);           /* Validate State City Zip If Address Is Provided */

                        //flag = ValidateAddressByState(obj, sb, flag);   /* Validate Address City Zip If State Is Provided */

                        //flag = ValidateAddressByCity(obj, sb, flag);     /* Validate Address State Zip If City Is Provided */
                    }
                    else
                    {
                        sb.Append("\n" + UserRegistrationConstant.ADDRESS_BOOK_DTO_INVALID);
                        flag = false;
                    }
                }
            }
            else /* Comment Added By Ismail If User Did Not provide Address DTO LIST it is ok as it is not compulsory*/
            {
               // sb.Append("\n" + UserRegistrationConstant.REGISTRATION_ADDRESS_INVALID); // Coomented By ismail Feb 10 2011 6:27 PM
                // flag = false; // Coomented By ismail Feb 10 2011 6:27 PM
            }
            return flag;
        }

        private static bool ValidateAddressByCity(AddressDTO addressDTO, StringBuilder sb, bool flag)
        {
            if (ValidateCity(addressDTO))
            {
                if (string.IsNullOrEmpty(addressDTO.AddressTypeName))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_ADDRESS_INVALID);
                    flag = false;
                }
                if (!ValidateState(addressDTO))
                {
                    // Please Provide City Name
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_CITY_EMPTY);
                    flag = false;
                }

                if (!ValidateZipCode(addressDTO))
                {
                    // Please Provide Zip Name
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_ZIP_EMPTY);
                    flag = false;
                }
            }
            return flag;
        }

        private static bool ValidateAddressByState(AddressDTO addressDTO, StringBuilder sb, bool flag)
        {
            if (ValidateState(addressDTO))
            {
                // State Is Selected Validate Rest Of Address Information 

                if (string.IsNullOrEmpty(addressDTO.AddressTypeName))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_ADDRESS_INVALID);
                    flag = false;
                }
                if (!ValidateCity(addressDTO))
                {
                    // Please Provide City Name
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_CITY_EMPTY);
                    flag = false;
                }

                if (!ValidateZipCode(addressDTO))
                {
                    // Please Provide Zip Name
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_ZIP_EMPTY);
                    flag = false;
                }
            }
            return flag;
        }

        private static bool ValidateAddress(AddressDTO addressDTO, StringBuilder sb, bool flag)
        {
            QuivusLogger.Info("Calling Validation On ValidateAddress addressDTO.StreetName:: " + addressDTO.StreetName);

            if (!string.IsNullOrEmpty(addressDTO.StreetName))
            {  
                if (!ValidateState(addressDTO))
                {
                    // Please Provide State Name
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_STATE_EMPTY);
                    flag = false;
                }
                //if (!ValidateCity(addressDTO))
                //{
                //    // Please Provide City Name
                //    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_CITY_EMPTY);
                //    flag = false;
                //}
                if (!ValidateZipCode(addressDTO))
                {
                    // Please Provide Zip Name
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_ZIP_EMPTY);
                    flag = false;
                }
                // Adress Is Provided State City Zip Code Are Compulsory
            }
            return flag;
        }

        private static bool ValidateState(AddressDTO addressDTO)
        {
            QuivusLogger.Info("Calling Validation On ValidateState addressDTO.StateID:: " + addressDTO.StateID);

            if (addressDTO.StateID <= 0)
            {
                return false;
            }
            return true;
        }

        private static bool ValidateCity(AddressDTO addressDTO)
        {
            QuivusLogger.Info("Calling Validation On ValidateCity addressDTO.CityCode:: " + addressDTO.CityCode);
            if (addressDTO.CityCode.Equals("0"))
            {
                return false;
            }
            return true;
        }

        private static bool ValidateZipCode(AddressDTO addressDTO)
        {
            QuivusLogger.Info("Calling Validation On ValidateZipCode addressDTO.ZipCodeID:: " + addressDTO.ZipCodeID);
            if (addressDTO.ZipCodeID <= 0)
            {
                return false;
            }
            return true;
        }

        private static bool ValidatePhoneOwnerType(PhoneDTO phoneObj, StringBuilder sb, bool flag)
        {
            QuivusLogger.Info("Calling Validation On ValidatePhoneOwnerType phoneObj.OwnerTypeID:: " + phoneObj.OwnerTypeID);
           
            
            // temporary patch need to review it on 27 june 2011

            if(phoneObj.OwnerTypeID <= 0)
            phoneObj.OwnerTypeID = 1;

            if (phoneObj.OwnerTypeID <= 0)
            {
                

               
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_PHONE_OWNER_TYPE_EMPTY);
                flag = false;
            }
            return flag;
        }

        private static bool ValidatePhoneType(PhoneDTO phoneObj, StringBuilder sb, bool flag)
        {
            QuivusLogger.Info("Calling Validation On ValidatePhoneType phoneObj.NumberTypeID:: " + phoneObj.NumberTypeID);

            if (phoneObj.NumberTypeID <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_PHONE_TYPE_EMPTY);
                flag = false;
            }
            return flag;
        }

        private static bool ValidatePhoneExt(PhoneDTO phoneObj, StringBuilder sb, bool flag)
        {
            QuivusLogger.Info("Calling Validation On ValidatePhoneExt phoneObj.Extension:: -" + phoneObj.Extension);

            //if (!string.IsNullOrEmpty(phoneObj.Extension))
            //{
            //    if (!ValidatePhoneExtension(phoneObj.Extension.Trim()))
            //    {
            //        sb.Append("\n" + UserRegistrationConstant.REGISTRATION_PHONE_EXTENSION_INVALID);
            //        flag = false;
            //    }
            //}
            return flag;
        }

        private static bool ValidatePhoneNo(PhoneDTO phoneObj, StringBuilder sb, bool flag)
        {
            QuivusLogger.Info("Calling Validation On ValidatePhoneNo phoneObj.PhoneNumber:: " + phoneObj.PhoneNumber);

            if (string.IsNullOrEmpty(phoneObj.PhoneNumber))
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_PHONE_EMPTY);
                flag = false;
            }
            return flag;
        }

        private static bool ValidatePhoneExtension(string value)
        {
            if (!Regex.IsMatch(value, @"^\d{4}$"))
            {
                return false;
            }
            return true;
        }

       
    }
}
