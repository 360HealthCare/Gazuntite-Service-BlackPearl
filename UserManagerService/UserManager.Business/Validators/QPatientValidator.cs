using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.User.Patient;
using Quivus.Utility.Logger;
using Quivus.Utility.ProjectConstants;
using Quivus.Model.TypeDTOs;
using Quivus.Model.AddressBook;
using QuivusUtility.ProjectConstants;


namespace UserManager.Business.Validators
{
    public class QPatientValidator : QValidator
    {
        /// <summary>
        /// Author Muhammad Ismail 
        /// Created Date 25 Jan 2011
        /// Method used To validate EmergencyContact Info list :)
        /// </summary>
        /// <param name="PatientID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="EmergencyContactList"></param>
        /// <param name="sb"></param>
        /// <returns></returns>
        public static bool ValidatePatientEmergencyContactList(int PatientID, int CreatedBy, List<PatientEmergencyContactDTO> EmergencyContactList, ref StringBuilder sb)
        {
            bool flag = true;

            QuivusLogger.Info("calling ValidatePatientEmergencyContactList ...");

            if (EmergencyContactList != null && EmergencyContactList.Count > 0)
            {
                QuivusLogger.Info("ValidatePatientEmergencyContactList List Count :: " + EmergencyContactList.Count);

                if (PatientID <= 0)
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_PATIENTID_MISSING);
                    flag = false;
                }
                flag = ValidateCreatedBy(CreatedBy, sb, flag);

                foreach (PatientEmergencyContactDTO obj in EmergencyContactList)
                {
                    QuivusLogger.Info("Calling Validation On EmergencyContactList FirstName:: " + obj.FirstName);
                    flag = ValidateEmergencyContactName(obj.FirstName, sb, flag);

                    //M Removed by me - 7 Feb 2011
                    //QuivusLogger.Info("Calling Validation On EmergencyContactList LastName:: " + obj.LastName);
                    //flag = ValidateLastName(obj.LastName, sb, flag);

                    QuivusLogger.Info("Calling Validation On EmergencyContactList RelationShip:: " + obj.RelationShipType);
                    flag = ValidateRelationShip(obj.RelationShipType, sb, flag);

                    QuivusLogger.Info("Calling Validation On EmergencyContactList StatusType:: " + obj.StatusType);
                    flag = ValidateAccountStatus(obj.StatusType, sb, flag);

                    if (obj.EmergencyPhone != null) // Only Validate If Provided Not Required According to Dosument :)
                        flag = QAddressBookValidator.ValidatePhoneDTOObject(sb, flag, obj.EmergencyPhone);  // Added By Ismail Need To Verify Phone Number if Added  :)
                }
            }
            return flag;
        }


        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 25 Jan 2011
        /// Method used to validate patient RelationShip Id
        /// </summary>
        /// <param name="relation"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected static bool ValidateRelationShip(RelationshipTypeDTO relation, StringBuilder sb, bool flag)
        {
            if (relation == null || relation.RelationshipId <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_PATIENT_RELATIONSHIP_EMPTY);
                flag = false;
            }
            return flag;
        }

        public static bool ValidatePatientBasicInfo(PatientDTO patientDTO, ref StringBuilder sb)
        {
            bool flag = true;

            if (patientDTO != null)
            {
                flag = ValidateAccountStatus(patientDTO.StatusType, sb, flag);

                flag = ValidateMaritalStatus(patientDTO.MaritalStatusType, sb, flag);

                flag = ValidatePatientDOB(patientDTO.DateOfBirth, sb, flag);

                flag = ValidateRaceType(patientDTO.RaceType, sb, flag);

                flag = ValidateEthenicityType(patientDTO.EthinicityType, sb, flag);

                // can be empty
                //flag = ValidateMedicareNumber(patientDTO.MedicareNumber, sb, flag);

                flag = ValidateMotherName(patientDTO.MotherMaidenName, sb, flag);

                // flag = ValidateMotherName(patientDTO.MotherMaidenName, sb, flag); //called twice commented by ismail

                flag = ValidateFatherName(patientDTO.FatherName, sb, flag);

                flag = ValidateEmployerName(patientDTO.EmployerName, sb, flag);

                flag = ValidateOccupation(patientDTO.OccupationType, sb, flag);

                if (patientDTO.PatientId > 0)
                    flag = ValidateModifiedBy(patientDTO.ModifiedBy, sb, flag);
                    
                else
                    flag = ValidateCreatedBy(patientDTO.CreatedBy, sb, flag);
            }
            else
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_DTO_INVALID);
                flag = false;
            }
            return flag;
        }

        public static bool ValidatePatientBasicInfo(PatientDTO patientDTO, ref StringBuilder sb, bool IsUpdate)
        {
            bool flag = true;

            if (patientDTO != null)
            {
                flag = ValidateAccountStatus(patientDTO.StatusType, sb, flag);

                flag = ValidateMaritalStatus(patientDTO.MaritalStatusType, sb, flag);

                flag = ValidatePatientDOB(patientDTO.DateOfBirth, sb, flag);

                flag = ValidateRaceType(patientDTO.RaceType, sb, flag);

                flag = ValidateEthenicityType(patientDTO.EthinicityType, sb, flag);

                // can be empty
                //flag = ValidateMedicareNumber(patientDTO.MedicareNumber, sb, flag);

                flag = ValidateMotherName(patientDTO.MotherMaidenName, sb, flag);

                // flag = ValidateMotherName(patientDTO.MotherMaidenName, sb, flag); //called twice commented by ismail

                flag = ValidateFatherName(patientDTO.FatherName, sb, flag);

                flag = ValidateEmployerName(patientDTO.EmployerName, sb, flag);

                flag = ValidateOccupation(patientDTO.OccupationType, sb, flag);

                flag = ValidateModifiedBy(patientDTO.ModifiedBy, sb, flag);

            }
            else
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_DTO_INVALID);
                flag = false;
            }
            return flag;
        }

        public static bool ValidatePatientLanguages(List<PatientLanguageDTO> PatientLanguageDTOList, ref StringBuilder sb)
        {
            bool flag = true;

            if (PatientLanguageDTOList != null && PatientLanguageDTOList.Count > 0)
            {
                foreach (PatientLanguageDTO obj in PatientLanguageDTOList)
                {
                    if (obj != null)
                    {

                        flag = ValidateLanguageType(obj, sb, flag);

                        flag = ValidateLanguageAbilityMode(obj.PatientLanguageAbilityMode, sb, flag);

                        flag = ValidateLanguageCountry(obj.Country, sb, flag);
                    }
                    else
                    {
                        sb.Append("\n" + UserRegistrationConstant.PATIENT_LANGUAGE_TYPE_EMPTY);
                        flag = false;
                    }
                }
            }
            return flag;
        }

        public static bool ValidateGuarantorInformation(PatientGuarantorDTO guarantorDTO,bool IsUpdate, ref StringBuilder sb)
        {
            //if (!string.IsNullOrEmpty(guarantorDTO.GurantorFirstName)
            //     || !string.IsNullOrEmpty(guarantorDTO.GuarantorLastName)
            //     || guarantorDTO.RelationshipType.RelationshipId > 0
            //     || guarantorDTO.GenderType.GenderId > 0
            //     || !string.IsNullOrEmpty(guarantorDTO.DOB.ToString())
            //     || !string.IsNullOrEmpty(guarantorDTO.AddressBook.AddressDTOlist[0].StreetName)
            //     || registrationControl.User.PatientDTO.GuarantorDTO.AddressBook.AddressDTOlist[0].StateID > 0
            //     || registrationControl.User.PatientDTO.GuarantorDTO.AddressBook.AddressDTOlist[0].ZipCodeID > 0
            //     || !string.IsNullOrEmpty(registrationControl.User.PatientDTO.GuarantorDTO.AddressBook.PhoneDTOlist[0].PhoneNumber)
            //     || !string.IsNullOrEmpty(registrationControl.User.PatientDTO.GuarantorDTO.AddressBook.PhoneDTOlist[0].Extension)
            //     || registrationControl.User.PatientDTO.GuarantorDTO.AddressBook.PhoneDTOlist[0].NumberTypeID > 0
            //    )

            bool flag = true;

            QuivusLogger.Info("Validation Started on  method ValidateGuarantorInformation :: ");

            if (guarantorDTO == null)
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_GURANTOR_EMPTY);
                flag = false;
            }
            else
            {
                flag = ValidateGuarantorFirstName(guarantorDTO.GurantorFirstName, sb, flag);
                flag = ValidateGuarantorLastName(guarantorDTO.GuarantorLastName, sb, flag);

                flag = ValidateGuarantorRelationShip(guarantorDTO.RelationshipType, sb, flag);

                # region Validate Address Book Only In Case If Provided As It is Gurantor
                if (guarantorDTO.AddressBook != null)
                {
                    flag = QAddressBookValidator.ValidateAddressBook(guarantorDTO.AddressBook, ref sb); // Added By Ismail  Use To Validate Gurantor Address Book
                }
                # endregion

                //M In the Address Book
                // flag = ValidateGuarantorAddressInformation(guarantorDTO.AddressBook, sb, flag);
                //flag = ValidateGuarantorPhonenumber(guarantorDTO.AddressBook, sb, flag);
            }
            return flag;
        }

        public static bool ValidateHealthInformation(PatientHealthDTO patientHealthDTO, ref StringBuilder sb)
        {
            bool flag = true;

            if (patientHealthDTO != null)
            {
                if (patientHealthDTO.PatientAllergyDTOList != null && patientHealthDTO.PatientAllergyDTOList.Count > 0)
                {
                    foreach (PatientAllergyDTO obj in patientHealthDTO.PatientAllergyDTOList)
                    {
                        if (obj != null)
                        {
                            flag = ValidateAllergy(obj, sb, flag);
                        }
                        else
                        {
                            sb.Append("\n" + UserRegistrationConstant.PATIENT_ALLERGY_EMPTY);
                            flag = false;
                        }
                    }
                }

                if (patientHealthDTO.PatientMedicalHistoryDTOList != null && patientHealthDTO.PatientMedicalHistoryDTOList.Count > 0)
                {
                    foreach (PatientMedicalHistoryDTO obj in patientHealthDTO.PatientMedicalHistoryDTOList)
                    {
                        if (obj != null)
                        {
                            flag = ValidateMedicalHistory(obj, sb, flag);
                        }
                        else
                        {
                            sb.Append("\n" + UserRegistrationConstant.PATIENT_MEDICAL_HISTORY_EMPTY);
                            flag = false;
                        }
                    }
                }

                if (patientHealthDTO.PatientFamilyHistoryDTOList != null && patientHealthDTO.PatientFamilyHistoryDTOList.Count > 0)
                {
                    foreach (PatientFamilyHistoryDTO obj in patientHealthDTO.PatientFamilyHistoryDTOList)
                    {
                        if (obj != null)
                        {
                            flag = ValidateFamilyHistory(obj, sb, flag);
                        }
                        else
                        {
                            sb.Append("\n" + UserRegistrationConstant.PATIENT_MEDICAL_HISTORY_EMPTY);
                            flag = false;
                        }
                    }
                }
            }
            return flag;
        }

        public static bool ValidateInsuranceInformation(List<PatientInsuranceDTO> patientInsuranceDTOList, ref StringBuilder sb)
        {
            bool flag = true;

            if (patientInsuranceDTOList != null && patientInsuranceDTOList.Count > 0)
            {
                foreach (PatientInsuranceDTO obj in patientInsuranceDTOList)
                {
                    flag = ValidateInsurance(obj, sb, flag);
                }
            }
            else if (patientInsuranceDTOList != null && patientInsuranceDTOList.Count > 2)
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_INSURANCE_COUNT_INVALID);
                flag = false;
            }
            else
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_INSURANCE_EMPTY);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Author Mansoor
        /// Created Date 24 jan 2011
        /// Validate Marital Status
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private static bool ValidateMaritalStatus(MaritalStatusTypeDTO maritalStatus, StringBuilder sb, bool flag)
        {
            if (maritalStatus != null && maritalStatus.MaritalStatusId <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.PATIENT_MARITAL_STATUS_EMPTY);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Author Mansoor
        /// Created Date 24 jan 2011
        /// Validate Race Type
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private static bool ValidateRaceType(RaceTypeDTO raceType, StringBuilder sb, bool flag)
        {
            if (raceType != null && raceType.RaceId <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.PATIENT_RACE_TYPE_EMPTY);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Author Mansoor
        /// Created Date 24 jan 2011
        /// Validate Ethinicity
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private static bool ValidateEthenicityType(EthinicityTypeDTO ethinicityType, StringBuilder sb, bool flag)
        {
            if (ethinicityType != null && ethinicityType.EthinicityId <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.PATIENT_ETHINICITY_TYPE_EMPTY);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateMedicareNumber(string medicareNumber, StringBuilder sb, bool flag)
        {
            if (!string.IsNullOrEmpty(medicareNumber))
            {
                if (Validations.IsAlphaNumeric(medicareNumber))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_MEDICARE_INVALID);
                    flag = false;
                }
            }
            return flag;
        }

        private static bool ValidateMotherName(string motherName, StringBuilder sb, bool flag)
        {
            if (!string.IsNullOrEmpty(motherName))
            {
                if (Validations.IsAlphaNumericWithSpace(motherName))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_MOTHER_MAIDENNAME_INVALID);
                    flag = false;
                }
            }
            return flag;
        }

        private static bool ValidateFatherName(string fatherName, StringBuilder sb, bool flag)
        {
            if (!string.IsNullOrEmpty(fatherName))
            {
                if (Validations.IsAlphaNumericWithSpace(fatherName))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_FATHER_NAME_INVALID);
                    flag = false;
                }
            }
            return flag;
        }

        private static bool ValidateEmployerName(string employerName, StringBuilder sb, bool flag)
        {
            if (!string.IsNullOrEmpty(employerName))
            {
                // Commented As it allow all characters :)

                //if (Validations.IsAlphaNumeric(employerName))
                //{
                //    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_Employer_INVALID);
                //    flag = false;
                //}
            }
            return flag;
        }

        private static bool ValidateOccupation(OccupationTypeDTO occupationType, StringBuilder sb, bool flag)
        {
            if (occupationType != null && occupationType.OccupationId <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.PATIENT_OCCUPATION_TYPE_EMPTY);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateLanguageType(PatientLanguageDTO languageType, StringBuilder sb, bool flag)
        {
            if (languageType != null && languageType.LanguageID <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.PATIENT_LANGUAGE_TYPE_EMPTY);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateLanguageAbilityMode(LanguageAbilityModeDTO languageAbilityMode, StringBuilder sb, bool flag)
        {
            if (languageAbilityMode != null && languageAbilityMode.LanguageAbilityModeId <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.PATIENT_LANGUAGE_ABILITY_MODE_EMPTY);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateLanguageCountry(CountryDTO country, StringBuilder sb, bool flag)
        {
            if (country != null && country.CountryID <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.PATIENT_LANGUAGE_COUNTRY_EMPTY);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateGuarantorFirstName(String firstName, StringBuilder sb, bool flag)
        {
            QuivusLogger.Info("Validating Guarantor FirstName " + firstName);
            if (string.IsNullOrEmpty(firstName))
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_GURANTOR_FIRST_NAME_EMPTY);
                flag = false;
            }
            else if (Validations.IsAlphaNumeric(firstName))
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_GURANTOR_FIRST_NAME_INVALID);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateGuarantorLastName(String lastName, StringBuilder sb, bool flag)
        {
            QuivusLogger.Info("Validating Guarantor lastName " + lastName);
            if (string.IsNullOrEmpty(lastName))
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_GURANTOR_LAST_NAME_EMPTY);
                flag = false;
            }
            else if (Validations.IsAlphaNumeric(lastName))
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_GURANTOR_LAST_NAME_INVALID);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateGuarantorRelationShip(RelationshipTypeDTO relation, StringBuilder sb, bool flag)
        {
            QuivusLogger.Info("Validating Guarantor relation " + relation);
            if (relation == null || relation.RelationshipId <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.REGISTRATION_GUARANTOR_RELATION_EMPTY);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateInsurance(PatientInsuranceDTO patientInsurance, StringBuilder sb, bool flag)
        {
            QuivusLogger.Info("Calling Validation On Insurance Information");
            if (patientInsurance == null)
            {
                flag = false;
            }
            else
            {
                if (patientInsurance.InsuranceType == null || patientInsurance.InsuranceType.InsuranceTypeId <= 0)
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_INSURANCE_TYPE);
                    flag = false;
                }
                if (patientInsurance.InsuranceNature == null || patientInsurance.InsuranceNature.InsuranceNatureId <= 0)
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_INSURANCE_NATURE); // Please Use Appropriate Messages Commented By Ismail :)
                    flag = false;
                }
                if (patientInsurance.InsuranceOrganization != null && patientInsurance.InsuranceOrganization.InsuranceOrganizationID <= 0) // Update By Ismail InsuranceOrganization is not required Check Only if provided :)
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_INSURANCE_ORGANIZATION); // Please Use Appropriate Messages Commented By Ismail :)
                    flag = false;
                }
                if (string.IsNullOrEmpty(patientInsurance.PolicyNumber))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_INSURANCE_POLICY_EMPTY);
                    flag = false;
                }
                if (string.IsNullOrEmpty(patientInsurance.InsuredPersonName))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_INSURANCE_PERSON_NAME_EMPTY);
                    flag = false;
                }
                if (!Validations.IsValidateDateTime(patientInsurance.StartDate))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_INSURANCE_START_DATE_EMPTY);
                    flag = false;
                }
                if (!Validations.IsValidateDateTime(patientInsurance.EndDate))
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_INSURANCE_END_DATE_EMPTY);
                    flag = false;
                }
               /* if (patientInsurance.StartDate >= patientInsurance.EndDate)
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_INSURANCE_INVALID_DATE);
                    flag = false;
                }*/
                if (patientInsurance.InsuranceStatus != null && patientInsurance.InsuranceStatus.StatusID <= 0)// Update By Ismail InsuranceStatus is not required Check Only if provided :)
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_INSURANCE_STATUS_INVALID);
                    flag = false;
                }
                if (patientInsurance.InsuredPersonDOB != null &&
                    !Validations.IsValidateDateTime(patientInsurance.InsuredPersonDOB)) // Optional InsuredPersonDOB is not required
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_INSURANCE_PERSON_DOB_INVALID);
                    flag = false;
                }
                if (patientInsurance.InsuredPersonGender != null && patientInsurance.InsuredPersonGender.GenderId <= 0) // Optional check only if provided 
                {
                    sb.Append("\n" + UserRegistrationConstant.REGISTRATION_INSURANCE_PERSON_GENDER_INVALID);
                    flag = false;
                }
            }
            return flag;
        }

        private static bool ValidateAllergy(PatientAllergyDTO allergy, StringBuilder sb, bool flag)
        {
            if (allergy.AllergyType == null || allergy.AllergyType.AllergyTypeIdSeq <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.PATIENT_ALLERGY_TYPE_INVALID);
                flag = false;
            }
            if (string.IsNullOrEmpty(allergy.Reaction))
            {
                sb.Append("\n" + UserRegistrationConstant.PATIENT_ALLERGY_REACTION_INVALID);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateMedicalHistory(PatientMedicalHistoryDTO medicalHistory, StringBuilder sb, bool flag)
        {
            if (medicalHistory.ICDCodeType == null || medicalHistory.ICDCodeType.IcdCodeIdSeq <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.PATIENT_MEDICAL_HISTORY_ICD_CODE_INVALID);
                flag = false;
            }
            if (medicalHistory.ICDCodeType == null || string.IsNullOrEmpty(medicalHistory.ICDCodeType.ProblemType))
            {
                sb.Append("\n" + UserRegistrationConstant.PATIENT_MEDICAL_HISTORY_ICD_CODE_PROBLEM_INVALID);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateFamilyHistory(PatientFamilyHistoryDTO familyHistory, StringBuilder sb, bool flag)
        {
            if (familyHistory.ICDCodeType == null || familyHistory.ICDCodeType.IcdCodeIdSeq <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.PATIENT_FAMILY_HISTORY_ICD_CODE_INVALID);
                flag = false;
            }
            if (familyHistory.ICDCodeType != null && string.IsNullOrEmpty(familyHistory.ICDCodeType.ProblemType))
            {
                sb.Append("\n" + UserRegistrationConstant.PATIENT_FAMILY_HISTORY_ICD_CODE_PROBLEM_INVALID);
                flag = false;
            }
            if (familyHistory.RelationshipType == null || familyHistory.RelationshipType.RelationshipId <= 0)
            {
                sb.Append("\n" + UserRegistrationConstant.PATIENT_FAMILY_HISTORY_RELATION_PROBLEM_INVALID);
                flag = false;
            }
            return flag;
        }

        private static bool? NotEmptyAndAlphaNumeric(string value)
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

        internal static bool ValidatePatientDTO(PatientDTO patientDTO, ref StringBuilder sb, bool IsUpdate)
        {
            bool flag = true;

                  

            if (patientDTO != null)
            {
                if (!IsUpdate)
                {
                    flag = ValidateKeyId(patientDTO.CreatedBy, sb, flag, ErrorMessages.INVALID_CREATEDBY_ERROR_MSG);
                    flag = ValidateDate(patientDTO.CreatedAt, sb, flag, ErrorMessages.INVALID_CREATEDAT_ERROR_MSG);

                }
                if (IsUpdate)
                {
                    flag = ValidateKeyId(patientDTO.ModifiedBy, sb, flag, ErrorMessages.INVALID_MODIFIEDBY_ERROR_MSG);
                }

                flag = ValidateKeyId(patientDTO.UserId, sb, flag, ErrorMessages.USER_ID_INVALID_ERROR_MSG);
                flag = ValidateDate(patientDTO.DateOfBirth, sb, flag, ErrorMessages.PATIENT_Date_OF_BIRTH_ERROR_MSG);
                flag = ValidateStringInfo(patientDTO.QuivusId, sb, flag, ErrorMessages.PATIENT_QUIVUS_ID_ERROR_MSG);
                ValidateAccountStatus(patientDTO.StatusType, sb, flag);

                if (patientDTO.RecGuid == Guid.Empty)
                {
                    sb.Append("\n" + ErrorMessages.INVALID_RECGUID_ERROR_MSG);
                    flag = false;
                }

            }
            else
            {
                sb.Append("\n" + ErrorMessages.PATIENT_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }
    }
}
