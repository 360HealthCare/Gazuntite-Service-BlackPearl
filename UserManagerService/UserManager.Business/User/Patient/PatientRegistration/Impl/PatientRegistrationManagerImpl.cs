using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Quivus.Model.User;
using UserManager.Business.Validators;
using Quivus.Utility.CustomException;

using Quivus.Model.User.Patient;
using Quivus.Utility.Logger;
using Quivus.Utility;
using Quivus.Utility.Factory;
using System.Configuration;
using System.Reflection;
using UserManagerService.DataAccess.Patient;
using Quivus.Utility.Utilities;


using Quivus.Model.AddressBook;
using UserManager.Business.Utility;
using UserManager.Business.AddressBook;
using UserManager.Business.AddressBook.Impl;
using QuivusModel.Clinic;

namespace UserManager.Business.User.Patient.PatientRegistration.Impl
{
    public class PatientRegistrationManagerImpl : IPatientRegistrationManager
    {
        PatientDAO patientDAO = null;
        IAddressBookManager addressBookManager = null;

        //  IAddressBookService addressBookServiceManager = null;

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 24 Jan 2011
        /// This Method Used To Insert/Update/Delete The patient emergency contact Information :)
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="EmergencyContactList"></param>
        /// <returns></returns>
        public int ManagePatientEmergencyContact(List<PatientEmergencyContactDTO> EmergencyContactList, int PatientId, int CreatedBy)
        {
            int UpdateResult = 0;
            List<PatientEmergencyContactDTO> InsertList = null;
            List<PatientEmergencyContactDTO> UpdateList = null;
            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManagePatientEmergencyContact EmergencyContactList :: " + EmergencyContactList + " PatientId:: " + PatientId + "CreatedBy :: " + CreatedBy);

                if (QPatientValidator.ValidatePatientEmergencyContactList(PatientId, CreatedBy, EmergencyContactList, ref sb))
                {
                    QuivusLogger.Info("Validation On Method ManagePatientEmergencyContact Gets SuccessFUll ");

                    if (patientDAO == null)
                    {
                        patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                    }

                    if (EmergencyContactList != null && EmergencyContactList.Count > 0)
                    {

                        QuivusLogger.Info("Seprating Emergency List & Calling DAO To Save");

                        //  ListSeprationUtility.SeprateEmergencyContactList(EmergencyContactList, ref InsertList, ref UpdateList); // This Method Seprate The List To Inset/Update 

                        // UpdateResult = patientDAO.ManagePatientEmergencyContact(InsertList, UpdateList, PatientId, CreatedBy);
                        UpdateResult = patientDAO.ManagePatientEmergencyContact(EmergencyContactList, UpdateList, PatientId, CreatedBy);

                        /* This Part should Moved To Main Quivus Manager Service*/
                        //  UpdateResult = SavePatientEmenrgencyAddressBook(EmergencyContactList, CreatedBy, UpdateResult); // Save Pateint Emergency Contact AddressBook :)
                    }
                }
                else
                {
                    QuivusLogger.Info("Validation On Method ManagePatientEmergencyContact Gets Failed");
                    // Validation Gets Failed Thorw Exception
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }// End Try Block
            catch
            {
                throw;
            }
            return UpdateResult;
        }


        //Reviewd by Mansoor
        //this strategy shd be redefine 
        // in case save the bulk adderss books or have to manage the EC phone in some other way

        private static int SavePatientEmenrgencyAddressBook(List<PatientEmergencyContactDTO> EmergencyContactList, int CreatedBy, int UpdateResult)
        {
            AddressBookDTO addressBookDTO = null;
            List<PhoneDTO> Phonelist = null;
            // addressBookDTO.PhoneDTOlist = Phonelist;

            foreach (PatientEmergencyContactDTO obj in EmergencyContactList)
            {
                if (obj != null)
                {
                    if (obj.EmergencyPhone != null)
                    {
                        Phonelist = new List<PhoneDTO>();
                        Phonelist.Add(obj.EmergencyPhone);
                    }
                    if (Phonelist != null)
                    {
                        addressBookDTO = new AddressBookDTO();
                        addressBookDTO.PhoneDTOlist = Phonelist;
                        addressBookDTO.AddressDTOlist = null;
                        addressBookDTO.PartyID = obj.RecGuid;
                        addressBookDTO.AddressPartyTypeID = (int)ProjectConstant.ADDRESS_PARTY_TYPE.EmergencyContact;

                        //   IAddressBookService addressBookManager = new AddressBookServiceImpl();
                        //   UpdateResult = addressBookManager.ManageAddressBook(CreatedBy, addressBookDTO);

                        Phonelist = null;
                        addressBookDTO = null;
                    }
                }
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011
        /// Save the patient profile and language w.r.t the userId provided
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="patientDTO"></param>
        /// <param name="PatientLanguageDTOList"></param>
        /// <returns></returns>
        public int SavePatientBasicProfileAndLangauge(int userId, int managedByUserID, PatientDTO patientDTO, List<PatientLanguageDTO> PatientLanguageDTOList)
        {
            int UpdateResult = 0;
            bool IsUpdate = false;
            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling SavePatientBasicProfileAndLangauge:: userId " + userId + " managedByUserID " + managedByUserID);
                QuivusLogger.Info("Date of Birth :" + patientDTO.DateOfBirth);

                if (patientDTO != null && patientDTO.PatientId > 0)
                {
                    IsUpdate = true;
                }
                if (userId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }
                else if (managedByUserID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }

                /*Only Handle Update Case For Profile Info If Update Call do not Skip Language Validation */

                else if ((QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb)) && (QPatientValidator.ValidatePatientLanguages(PatientLanguageDTOList, ref sb)))
                {
                    if (patientDAO == null)
                    {
                        patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                    }

                    if (patientDTO.RecGuid == System.Guid.Empty)
                    {
                        patientDTO.RecGuid = System.Guid.NewGuid();
                    }

                    try
                    {
                        patientDTO.IVR_PINCODE = Int32.Parse(QuivusUtility.Utilities.GeneralUtil.GetUniqueNumber(4));
                    }
                    catch
                    {
                        patientDTO.IVR_PINCODE = 1234;
                    }
                    UpdateResult = patientDAO.SavePatientBasicProfile(userId, patientDTO, IsUpdate);

                    if (PatientLanguageDTOList != null && PatientLanguageDTOList.Count > 0)
                    {
                        patientDAO.SavePatientLanguages(UpdateResult, userId, PatientLanguageDTOList);
                    }
                }
                else
                {
                    // Validation Gets Failed Thorw Exception
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }

                # region Commented
                /* Commented Coz of Updated Case handled through TVP Tuesday 21 2011
                 * 
                 * 
                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }
                if (patientDTO.RecGuid == System.Guid.Empty)
                {
                    patientDTO.RecGuid = System.Guid.NewGuid();
                }

          
             * if (!IsUpdate)
                {
                    if (QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb) && QPatientValidator.ValidatePatientLanguages(PatientLanguageDTOList, ref sb))
                    {
                        try
                        {
                            patientDTO.IVR_PINCODE = Int32.Parse(Quivus.Utility.Utilities.GeneralUtil.GetUniqueNumber(4));
                        }
                        catch
                        {
                            patientDTO.IVR_PINCODE = 1234;
                        }
                        UpdateResult = patientDAO.SavePatientBasicProfile(userId, patientDTO, IsUpdate);

                        if (PatientLanguageDTOList != null && PatientLanguageDTOList.Count > 0)
                        {
                            patientDAO.SavePatientLanguages(UpdateResult, userId, PatientLanguageDTOList);
                            // Update This method to TVP Style Updated On thuesday 21 2011 by Ismail
                        }
                    }
                    else
                    { 
                    // thorw exception
                        throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG,sb.ToString());
                    }
                }
                else
                {
                    if (QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb, IsUpdate))
                    {
                        UpdateResult = patientDAO.SavePatientBasicProfile(userId, patientDTO, IsUpdate);
                    }
                    else
                    {
                        throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                    }
                } */
                #endregion Commented End
            }

            catch
            {
                throw;
            }

            return UpdateResult;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011
        /// Save Patient gurantor Info
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientGurantorDTO"></param>
        /// <returns></returns>
        /// <Code> <review> Done By Baber Waqas  on 1/20/2012 plus </review> <enhancements> Done by Baber Waqas in result of code review</enhancements></Code>
        public int ManagePatientGurantor(int patientID, int managedByUserID, PatientGuarantorDTO patientGurantorDTO)
        {
            var response = 0;
            var isUpdate = false;
            try
            {
                var sb = new StringBuilder();

                if (patientGurantorDTO != null && patientGurantorDTO.GuarantorId > 0)
                    isUpdate = true;

                QuivusLogger.Info("Calling ManagePatientGurantor PARAM:: "
                    +"patientID " + patientID + 
                    " managedByUserID " + managedByUserID 
                    + "GuarantorFirstName "+patientGurantorDTO.GurantorFirstName
                    +"GuarantorLastName " + patientGurantorDTO.GuarantorLastName
                    + "GenderID " + patientGurantorDTO.GenderType.GenderId
                    + "RelationShipID " + patientGurantorDTO.RelationshipType.RelationshipId
                    + "DateOfBirth " + patientGurantorDTO.DOB
                    + "StatusID " + patientGurantorDTO.StatusType.StatusID
                    + "RecGuid " + patientGurantorDTO.RecGuid
                    + "PatientGuarantorID " + patientGurantorDTO.GuarantorId);

                if (patientID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }
                else if (managedByUserID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }

                /* Updated By Ismail Need to be discussed */
                else if (patientGurantorDTO == null)
                {
                    QuivusLogger.Info("patientGurantorDTO is Empty Simply Return Success");

                }
                /* End Of Ismail Comments*/

                else if (QPatientValidator.ValidateGuarantorInformation(patientGurantorDTO,isUpdate, ref sb) && QAddressBookValidator.ValidateAddressBook(patientGurantorDTO.AddressBook, ref sb))
                {
                    QuivusLogger.Info("Validation Gets Cleared On ManagePatientGurantor");

                    if (patientDAO == null)
                    {
                        patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                    }
                    if (patientGurantorDTO.RecGuid == System.Guid.Empty)
                    {
                        patientGurantorDTO.RecGuid = System.Guid.NewGuid(); // create gurantor guid
                    }
                    if (patientGurantorDTO.AddressBook != null && patientGurantorDTO.AddressBook.PartyID == System.Guid.Empty)
                    {
                        patientGurantorDTO.AddressBook.PartyID = patientGurantorDTO.RecGuid; // assign the gurantor created guid to addresss book party id
                    }

                    response = patientDAO.ManagePatientGurantor(patientID, managedByUserID, patientGurantorDTO,isUpdate);
                    //save address book
                    //  IAddressBookService addressBookManager = new AddressBookServiceImpl();
                    //  addressBookManager.ManageAddressBook(managedByUserID, patientGurantorDTO.AddressBook);
                }
                else
                {
                    // Validation Gets Failed Thorw Exception
                    QuivusLogger.Info("Validation Gets Failed !! On ManagePatientGurantor");
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011 
        /// Save Patient Insurance
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientInsuranceDTOList"></param>
        /// <returns></returns>
        public int ManagePatientInsuranceInfo(int patientID, int managedByUserID, List<PatientInsuranceDTO> patientInsuranceDTOList)
        {
            int UpdateResult = 0;
            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatientInsuranceInfo PARAM:: patientID " + patientID + " managedByUserID " + managedByUserID + " patientInsuranceDTOList " + patientInsuranceDTOList);

                if (patientID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }
                else if (managedByUserID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }
                else if (patientInsuranceDTOList != null && patientInsuranceDTOList.Count <= 0) // Added By ismail need to be discussed
                {
                    //QuivusLogger.Info("on ManagePatientInsuranceInfo list is empty just return & Say Success");

                    //if (response == null)
                    //{
                    //    response = new ServiceResponse();
                    //    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                    //}
                }
                else
                {
                    if (QPatientValidator.ValidateInsuranceInformation(patientInsuranceDTOList, ref sb))
                    {
                        QuivusLogger.Info("Validation Gets Cleared On ManagePatientInsuranceInfo Ready To Call DAO To Save");
                        if (patientDAO == null)
                        {
                            patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                        }

                        UpdateResult = patientDAO.ManagePatientInsuranceInfo(patientID, managedByUserID, patientInsuranceDTOList);
                    }
                    else
                    {
                        QuivusLogger.Info("Validation Gets Failed On ManagePatientInsuranceInfo");
                        // Validation Gets Failed Thorw Exception
                        throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                    }
                }
            }
            catch
            {
                throw;
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011 
        /// save the patient health Info like allergies, medical history, family history
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="managedByUserID"></param>
        /// <param name="patientHealthDTO"></param>
        /// <returns></returns>
        public int ManagePatientHealthInfo(int patientID, int managedByUserID, PatientHealthDTO patientHealthDTO)
        {
            int UpdateResult = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManagePatientHealthInfo PARAM:: patientID " + patientID + " managedByUserID " + managedByUserID);

                if (patientID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }
                else if (managedByUserID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }
               
                else if (QPatientValidator.ValidateHealthInformation(patientHealthDTO, ref sb))
                {
                    if (patientDAO == null)
                    {
                        patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                    }

                    UpdateResult = patientDAO.ManagePatientHealthInfo(patientID, managedByUserID, patientHealthDTO);
                }
                else
                {
                    // Validation Gets Failed Thorw Exception
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 2 Feb 2011
        /// Load the patient profile and also save the required info in the provider's most recent patient list.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <returns></returns>     
        public PatientProfileDTO LoadPatientCompleteProfilebyPatientId(int patientId, int clinicBranchId, int providerUserId)
        {
            PatientProfileDTO result = null;

            try
            {
                QuivusLogger.Info("Calling LoadPatientProfilebyPatientId PARAM:: patientId " + patientId + " clinicBranchId" + clinicBranchId + " providerUserId" + providerUserId);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }
                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                }
                if (providerUserId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PROFILE_ID_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }
                if (addressBookManager == null)
                {
                    addressBookManager = (AddressBookManagerImpl)DAOFactory.GetDataAccessObject(typeof(AddressBookManagerImpl));
                }

                //Here comes all the logic to load all data

                result = new PatientProfileDTO();

                result.UserDTO = patientDAO.LoadPatientBaseUserInfoByPatientIdForCompleteProfile(patientId);
                result.PatientDTO = patientDAO.LoadPatientInfoByPatientIdForCompleteProfile(patientId);

                PhoneDTO phone = new PhoneDTO();
                phone = addressBookManager.GetPartyPrimaryPhoneNoByRegGuid(result.PatientDTO.RecGuid);
                if (phone != null)
                {
                    result.PatientDTO.PatientPhoneNumber = phone.PhoneNumber;
                }

                result.PatientLanguageList = patientDAO.LoadPatientLanguageInfoByPatientIdForCompleteProfile(patientId);
                result.PatientEmergencyContactList = patientDAO.LoadPatientEmergencyContactInfoByPatientIdForCompleteProfile(patientId);
                result.PatientGurantor = patientDAO.LoadPatientGurantorInfoByPatientIdForCompleteProfile(patientId);

                result.PatientAllergyList = patientDAO.LoadPatientAllergyInfoByPatientIdForCompleteProfile(patientId);
                result.PatientMedicalHistoryList = patientDAO.LoadPatientMedicalHistoryInfoByPatientIdForCompleteProfile(patientId);
                result.PatientFamilyHistoryList = patientDAO.LoadPatientFamilyHistoryInfoByPatientIdForCompleteProfile(patientId);

                result.PatientInsuranceList = patientDAO.LoadPatientInsuranceInfoByPatientIdForCompleteProfile(patientId);

                // now get the AB

                //if (addressBookServiceManager == null)
                //{
                //    addressBookServiceManager = new AddressBookServiceImpl();
                //}

                //if (result.PatientDTO != null)
                //{
                //    // Get patient address book
                //    result.PatientAddressBook = addressBookServiceManager.GetAddressBookInfoByGuid(result.PatientDTO.RecGuid);
                //}

                //if (result.PatientGurantor != null)
                //{
                //    // Get patient gurantor address book
                //    result.PatientGurantor.AddressBook = addressBookServiceManager.GetAddressBookInfoByGuid(result.PatientGurantor.RecGuid);
                //}

                if (result.PatientEmergencyContactList != null && result.PatientEmergencyContactList.Count > 0)
                {
                    // Get energency contact phone no

                    //for (int indexEC = 0; indexEC < result.PatientEmergencyContactList.Count; indexEC++)
                    //{
                    //    PatientEmergencyContactDTO objEC = result.PatientEmergencyContactList[indexEC];
                    //    objEC.EmergencyPhone = addressBookServiceManager.GetPartyPrimaryPhoneInfoByGuid(result.PatientDTO.RecGuid);
                    //}
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Muhammad ISmail
        /// Date 28 March 2011
        /// Method Will be used to save/update patient Info.
        /// </summary>
        /// <param name="patientDTO"></param>
        /// <returns></returns>
        public int ManagePatient(PatientDTO patientDTO)
        {
            int result = 0;
            bool IsUpdate = false;
            try
            {
                if (patientDTO.PatientId > 0)
                {
                    IsUpdate = true;
                }
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatient patientDTO :: " + patientDTO);
                QuivusLogger.Info("Patient Dateofbirth:: "+patientDTO.DateOfBirth);

                if (QPatientValidator.ValidatePatientDTO(patientDTO, ref sb, IsUpdate))
                {
                    if (patientDAO == null)
                    {
                        patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                    }

                    result = patientDAO.ManagePatient(patientDTO, IsUpdate);


                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}
