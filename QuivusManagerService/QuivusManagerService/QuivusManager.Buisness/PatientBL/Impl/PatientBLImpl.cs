using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using QuivusManager.Buisness.AddressBookBL.Impl;
using QuivusManager.Buisness.AddressBookBL;
using QuivusManager.IntegratedServies.AddressBookService;
using QuivusManager.IntegratedServies.UserManagerService;
using Quivus.Utility;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User.Patient;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User;
using QuivusManager.IntegratedServies.ReferenceFactory;
using QuivusManager.Buisness.Utility;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Search;
using QuivusManager.ServiceResponse.ServiceCustomResponse.RecentPatient;
using QuivusManager.ServiceResponse.ServiceCustomResponse.AdderssBook;
using Quivus.Utility.Logger;
using QuivusManager.ServiceResponse.ServiceCustomResponse.PatientProfileCustomization;
using QuivusManager.Buisness.UserBL;
using QuivusManager.Buisness.UserBL.Impl;
using QuivusManager.IntegratedServies.EprescriptionService;
using QuivusManager.Buisness.E_PrescriptionBL;
using QuivusManager.Buisness.E_PrescriptionBL.Impl;
using Quivus.Utility.MailTemplates;
using QuivusManager.IntegratedServies.VisitService;


namespace QuivusManager.Buisness.PatientBL.Impl
{
    public class PatientBLImpl : IPatientBL
    {
        UserManagerServiceClient UserServiceClient = null;
        VisitServiceClient clientVisit = null;

        //AddressBookServiceClient AddressBookClient = null;

        IAddressBookBL AddressBL = null;

        IUserBL UserBl = null;
        IEPrescriptionBL EPrescriptionBl = null;
        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 24 Jan 2011
        /// This Method Used To Insert/Update/Delete The patient emergency contact Information :)
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="EmergencyContactList"></param>
        /// <returns></returns>
        public PatientEmergencyResponse ManagePatientEmergencyContact(int PatientId, int CreatedBy, List<PatientEmergencyContactDTO> EmergencyContactList)
        {
            PatientEmergencyResponse response = null;
            int Result = 0;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Verfiy RecGuid If Empty Generate New ");

                foreach (PatientEmergencyContactDTO obj in EmergencyContactList)
                {
                    if (obj.RecGuid == System.Guid.Empty)
                    {
                        obj.RecGuid = Guid.NewGuid();
                    }
                }

                QuivusLogger.Info("Dispatching ManagePatientEmergencyContact Call TO User-Service");

                Result = UserServiceClient.ManagePatientEmergencyContact(PatientId, CreatedBy, EmergencyContactList);

                QuivusLogger.Info("Response Recieved  Result::" + Result);

                if (response == null)
                {
                    response = new PatientEmergencyResponse();
                }
                if (Result > 0)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;

                    QuivusLogger.Info("Calling Internal SavePatientEmenrgencyAddressBook");

                    Result = SavePatientEmenrgencyAddressBook(EmergencyContactList, CreatedBy, Result);
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;

        }


        private int SavePatientEmenrgencyAddressBook(List<PatientEmergencyContactDTO> EmergencyContactList, int CreatedBy, int UpdateResult)
        {

            QuivusLogger.Info("SavePatientEmenrgencyAddressBook Called PARAM EmergencyContactList::" + EmergencyContactList + "CreatedBy::" + CreatedBy + "UpdateResult::" + UpdateResult);

            IntegratedServies.AddressBookService.AddressBookDTO addressBookDTO = null;
            List<QuivusManager.IntegratedServies.UserManagerService.PhoneDTO> Phonelist = null;
            try
            {
                if (AddressBL == null)
                {
                    AddressBL = new AddressBookBLImpl();
                }

                if (EmergencyContactList != null)
                {
                    QuivusLogger.Info("Iterate EmergencyContactList Save Each Phone DTO");
                    foreach (PatientEmergencyContactDTO obj in EmergencyContactList)
                    {
                        if (obj != null)
                        {
                            if (obj.EmergencyPhone != null)
                            {
                                Phonelist = new List<QuivusManager.IntegratedServies.UserManagerService.PhoneDTO>();
                                Phonelist.Add(obj.EmergencyPhone);
                            }

                            if (Phonelist != null)
                            {
                                addressBookDTO = new IntegratedServies.AddressBookService.AddressBookDTO();

                                QuivusLogger.Info("Cloning AddressBOOk");
                                if (addressBookDTO.PhoneDTOlist == null)
                                {
                                    addressBookDTO.PhoneDTOlist = new List<IntegratedServies.AddressBookService.PhoneDTO>();
                                }

                                CloneAddressBook.CloneManagerPhoneToABPhone(Phonelist, addressBookDTO.PhoneDTOlist);

                                addressBookDTO.PhoneDTOlist = addressBookDTO.PhoneDTOlist;
                                addressBookDTO.AddressDTOlist = null;
                                addressBookDTO.PartyID = obj.RecGuid;
                                addressBookDTO.AddressPartyTypeID = (int)ProjectConstant.ADDRESS_PARTY_TYPE.EmergencyContact;

                                QuivusLogger.Info("Calling  SaveUserAddressBook On Address BL");
                                UpdateResult = AddressBL.SaveUserAddressBook(CreatedBy, addressBookDTO);

                                Phonelist = null;
                                addressBookDTO = null;
                            }
                        }
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
        /// Save the patient profile and language w.r.t the userId provided
        /// </summary>
        /// <param name="patientDTO"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public PatientSaveResponse SavePatientBasicProfileAndLangauge(int userId, int managedByUserID, PatientDTO patientDTO, List<PatientLanguageDTO> PatientLanguageDTOList)
        {
            int Result = 0;
            String RCopiaId = String.Empty;
            PatientSaveResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }


                QuivusLogger.Info("Dispatching SavePatientBasicProfileAndLangauge Call TO User-Service");

                Result = UserServiceClient.SavePatientBasicProfileAndLangauge(userId, managedByUserID, patientDTO, PatientLanguageDTOList);

                if (UserBl == null)
                {
                    UserBl = new UserBLImpl();
                }
                // Make Call to e-prescription service to upload patient to Dr. First 
                // Use Result i.e PatientId To Update RCOPIAID in DB :)

                BaseUserResponse UserInfo = UserBl.LoadBaseUserByUserId(userId); // Load user Info :)

                String QuivusId = string.Empty;
                try
                {
                    QuivusId = LoadQuivusIdByPatientId(Result);
                }
                catch
                { //Exception pe jao
                }

                if (UserInfo != null && UserInfo.ResponseData != null)
                    SendPatientEmail(UserInfo.ResponseData.FirstName, QuivusId, UserInfo.ResponseData.UserEmail, UserInfo.ResponseData.TextPassword);

                //  PatientDTO LoadPatientInfoByPatientIdForCompleteProfile(int patientId)
                if (patientDTO.PatientId <= 0)
                {
                    if (UserInfo != null && UserInfo.ResponseData != null)
                    {
                        PatientInfoDTO patientInfo = new PatientInfoDTO();

                        # region Required Fields For Patient Upload On Dr First

                        patientInfo.FirstName = UserInfo.ResponseData.FirstName;
                        patientInfo.LastName = UserInfo.ResponseData.LastName;
                        patientInfo.DOB = patientDTO.DateOfBirth.ToString();
                        patientInfo.Address = patientDTO.Address;
                        patientInfo.City = patientDTO.City;
                        patientInfo.State = patientDTO.State;
                        patientInfo.Zip = patientDTO.ZipCode;
                        patientInfo.PhoneNumber = patientDTO.PatientPhoneNumber;

                        if (patientDTO.PatientId > 0)
                        { 
                        // This is edit case also set rcopia id
                            patientInfo.RocpiaID = patientDTO.RCOPIAID;
                        }
                        
                        // Edit RCOpia & external Id 
                       // patientInfo.ph
                        # endregion

                        # region Fields required for eligibility checks to succeed

                        if (UserInfo.ResponseData.Gender != null)
                        {
                            if (UserInfo.ResponseData.Gender.GenderName.ToLower().Equals("male"))
                            {
                                patientInfo.Sex = "M";
                            }
                            else
                            {
                                patientInfo.Sex = "F";
                            }
                        }

                        //   patientInfo.ExternalID = UserInfo.ResponseData.UserId.ToString(); // This Id will be used to refere record in Dr. First System & We can refere record both This ExternalId or RcopiadId In Dr. First system
                        patientInfo.ExternalID = Result.ToString();
                        //  patientInfo.Zip = UserInfo.ResponseData.zi
                        # endregion
                        if (EPrescriptionBl == null)
                        {
                            EPrescriptionBl = new EPrescriptionBLImpl();
                        }

                        RCopiaId = EPrescriptionBl.UploadPatient(patientInfo); //TODO Check
                        if (!string.IsNullOrEmpty(RCopiaId))
                        {
                            if (RCopiaId.ToLower().StartsWith("error"))
                            {
                                // Error Occured While Updating Patient E-Prescription Details
                                QuivusLogger.Info("Error Response Recieved From EPrescription-Service Result::" + Result);
                            }
                            else
                            {
                                QuivusLogger.Info("Updating Patient E-Prescription Details" + Result);
                                bool result = UserServiceClient.UpdatePatientEPrescriptionDetails(RCopiaId, Result);
                                // Update  Patient E-Prescription Details
                            }
                        }
                    }
                }// If Not Update Case 

                QuivusLogger.Info("Response Recieved From User-Service Result::" + Result);

                if (response == null)
                {
                    response = new PatientSaveResponse();
                }
                if (Result > 0)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.PatientId = Result;

                    if (clientVisit == null)
                    {
                        clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                    }

                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public string LoadQuivusIdByPatientId(int PatientId)
        {
            string QuivusId = null;

            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching GetUserEmailByUserId Call TO User-Service");

                QuivusId = UserServiceClient.LoadQuivusIdByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved QuivusId::" + QuivusId);


            }
            catch
            {
                throw;
            }
            return QuivusId;
        }

        public string LoadRcopiaIdByPatientId(int PatientId)
        {
            string RcopiaId = null;

            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadRcopiaIdByPatientId Call TO User-Service");

                RcopiaId = UserServiceClient.LoadRcopiaIdByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved RcopiaId::" + RcopiaId);


            }
            catch
            {
                throw;
            }
            return RcopiaId;
        }

        /// <summary>
        /// Author Ismail
        /// Date 23 May 2011
        /// This Method Will Used to send email to Patient 
        /// </summary>
        /// <param name="baseUserDTO"></param>
        private static void SendPatientEmail(String FirstName, String QuivusUserID, String UserEmail, String TextPassword)
        {
            try
            {
                QuivusLogger.Info("Sending Email To User FirstName::" + FirstName);
                TemplateHandler handler = TemplateHandler.Instance;
                MailTemplate template = handler[ProjectConstant.PATIENT_REGISTRATION_EMAIL_TEMPLATE];
                template.Body = String.Format(template.Body,
                                              FirstName,
                                              QuivusUserID,
                                              UserEmail,
                                              TextPassword);

                handler.Send(template, UserEmail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Info("Error Sending Email To User exp::" + exp.ToString());
            }
        }

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011        
        /// save the patient Gurantor Info
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientGurantorDTO"></param>
        /// <returns></returns>
        public QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse SavePatientGurantor(int patientID, int managedByUserID, PatientGuarantorDTO patientGurantorDTO)
        {
            int Result = 0;
            QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                if (AddressBL == null)
                {
                    AddressBL = new AddressBookBLImpl();
                }

                QuivusLogger.Info("SavePatientGurantor Verfiy RecGuid If Empty Generate New");

                if (patientGurantorDTO.RecGuid == System.Guid.Empty)
                {
                    patientGurantorDTO.RecGuid = System.Guid.NewGuid(); // create gurantor guid
                }
                if (patientGurantorDTO.AddressBook != null && patientGurantorDTO.AddressBook.PartyID == System.Guid.Empty)
                {
                    patientGurantorDTO.AddressBook.PartyID = patientGurantorDTO.RecGuid; // assign the gurantor created guid to addresss book party id
                }


                QuivusLogger.Info("Dispatching SavePatientGurantor Call TO User Service");

                Result = UserServiceClient.SavePatientGurantor(patientID, managedByUserID, patientGurantorDTO);

                QuivusLogger.Info("Response Recieved From User-Service Result::" + Result);

                if (response == null)
                {
                    response = new QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse();
                }
                if (Result > 0)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    //M response.PatientId = Result;

                    if (patientGurantorDTO.AddressBook != null)
                    {
                        QuivusLogger.Info("SavePatientGurantor Saving Gurantor AddressBook");

                        IntegratedServies.UserManagerService.AddressBookDTO ManagerAddressBook = patientGurantorDTO.AddressBook;
                        IntegratedServies.AddressBookService.AddressBookDTO ABAddressBook = new IntegratedServies.AddressBookService.AddressBookDTO();

                        QuivusLogger.Info("SavePatientGurantor Cloning Address Book");

                        CloneAddressBook.CloneManagerAdressBookTOMainAB(ManagerAddressBook, ABAddressBook);

                        QuivusLogger.Info("Dispatching SaveUserAddressBook Call TO AddressBook Service");

                        Result = AddressBL.SaveUserAddressBook(managedByUserID, ABAddressBook);

                        QuivusLogger.Info("Response Recieved From AddressBook-Service Result::" + Result);
                    }
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
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
        /// save the patient health Info like allergies, medical history, family history
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="managedByUserID"></param>
        /// <param name="patientHealthDTO"></param>
        /// <returns></returns>
        public QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse SavePatientHealthInfo(int patientID, int managedByUserID, PatientHealthDTO patientHealthDTO)
        {
            int Result = 0;
            QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching SavePatientHealthInfo Call TO User Service");

                Result = UserServiceClient.SavePatientHealthInfo(patientID, managedByUserID, patientHealthDTO);

                QuivusLogger.Info("Response Recieved From User-Service Result::" + Result);

                if (response == null)
                {
                    response = new QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse();
                }
                if (Result > 0)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
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
        /// save the patient Insurance Info
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientInsuranceDTOList"></param>
        /// <returns></returns>
        public ServiceResponse.ServiceCustomResponse.ServiceResponse SavePatientInsuranceInfo(int patientID, int managedByUserID, List<PatientInsuranceDTO> patientInsuranceDTOList)
        {
            int Result = 0;
            QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching SavePatientInsuranceInfo Call TO User Service");

                Result = UserServiceClient.SavePatientInsuranceInfo(patientID, managedByUserID, patientInsuranceDTOList);

                QuivusLogger.Info("Response Recieved From User-Service Result::" + Result);

                if (response == null)
                {
                    response = new QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse();
                }
                if (Result > 0)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
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
        /// Date 27 Jan 2011
        /// Get portion of patient search data, based on query string and demand page no
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchString"></param>
        /// <param name="demandPageNo"></param>
        /// <returns></returns>
        public PatientSearchResponse SearchPatientbyClinicIdAndSearchString(int clinicBranchId, int providerId, int userTypeId, string searchString, int demandPageNo)
        {
            PatientSearchResultDTO searchResult = null;
            PatientSearchResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching SearchPatientbyClinicIdAndSearchString Call TO User Service");

                searchResult = UserServiceClient.SearchPatientbyClinicIdAndSearchString(clinicBranchId, providerId, userTypeId, searchString, demandPageNo);

                QuivusLogger.Info("Response Recieved From User-Service searchResult::" + searchResult);

                if (response == null)
                {
                    response = new PatientSearchResponse();
                }
                if (searchResult != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.PatientSearchResultResponseData = searchResult;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
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
        /// Date 1 Feb 2011
        /// Save the patient in the most patient list for that user on this clinic
        /// </summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>   
        public bool SaveRecentPatient(int clinicBranchId, int providerUserId, int patientId)
        {
            bool response = false;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching SaveRecentPatient Call TO User Service");

                response = UserServiceClient.SaveRecentPatient(clinicBranchId, providerUserId, patientId);

                QuivusLogger.Info("Response Recieved From User-Service response::" + response);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 1 Feb 2011
        /// Load the list of most recent loaded patients for the provider for location
        /// </summary>
        /// <param name="providerUserId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>       
        public RecentPatientRespose LoadRecentPatientsByProviderIdAndBranchId(int providerUserId, int clinicBranchId)
        {
            List<RecentPatientDTO> RecentPatientList = null;
            RecentPatientRespose response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadRecentPatientsByProviderIdAndBranchId Call TO User Service");

                RecentPatientList = UserServiceClient.LoadRecentPatientsByProviderIdAndBranchId(providerUserId, clinicBranchId);

                QuivusLogger.Info("Response Recieved From User-Service RecentPatientList::" + RecentPatientList);

                if (response == null)
                {
                    response = new RecentPatientRespose();
                }
                if (RecentPatientList != null)
                {
                    QuivusLogger.Info("RecentPatientList Count ::" + RecentPatientList.Count);

                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.MostRecentPatientsResponseData = RecentPatientList;
                }
                else
                {
                    QuivusLogger.Info("Null List Recieved From LoadRecentPatientsByProviderIdAndBranchId Send Fail MSG");

                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// This Method Will be used to load Patient Profile Setting Providing 
        /// Location-Id & Provider i.e doctor or nurse Id.
        /// Settings are customized by combination of Provider&User Id 
        /// i.e Same user on different location may have different setting Vise Vers :)
        /// <param name="clinicBranchId"></param>
        /// <param name="providerId"></param>
        /// <returns></returns>
        /// 
        public PatientProfileSettingResponse LoadPatientProfileSettings(int clinicBranchId, int UserId, int PatientId)
        {
            PatientProfileCompleteSettingDTO CompleteSettingDTO = null;
            PatientProfileSettingResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadPatientProfileSettings Call TO User Service");

                CompleteSettingDTO = UserServiceClient.LoadPatientProfileSettings(clinicBranchId, UserId, PatientId);

                QuivusLogger.Info("Response Recieved From User-Service CompleteSettingDTO::" + CompleteSettingDTO);


                LoadFirstSectionAddressBookData(CompleteSettingDTO, PatientId);

                LoadSecondSectionAddressBookData(CompleteSettingDTO, PatientId);

                if (response == null)
                {
                    response = new PatientProfileSettingResponse();
                }

                if (CompleteSettingDTO.SectionThirdData != null && CompleteSettingDTO.SectionThirdData.CollectionType.Equals(ProjectConstant.ALLERGY_COLLECTION))
                {
                    if (clientVisit == null)
                    {
                        clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                    }
                    List<AllergyDTO> allergyLst = clientVisit.LoadPatientAllergyByPatientId(PatientId);

                    if (allergyLst != null && allergyLst.Count > 0)
                    {
                        if (CompleteSettingDTO.SectionThirdData.AllergyList == null)
                        {
                            CompleteSettingDTO.SectionThirdData.AllergyList = new List<PatientAllergyInfoDTO>();
                        }
                        foreach (AllergyDTO src in allergyLst)
                        {
                            PatientAllergyInfoDTO dest = new PatientAllergyInfoDTO();

                            dest.PatientAllergyId = src.PatientAllergyId;
                            dest.AllergyName = src.AllergyName;
                            dest.IsDeleted = src.IsDeleted;
                            dest.IsDirty = src.IsDirty;
                            CompleteSettingDTO.SectionThirdData.AllergyList.Add(dest);
                        }
                    }


                    #region NKDA fix for profile settings
                    if (CompleteSettingDTO.SectionThirdData.AllergyList.Count > 1)
                    {
                        CompleteSettingDTO.SectionThirdData.AllergyList.RemoveAll(allergen => allergen.AllergyName.Equals(ProjectConstant.VISIT_NKDA));
                        if (CompleteSettingDTO.SectionThirdData.AllergyList.Count > 1)
                            CompleteSettingDTO.SectionThirdData.AllergyList.RemoveAll(allergen => allergen.AllergyName.Equals(ProjectConstant.REGISTRATION_NKDA));

                        //var qResults = from allergy in lst
                        //               where allergy.AllergyName.Equals(ProjectConstant.REGISTRATION_NKDA) || allergy.AllergyName.Equals(ProjectConstant.VISIT_NKDA)
                        //               select allergy;

                    }

                    #endregion 
                    // Load patient Allergies i.e Visit Allergies  
                }

                if (CompleteSettingDTO != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.CompleteSettings = CompleteSettingDTO;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// This Method Will be used to load Patient Profile Setting Providing 
        /// Location-Id & Provider i.e doctor or nurse Id.
        /// Settings are customized by combination of Provider&User Id 
        /// i.e Same user on different location may have different setting Vise Vers :)
        /// <param name="clinicBranchId"></param>
        /// <param name="providerId"></param>
        /// <returns></returns>
        /// 
        public PatientProfileSettingResponse LoadPatientProfileSettingsBySection(int clinicBranchId, int UserId, int PatientId, int SectionId)
        {
            PatientProfileCompleteSettingDTO CompleteSettingDTO = null;
            PatientProfileSettingResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadPatientProfileSettingsBySection Call TO User Service");

                CompleteSettingDTO = UserServiceClient.LoadPatientProfileSettingsBySection(clinicBranchId, UserId, PatientId, SectionId);

                QuivusLogger.Info("Response Recieved From User-Service CompleteSettingDTO::" + CompleteSettingDTO);

                if (response == null)
                {
                    response = new PatientProfileSettingResponse();
                }
                if (CompleteSettingDTO != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.CompleteSettings = CompleteSettingDTO;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// This Method Will be used to Update Patient Profile Settings 
        /// </summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerId"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public PatientProfileSettingResponse ManagePatientProfileSettings(PatientProfileSettingDTO ProfileSettingObj)
        {
            PatientProfileSettingResponse response = null;
            int Result = 0;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                if (response == null)
                {
                    response = new PatientProfileSettingResponse();
                }

                QuivusLogger.Info("Dispatching ManagePatientProfileSettings Call TO User Service");

                Result = UserServiceClient.ManagePatientProfileSettings(ProfileSettingObj);

                QuivusLogger.Info("Response Recieved From User-Service Result::" + Result);

                if (Result > 0)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
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
        /// Date 1 Feb 2011
        /// Save the patient allergy info provided
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="patientAllergy"></param>
        /// <returns></returns>     
        public ServiceResponse.ServiceCustomResponse.ServiceResponse SavePatientAllergy(int patientId, int createdBy, PatientAllergyDTO patientAllergy)
        {
            ServiceResponse.ServiceCustomResponse.ServiceResponse response = null;
            bool Result = false;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching SavePatientAllergy Call TO User Service");

                Result = UserServiceClient.SavePatientAllergy(patientId, createdBy, patientAllergy);

                QuivusLogger.Info("Response Recieved From User-Service Result::" + Result);

                if (response == null)
                {
                    response = new ServiceResponse.ServiceCustomResponse.ServiceResponse();
                }

                if (Result)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
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
        /// Date 2 Feb 2011
        /// load the patient profile and also save the required info in the provider's most recent patient list.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <returns></returns>       
        /// 
        public PatientProfileResponse LoadPatientCompleteProfileForClinic(int patientId, int clinicBranchId, int providerUserId)
        {
            PatientProfileResponse response = null;
            PatientProfileDTO PatientProfile = null;

            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                if (AddressBL == null)
                {
                    AddressBL = new AddressBookBLImpl();
                }

                if (response == null)
                {
                    response = new PatientProfileResponse();
                }

                QuivusLogger.Info("Dispatching LoadPatientCompleteProfileForClinic  Call TO User Service");

                PatientProfile = UserServiceClient.LoadPatientCompleteProfileForClinic(patientId, clinicBranchId, providerUserId);

                QuivusLogger.Info("Response Recieved From User-Service PatientProfile::" + PatientProfile);

                if (PatientProfile.PatientDTO != null)
                {
                    QuivusLogger.Info("Calling LoadAddressBookByPartyRegGuid On Address BL Using GUID::" + PatientProfile.PatientDTO.RecGuid);

                    AddressBookAddressResponse AddressResponse = AddressBL.LoadAddressBookByPartyRegGuid(PatientProfile.PatientDTO.RecGuid);

                    QuivusLogger.Info("Response Recieved AddressResponse::" + AddressResponse);

                    if (AddressResponse != null && AddressResponse.ResponseData != null)
                    {
                        QuivusLogger.Info("Cloning Address Book ");

                        QuivusManager.IntegratedServies.UserManagerService.AddressBookDTO ManagerAB = new IntegratedServies.UserManagerService.AddressBookDTO();

                        CloneAddressBook.CloneMainABTOManagerAdressBook(AddressResponse.ResponseData, ManagerAB);
                        PatientProfile.PatientAddressBook = ManagerAB;
                    }
                }

                if (PatientProfile.PatientGurantor != null)
                {
                    QuivusLogger.Info("Calling LoadAddressBookByPartyRegGuid On Address BL Using GUID For Gurantor::" + PatientProfile.PatientGurantor.RecGuid);

                    if (PatientProfile.PatientGurantor.RecGuid != Guid.Empty)
                    {

                        AddressBookAddressResponse AddressResponse = AddressBL.LoadAddressBookByPartyRegGuid(PatientProfile.PatientGurantor.RecGuid);

                        QuivusLogger.Info("Response Recieved AddressResponse For Gurantor::" + AddressResponse);

                        if (AddressResponse != null && AddressResponse.ResponseData != null)
                        {
                            QuivusLogger.Info("Cloning Address Book For Gurantor");

                            QuivusManager.IntegratedServies.UserManagerService.AddressBookDTO ManagerAB = new IntegratedServies.UserManagerService.AddressBookDTO();
                            CloneAddressBook.CloneMainABTOManagerAdressBook(AddressResponse.ResponseData, ManagerAB);
                            PatientProfile.PatientGurantor.AddressBook = ManagerAB;
                        }
                    }
                }
                if (PatientProfile.PatientEmergencyContactList != null && PatientProfile.PatientEmergencyContactList.Count > 0)
                {
                    // Get energency contact phone no


                    for (int indexEC = 0; indexEC < PatientProfile.PatientEmergencyContactList.Count; indexEC++)
                    {
                        PatientEmergencyContactDTO objEC = PatientProfile.PatientEmergencyContactList[indexEC];

                        QuivusLogger.Info("Calling GetPartyPrimaryPhoneNoByRegGuid With Guid PatientProfile.PatientDTO.RecGuid On AddressBL" + objEC.RecGuid);

                        AddressBookPhoneResponse PhoneResponse = AddressBL.GetPartyPrimaryPhoneNoByRegGuid(objEC.RecGuid);

                        QuivusLogger.Info("Response Recieved for GetPartyPrimaryPhoneNoByRegGuid PhoneResponse::" + PhoneResponse);

                        if (PhoneResponse != null && PhoneResponse.ResponseData != null)
                        {
                            QuivusManager.IntegratedServies.UserManagerService.PhoneDTO ManagerPhoneAB = new IntegratedServies.UserManagerService.PhoneDTO();

                            QuivusLogger.Info("Cloning Address Book Phones ");

                            CloneAddressBook.CloneABPhoneManagerPhone(PhoneResponse.ResponseData, ManagerPhoneAB);

                            objEC.EmergencyPhone = ManagerPhoneAB;
                        }
                    }
                }

                if (PatientProfile.PatientInsuranceList != null)
                {
                    foreach (PatientInsuranceDTO obj in PatientProfile.PatientInsuranceList)
                    {
                        if (obj != null)
                        {
                            if (obj.InsuredPersonGender != null)
                            {
                                List<GenderTypeDTO> GenderTpyes = QApplicationDataCache.GENDER_INFO_CACHE_DATA;
                                if (GenderTpyes != null)
                                {

                                    var CacheQuery =
                            from d in GenderTpyes
                            where d.GenderId.Equals(obj.InsuredPersonGender.GenderId)
                            select d;

                                    if (CacheQuery != null && CacheQuery.ToArray().Length > 0)
                                    {
                                        GenderTypeDTO resultObj = CacheQuery.First();
                                        obj.InsuredPersonGender.GenderName = resultObj.GenderName;
                                    }

                                }

                            }
                        }
                    }
                }
                if (PatientProfile != null)
                {
                    response.PatientProfileResponseData = PatientProfile;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
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
        /// Date 3 Feb 2011
        /// load the patient info to show in the left window panel
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <returns></returns>    
        /// 
        public PatientProfileInfoResposne LoadPatientProfileInfoForProvider(int patientId, int clinicBranchId, int providerUserId)
        {
            PatientProfileInfoResposne response = null;
            PatientProfileTaggedDTO PatientProfile = null;
            QuivusManager.IntegratedServies.AddressBookService.PhoneDTO phoneDTOobj = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                if (AddressBL == null)
                {
                    AddressBL = new AddressBookBLImpl();
                }

                if (response == null)
                {
                    response = new PatientProfileInfoResposne();
                }

                QuivusLogger.Info("Dispatching LoadPatientCompleteProfileForClinic  Call TO User Service");

                PatientProfile = UserServiceClient.LoadPatientProfileInfoForProvider(patientId, clinicBranchId, providerUserId);

                QuivusLogger.Info("Response Recieved From User-Service PatientProfile::" + PatientProfile);

                if (PatientProfile != null && PatientProfile.PatientProfileBasicInfoData != null)
                {
                    QuivusLogger.Info("Method LoadPatientProfileInfoForProvider:: Calling GetPartyPrimaryPhoneNoByRegGuid With Guid::" + PatientProfile.PatientProfileBasicInfoData.RecGuid);
                    AddressBookPhoneResponse PhoneResponse = AddressBL.GetPartyPrimaryPhoneNoByRegGuid(PatientProfile.PatientProfileBasicInfoData.RecGuid);

                    QuivusLogger.Info("Response Recieved From GetPartyPrimaryPhoneNoByRegGuid PhoneResponse::" + PhoneResponse);

                    if (PhoneResponse != null && PhoneResponse.ResponseData != null)
                    {
                        QuivusManager.IntegratedServies.UserManagerService.PhoneDTO ManagerPhoneAB = new IntegratedServies.UserManagerService.PhoneDTO();

                        QuivusLogger.Info("Cloning Address Book");

                        CloneAddressBook.CloneABPhoneManagerPhone(PhoneResponse.ResponseData, ManagerPhoneAB);

                        PatientProfile.PrimaryPhoneNo = ManagerPhoneAB;
                    }
                }

                if (PatientProfile != null)
                {
                    response.TaggedProfile = PatientProfile;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
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
        /// Date 3 Feb 2011
        /// load the patient profile allergies for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>   
        public PatientProfileAllergyResponse LoadPatientAllergiesForProvider(int patientId)
        {
            PatientProfileAllergyResponse response = null;
            PatientProfileAllergyDTO PatientProfileAllergy = null;

            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadPatientAllergiesForProvider  Call TO User Service");

                PatientProfileAllergy = UserServiceClient.LoadPatientAllergiesForProvider(patientId);

                QuivusLogger.Info("Response Recieved From User-Service PatientProfileAllergy::" + PatientProfileAllergy);

                if (response == null)
                {
                    response = new PatientProfileAllergyResponse();
                }

                if (PatientProfileAllergy != null)
                {
                    response.PatientProfileAllergy = PatientProfileAllergy;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
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
        /// Date 4 Feb 2011
        /// load the patient profile diagnosis for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>    
        public PatientProfileDiagnosisResponse LoadPatientDiagnosisForProvider(int patientId)
        {
            PatientProfileDiagnosisResponse response = null;
            PatientProfileDiagnosisDTO PatientProfileDiagnosis = null;

            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadPatientDiagnosisForProvider  Call TO User Service");

                PatientProfileDiagnosis = UserServiceClient.LoadPatientDiagnosisForProvider(patientId);

                QuivusLogger.Info("Response Recieved From User-Service PatientProfileDiagnosis::" + PatientProfileDiagnosis);

                if (response == null)
                {
                    response = new PatientProfileDiagnosisResponse();
                }

                if (PatientProfileDiagnosis != null)
                {
                    response.ProfileDiagnosis = PatientProfileDiagnosis;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
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
        /// Date 4 Feb 2011
        /// load the patient profile immunization for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>  
        public PatientProfileImmunizationResponse LoadPatientImmunizationForProvider(int patientId)
        {
            PatientProfileImmunizationResponse response = null;
            PatientProfileImmuniztionDTO PatientProfileImmunization = null;

            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadPatientImmunizationForProvider  Call TO User Service");

                PatientProfileImmunization = UserServiceClient.LoadPatientImmunizationForProvider(patientId);

                QuivusLogger.Info("Response Recieved From User-Service PatientProfileImmunization::" + PatientProfileImmunization);

                if (response == null)
                {
                    response = new PatientProfileImmunizationResponse();
                }

                if (PatientProfileImmunization != null)
                {
                    response.ProfileImmunizationDTO = PatientProfileImmunization;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
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
        /// Date 4 Feb 2011
        /// load the patient profile medication for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>   
        public PatientProfileChronicMedicationResponse LoadPatientMedicationForProvider(int patientId)
        {
            PatientProfileChronicMedicationResponse response = null;
            PatientProfileChronicMedicationDTO PatientProfileChronicMedication = null;

            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadPatientMedicationForProvider  Call TO User Service");
                PatientProfileChronicMedication = UserServiceClient.LoadPatientMedicationForProvider(patientId);

                QuivusLogger.Info("Response Recieved From User-Service PatientProfileChronicMedication::" + PatientProfileChronicMedication);

                if (response == null)
                {
                    response = new PatientProfileChronicMedicationResponse();
                }

                if (PatientProfileChronicMedication != null)
                {
                    response.PatientProfileChronicMedication = PatientProfileChronicMedication;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 4 Feb 2011
        /// load the patient profile Insurance for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns> 
        public PatientProfileInsuranceResponse LoadPatientInsuranceForProvider(int patientId)
        {
            PatientProfileInsuranceResponse response = null;
            PatientProfileInsuranceDTO PatientProfileInsurance = null;

            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadPatientInsuranceForProvider Call TO User Service");

                PatientProfileInsurance = UserServiceClient.LoadPatientInsuranceForProvider(patientId);

                QuivusLogger.Info("Response Recieved From User-Service PatientProfileInsurance::" + PatientProfileInsurance);

                if (response == null)
                {
                    response = new PatientProfileInsuranceResponse();
                }

                if (PatientProfileInsurance != null)
                {
                    response.ProfileInsuranceDTO = PatientProfileInsurance;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        //Added by Mansoor
        //Date 28 Feb 2011
        //Load the settings related to Address Book

        private void LoadFirstSectionAddressBookData(PatientProfileCompleteSettingDTO completeSettingDTO, int patientId)
        {
            // Address

            if (completeSettingDTO.SectionFirstData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.Address
                || completeSettingDTO.SectionFirstData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.State
                || completeSettingDTO.SectionFirstData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.City
                || completeSettingDTO.SectionFirstData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.Zip
                )
            {
                if (AddressBL == null)
                {
                    AddressBL = new AddressBookBLImpl();
                }

                Guid partyGuid = new Guid();
                partyGuid = UserServiceClient.GetPartyGuidByProfileIdAndTypeId(patientId, (int)ProjectConstant.ADDRESS_PARTY_TYPE.Patient);

                completeSettingDTO.SectionFirstData.FieldData = AddressBL.LoadUserProfileSettingsAddressByRegGuidAndType(
                                partyGuid, completeSettingDTO.SectionFirstData.UserProfileCategoryID);
            }

            // Phone

            if (completeSettingDTO.SectionFirstData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.Phone
                || completeSettingDTO.SectionFirstData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.MotherPhoneExt
                || completeSettingDTO.SectionFirstData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.FatherPhoneExt
                || completeSettingDTO.SectionFirstData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.EmergencyContactPhoneExt
                || completeSettingDTO.SectionFirstData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.GurantorPhoneExt
                )
            {
                if (AddressBL == null)
                {
                    AddressBL = new AddressBookBLImpl();
                }

                Guid partyGuid = new Guid();
                int ownerTypeId = 0;
                int partyTypeId = 0;

                partyTypeId = (int)ProjectConstant.ADDRESS_PARTY_TYPE.Patient;

                if (completeSettingDTO.SectionFirstData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.MotherPhoneExt)
                {
                    ownerTypeId = (int)ProjectConstant.ADDRESS_OWNER_TYPE.Mother;

                    partyGuid = UserServiceClient.GetPartyGuidByProfileIdAndTypeId(patientId, partyTypeId);
                }
                else if (completeSettingDTO.SectionFirstData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.FatherPhoneExt)
                {
                    ownerTypeId = (int)ProjectConstant.ADDRESS_OWNER_TYPE.Father;

                    partyGuid = UserServiceClient.GetPartyGuidByProfileIdAndTypeId(patientId, partyTypeId);
                }
                else if (completeSettingDTO.SectionFirstData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.Phone)
                {
                    ownerTypeId = (int)ProjectConstant.ADDRESS_OWNER_TYPE.Self;

                    partyGuid = UserServiceClient.GetPartyGuidByProfileIdAndTypeId(patientId, partyTypeId);
                }
                else
                {
                    ownerTypeId = (int)ProjectConstant.ADDRESS_OWNER_TYPE.Self;

                    if (completeSettingDTO.SectionFirstData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.EmergencyContactPhoneExt)
                    {
                        partyTypeId = (int)ProjectConstant.ADDRESS_PARTY_TYPE.EmergencyContact;
                    }
                    else if (completeSettingDTO.SectionFirstData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.GurantorPhoneExt)
                    {
                        partyTypeId = (int)ProjectConstant.ADDRESS_PARTY_TYPE.PatientGuarantor;
                    }
                    partyGuid = UserServiceClient.GetPartyGuidByPatientIdAndPartyTypeId(patientId, partyTypeId);
                }
                if (partyGuid != Guid.Empty)
                {
                    completeSettingDTO.SectionFirstData.FieldData = AddressBL.LoadPartyProfileSettingsPhoneByRegGuidAndType(partyGuid, ownerTypeId);
                }
            }
        }

        //Added by Mansoor
        //Date 28 Feb 2011
        //Load the settings related to Address Book

        private void LoadSecondSectionAddressBookData(PatientProfileCompleteSettingDTO completeSettingDTO, int patientId)
        {
            // Address

            if (completeSettingDTO.SectionSecondData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.Address
                || completeSettingDTO.SectionSecondData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.State
                || completeSettingDTO.SectionSecondData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.City
                || completeSettingDTO.SectionSecondData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.Zip
                )
            {
                if (AddressBL == null)
                {
                    AddressBL = new AddressBookBLImpl();
                }

                Guid partyGuid = new Guid();
                partyGuid = UserServiceClient.GetPartyGuidByProfileIdAndTypeId(patientId, (int)ProjectConstant.ADDRESS_PARTY_TYPE.Patient);

                completeSettingDTO.SectionSecondData.FieldData = AddressBL.LoadUserProfileSettingsAddressByRegGuidAndType(
                                partyGuid, completeSettingDTO.SectionSecondData.UserProfileCategoryID);
            }

            // Phone

            if (completeSettingDTO.SectionSecondData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.Phone
                || completeSettingDTO.SectionSecondData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.MotherPhoneExt
                || completeSettingDTO.SectionSecondData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.FatherPhoneExt
                || completeSettingDTO.SectionSecondData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.EmergencyContactPhoneExt
                || completeSettingDTO.SectionSecondData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.GurantorPhoneExt
                )
            {
                if (AddressBL == null)
                {
                    AddressBL = new AddressBookBLImpl();
                }

                Guid partyGuid = new Guid();
                int ownerTypeId = 0;
                int partyTypeId = 0;

                partyTypeId = (int)ProjectConstant.ADDRESS_PARTY_TYPE.Patient;

                if (completeSettingDTO.SectionSecondData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.MotherPhoneExt)
                {
                    ownerTypeId = (int)ProjectConstant.ADDRESS_OWNER_TYPE.Mother;

                    partyGuid = UserServiceClient.GetPartyGuidByProfileIdAndTypeId(patientId, partyTypeId);
                }
                else if (completeSettingDTO.SectionSecondData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.FatherPhoneExt)
                {
                    ownerTypeId = (int)ProjectConstant.ADDRESS_OWNER_TYPE.Father;

                    partyGuid = UserServiceClient.GetPartyGuidByProfileIdAndTypeId(patientId, partyTypeId);
                }
                else if (completeSettingDTO.SectionSecondData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.Phone)
                {
                    ownerTypeId = (int)ProjectConstant.ADDRESS_OWNER_TYPE.Self;

                    partyGuid = UserServiceClient.GetPartyGuidByProfileIdAndTypeId(patientId, partyTypeId);
                }
                else
                {
                    ownerTypeId = (int)ProjectConstant.ADDRESS_OWNER_TYPE.Self;

                    if (completeSettingDTO.SectionSecondData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.EmergencyContactPhoneExt)
                    {
                        partyTypeId = (int)ProjectConstant.ADDRESS_PARTY_TYPE.EmergencyContact;
                    }
                    else if (completeSettingDTO.SectionSecondData.UserProfileCategoryID == (int)ProjectConstant.USER_PROFILE_CATEGORY_ID.GurantorPhoneExt)
                    {
                        partyTypeId = (int)ProjectConstant.ADDRESS_PARTY_TYPE.PatientGuarantor;
                    }
                    partyGuid = UserServiceClient.GetPartyGuidByPatientIdAndPartyTypeId(patientId, partyTypeId);
                }
                if (partyGuid != Guid.Empty)
                {
                    completeSettingDTO.SectionSecondData.FieldData = AddressBL.LoadPartyProfileSettingsPhoneByRegGuidAndType(partyGuid, ownerTypeId);
                }
            }
        }
    }
}
