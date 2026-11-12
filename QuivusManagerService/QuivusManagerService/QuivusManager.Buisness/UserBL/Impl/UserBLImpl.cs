using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Utility;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using Quivus.Utility.Logger;
using System.ServiceModel;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickLinks;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.IntegratedServies.ReferenceFactory;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Search;
using Quivus.Utility.Fault;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User.Patient;
using Quivus.Utility.MailTemplates;
using QuivusManager.ServiceResponse.ServiceCustomResponse.AdderssBook;
using QuivusManager.Buisness.AddressBookBL;
using QuivusManager.Buisness.AddressBookBL.Impl;
using QuivusManager.Buisness.Utility;
using QuivusManager.Buisness.E_PrescriptionBL;
using QuivusManager.Buisness.E_PrescriptionBL.Impl;
using QuivusManager.IntegratedServies.EprescriptionService;
using QuivusManager.IntegratedServies.AddressBookService;
using System.Xml.Serialization;
using System.IO;
//using QuivusModel.User.ClinicUser;
//using QuivusModel.Common;
//using QuivusModel.Association;


namespace QuivusManager.Buisness.UserBL.Impl
{
    public class UserBLImpl : IUserBL
    {

        UserManagerServiceClient UserServiceClient = null;
        AddressBookServiceClient AddressBookClient = null;
        IUserBL UserBl = null;
        IEPrescriptionBL EPrescriptionBL = null;
        IAddressBookBL AddressBL = null;
        /// <summary>
        /// Author M.Ismail
        /// Created Date Jan 11 2010
        /// This Method is Used TO Authenicate the User 
        /// In Case Of Successfull verfication system return User DTO Object
        /// Update by Mansoor
        /// Date 13 Jan 2010
        /// Response type changed to custom to catter the unavaliblity of Exception handling on
        /// Clinet side
        /// Update by Mansoor
        /// Date 14 Jan 2010
        /// Incase of unsucessfull scenario system sends the error message and code
        /// Clinet side
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserPassword"></param>
        /// <returns></returns>
        /// 
        public LoginResponse AuthenticateUser(string UserId, string UserPassword)
        {
            LoginResponse response = null;
            LoginDTO loginDTO = null;

            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching AuthenticateUser Call TO User-Service");

                loginDTO = UserServiceClient.AuthenticateUser(UserId, UserPassword);

                QuivusLogger.Info("Response Recieved From loginDTO::" + loginDTO);

                if (response == null)
                {
                    response = new LoginResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (loginDTO != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = loginDTO;
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
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load User Quick link List
        /// W.R.T location where he/she gets logged in
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ClinicBranchId"></param>
        public QuickLinkResponse LoadUserDefaultQuickLinksbyLocation(int UserId, int ClinicBranchId)
        {
            QuickLinkResponse response = null;
            List<QuickLinkDTO> quickLinkDTO = null;

            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadUserDefaultQuickLinksbyLocation Call TO User-Service");

                quickLinkDTO = UserServiceClient.LoadUserDefaultQuickLinksbyLocation(UserId, ClinicBranchId);

                QuivusLogger.Info("Response Recieved From quickLinkDTO::" + quickLinkDTO);

                if (response == null)
                {
                    response = new QuickLinkResponse();
                }
                if (quickLinkDTO != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = quickLinkDTO;
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
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load User Default Search Engine List
        /// W.R.T location where he/she gets logged in
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ClinicBranchId"></param>
        public SearchEngineResponse LoadUserSearchEnginebyLocation(int UserId, int ClinicBranchId)
        {
            List<SearchEngineDTO> SearchengineList = null;
            SearchEngineResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadUserSearchEnginebyLocation Call TO User-Service");

                SearchengineList = UserServiceClient.LoadUserSearchEnginebyLocation(UserId, ClinicBranchId);

                QuivusLogger.Info("Response Recieved From SearchengineList::" + SearchengineList);

                if (response == null)
                {
                    response = new SearchEngineResponse();
                }
                if (SearchengineList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = SearchengineList;
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
        /// Created Date 17 Jan 2011
        /// This Method Is Used To set specified Search engine As default
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="UserSearchEngineID"></param>
        public SearchEngineResponse SetDefaultSearchEngine(int ProviderUserID, int ClinicBranchID, int UserSearchEngineID)
        {
            int Result = 0;
            SearchEngineResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching SetDefaultSearchEngine Call TO User-Service");

                Result = UserServiceClient.SetDefaultSearchEngine(ProviderUserID, ClinicBranchID, UserSearchEngineID);

                QuivusLogger.Info("Response Recieved Result::" + Result);

                if (response == null)
                {
                    response = new SearchEngineResponse();
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
        /// Author Muhammad Ismail
        /// Created Jan 17 2011
        /// Method Used to MAnage the Search engine for user against Location
        /// Changed 07 Feb 2011
        /// Changed by Mansoor
        /// Params removed [int UserSearchEngineID, bool IsDefault] //M
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="SearchEngineTypeIDs"></param>
        /// <param name="UserSearchEngineID"></param>
        /// <param name="IsDefault"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public SearchEngineResponse ManageUserSearchEngine(int ProviderUserID, int ClinicBranchID, List<int> SearchEngineTypeIDs, int CreatedBy)
        {
            int Result = 0;
            SearchEngineResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching ManageUserSearchEngine Call TO User-Service");

                Result = UserServiceClient.ManageUserSearchEngine(ProviderUserID, ClinicBranchID, SearchEngineTypeIDs, CreatedBy);

                QuivusLogger.Info("Response Recieved Result::" + Result);

                if (response == null)
                {
                    response = new SearchEngineResponse();
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
        /// Author Muhammad ismail 
        /// Date 09 Feb 2011
        /// Method Used TO clear Search engines Added by User
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        public SearchEngineResponse ClearUserAddedSearchEngines(int ProviderUserID, int ClinicBranchID, int ModifiedBy)
        {
            int Result = 0;
            SearchEngineResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }
                QuivusLogger.Info("Dispatching ClearUserAddedSearchEngines Call TO User-Service");

                Result = UserServiceClient.ClearUserAddedSearchEngines(ProviderUserID, ClinicBranchID, ModifiedBy);

                QuivusLogger.Info("Response Recieved Result::" + Result);

                if (response == null)
                {
                    response = new SearchEngineResponse();
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
        /// Author Muhammad Ismail
        /// Created Date 17 jan 2011
        /// Method Used To Load Clinic Users
        /// </summary>
        /// <param name="ClinicBranchId"></param>
        /// <returns></returns>
        public ContactSearchResponse LoadUsersByClinicBranchID(int ClinicBranchID)
        {
            ContactSearchResponse response = null;
            List<ContactSearchDTO> ContactList = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }
                QuivusLogger.Info("Dispatching LoadUsersByClinicBranchID Call TO User-Service");
                ContactList = UserServiceClient.LoadUsersByClinicBranchID(ClinicBranchID);

                QuivusLogger.Info("Response Recieved ContactList::" + ContactList);

                if (response == null)
                {
                    response = new ContactSearchResponse();
                }
                if (ContactList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = ContactList;
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
        /// Created Date 18 jan 2011
        /// Method Used To Register User/ Update User Information
        /// </summary>
        /// <param name="registrationDTO"></param>
        /// <returns></returns>
        public UserRegistrationResponse RegisterNewUser(RegistrationDTO registrationDTO)
        {
            UserDTO userDTO = null;
            UserRegistrationResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching RegisterNewUser Call TO User-Service");

                userDTO = UserServiceClient.RegisterNewUser(registrationDTO);

                

                QuivusLogger.Info("Response Recieved userDTO::" + userDTO);

                if (response == null)
                {
                    response = new UserRegistrationResponse();
                }
                if (userDTO != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = userDTO;
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
        /// Author Muhaammad Ismail
        /// Created Date 24 Jan 2011
        /// This Method will  be used as an abstarct interface to save user that might belong 
        /// to any category i.e either patient or clinical User
        /// </summary>
        /// <param name="baseUserDTO"></param>
        /// <returns></returns>
        public BaseUserRegistrationResponse RegisterBaseUser(BaseUserDTO baseUserDTO)
        {
            int Result = 0;
            BaseUserRegistrationResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching RegisterBaseUser Call TO User-Service");

                Result = UserServiceClient.RegisterBaseUser(baseUserDTO);

                QuivusLogger.Info("Response Recieved Result::" + Result);
                

                if (response == null)
                {
                    response = new BaseUserRegistrationResponse();
                }
                if (Result > 0)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.UserId = Result;

                    if (UserBl == null)
                        UserBl = new UserBLImpl();

                    // Commente dBy Ismail moved to save patient prfile & languages
                    //   SendPatientEmail(baseUserDTO); // RegisterBaseUser is abstarct interface to register ny kind of user, right now only patient gets register from system so send email later on provide check here
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
        /// Date 23 May 2011
        /// This Method Will Used to send email to Patient 
        /// </summary>
        /// <param name="baseUserDTO"></param>
        private static void SendPatientEmail(BaseUserDTO baseUserDTO)
        {
            try
            {
                QuivusLogger.Info("Sending Email To User baseUserDTO::" + baseUserDTO);
                TemplateHandler handler = TemplateHandler.Instance;
                MailTemplate template = handler[ProjectConstant.PATIENT_REGISTRATION_EMAIL_TEMPLATE];
                template.Body = String.Format(template.Body,
                                              baseUserDTO.FirstName,
                                              baseUserDTO.QuivusUserID,
                                              baseUserDTO.UserEmail,
                                              baseUserDTO.TextPassword);

                handler.Send(template, baseUserDTO.UserEmail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Info("Error Sending Email To User exp::" + exp.ToString());
            }
        }


        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the user email id by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>    
        public EmailResponse GetUserEmailByUserId(int userId)
        {
            string emailId = null;
            EmailResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching GetUserEmailByUserId Call TO User-Service");

                emailId = UserServiceClient.GetUserEmailByUserId(userId);

                QuivusLogger.Info("Response Recieved emailId::" + emailId);

                if (response == null)
                {
                    response = new EmailResponse();
                }
                response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;

                if (!string.IsNullOrEmpty(emailId))
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.UserEmail = emailId;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS; // On Front End Request 
                    response.ResponseWarning = ProjectConstant.EMAIL_NOT_FOUND;
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
        /// Date 22 Feb 2011
        /// get the RecGuid of respective table, its a internal call used to get guid before saving Address Book
        /// </summary>        
        /// <param name="roleProfileID"></param>
        /// <param name="addressPartyTypeID"></param>
        /// <returns></returns>       
        public Guid GetPartyGuidByProfileIdAndTypeId(int roleProfileID, int addressPartyTypeID)
        {
            Guid partyGuid = new Guid();
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching GetPartyGuidByProfileIdAndTypeId Call TO User-Service");

                partyGuid = UserServiceClient.GetPartyGuidByProfileIdAndTypeId(roleProfileID, addressPartyTypeID);

                QuivusLogger.Info("Response Recieved partyGuid::" + partyGuid);

            }
            catch
            {
                throw;
            }
            return partyGuid;
        }


        /// <summary>
        /// Author waqas baber
        /// Date: 25/11/2011
        /// This call will station id :)
        /// </summary>        
        /// <param name="roleProfileID"></param>
        /// <param name="addressPartyTypeID"></param>
        /// <returns></returns>
        public List<StationDTO> GetStationNamesByStationID(List<int> stationIDList)
        {
            List<StationDTO> response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching GetUserBasicInfoById Call TO User-Service");

                response = UserServiceClient.GetStationNamesByStationID(stationIDList);
                

                QuivusLogger.Info("Response Recieved response::" + response);

            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad ISmail
        /// Date 22 Feb 2011
        /// This call will load user basic infor like name email using thier Ids :)
        /// </summary>        
        /// <param name="roleProfileID"></param>
        /// <param name="addressPartyTypeID"></param>
        /// <returns></returns>
        public List<UserBasicInfoDTO> GetUserBasicInfoById(List<int> ProviderIdList)
        {
            List<UserBasicInfoDTO> response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching GetUserBasicInfoById Call TO User-Service");

                response = UserServiceClient.GetUserBasicInfoById(ProviderIdList);

                QuivusLogger.Info("Response Recieved response::" + response);

            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// tell if a user is enrolled as provider on a specific branch
        /// </summary> 
        /// <param name="userId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>      
        public bool IsProviderAtClinic(int userId, int clinicBranchId)
        {
            bool isProvider = false;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching IsProviderAtClinic Call TO User-Service");

                isProvider = UserServiceClient.IsProviderAtClinic(userId, clinicBranchId);

                QuivusLogger.Info("Response Recieved isProvider::" + isProvider);

            }
            catch
            {
                throw;
            }
            return isProvider;
        }

        // <summary>
        /// Author Muhammad Ismail
        /// Date 29 march 2011
        /// Method WIll be use to verify USer password
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ServiceResponse.ServiceCustomResponse.ServiceResponse VerifyUser(int UserId, string password)
        {
            ServiceResponse.ServiceCustomResponse.ServiceResponse response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching IsProviderAtClinic Call TO User-Service");

                UserVerificationDTO result = UserServiceClient.VerifyUser(UserId, password);
                QuivusLogger.Info("Response Recieved result::" + result);

                if (response == null)
                {
                    response = new ServiceResponse.ServiceCustomResponse.ServiceResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (result != null)
                {
                    if (result.IsValidUser)
                    {
                        response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    }
                    else
                    {
                        response.ResponseMessage = result.ErrorMessage;
                    }
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad ISmail
        /// Date 28 March 2011
        /// Method Will be used to save/update patient Info.
        /// </summary>
        /// <param name="patientDTO"></param>
        /// <returns></returns>
        public PatientResponse ManagePatient(PatientDTO patientDTO)
        {
            QuivusLogger.Info("Patient DOB in manage patient:: " + patientDTO.DateOfBirth);
            PatientResponse response = null;
            int Result = 0;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching ManagePatient Call TO User-Service");

                Result = UserServiceClient.ManagePatient(patientDTO);

                QuivusLogger.Info("Response Recieved Result::" + Result);

                QuivusLogger.Info("Patient Dateofbirth:: " + patientDTO.DateOfBirth);

                if (response == null)
                {
                    response = new PatientResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (Result > 0)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad ISmail
        /// Date 28 March 2011
        /// Method Used To load patient info by Id
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public PatientResponse LoadPatientByPatientId(int PatientId)
        {
            PatientResponse response = null;
            PatientDTO responseData = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientByPatientId");

                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientByPatientId Call TO VisitService");

                responseData = UserServiceClient.LoadPatientByPatientId(PatientId);

                //QuivusLogger.Info("Response Recieved  HistoryList::" + followUp);

                if (response == null)
                {
                    response = new PatientResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (responseData != null)
                {
                    response.ResponseData = responseData;
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
        /// Date May 2 2011
        /// Method Will BE used to load Base User Info By User Id :)
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public BaseUserResponse LoadBaseUserByUserId(int UserId)
        {
            BaseUserResponse response = null;
            BaseUserDTO responseData = null;
            try
            {
                QuivusLogger.Info("Calling LoadBaseUserByUserId");

                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadBaseUserByUserId Call TO User service");

                responseData = UserServiceClient.LoadBaseUserByUserId(UserId);

                //QuivusLogger.Info("Response Recieved  HistoryList::" + followUp);

                if (response == null)
                {
                    response = new BaseUserResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (responseData != null)
                {
                    response.ResponseData = responseData;
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


        public ExternalPatientDTO LoadExternalPatients(int FromRecord, int ToRecord)
        {
            ExternalPatientDTO response = null;
            try
            {
                QuivusLogger.Info("Calling LoadExternalPatients");

                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                if (AddressBL == null)
                {
                    AddressBL = new AddressBookBLImpl();
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadExternalPatients Call TO User service");

                response = UserServiceClient.LoadExternalPatients(FromRecord, ToRecord);

                //QuivusLogger.Info("Response Recieved  HistoryList::" + followUp);

                if (response != null && response.PatientList != null)
                {
                    List<Guid> recList = new List<Guid>();
                    foreach (ExternalPatientInfoDTO obj in response.PatientList)
                    {
                        recList.Add(obj.RecGuid);

                        //AddressBookAddressResponse AddressResponse = AddressBL.LoadAddressBookByPartyRegGuid(obj.RecGuid);

                        //QuivusLogger.Info("Response Recieved AddressResponse::" + AddressResponse);

                        //if (AddressResponse != null && AddressResponse.ResponseData != null)
                        //{
                        //    QuivusLogger.Info("Cloning Address Book ");

                        //    QuivusManager.IntegratedServies.UserManagerService.AddressBookDTO ManagerAB = new IntegratedServies.UserManagerService.AddressBookDTO();

                        //    CloneAddressBook.CloneMainABTOManagerAdressBook(AddressResponse.ResponseData, ManagerAB);
                        //    obj.AddressBookInfo = ManagerAB;
                        //}
                    }

                    List<PatientAddressBookInfoDTO> AddressList = AddressBL.LoadPatientAddressBookByRegGuidAndType(recList);

                    if (AddressList != null)
                    {
                        foreach (ExternalPatientInfoDTO obj in response.PatientList)
                        {
                            var Query =
                                        from d in AddressList
                                        where d.RecGuid.Equals(obj.RecGuid)
                                        select d;

                            if (Query != null && Query.ToArray().Length > 0)
                            {
                                PatientAddressBookInfoDTO resultObj = Query.First();

                                obj.Address = resultObj.Address;
                                obj.City = resultObj.City;
                                obj.State = resultObj.State;
                                obj.Zip = resultObj.Zip;
                            }
                        }
                    }
                } //External IFFF

            }
            catch
            {
                throw;
            }
            return response;
        }


        public bool SyncExternalPatients(int FromRecord, int ToRecord)
        {
            bool response = false;
            try
            {
                QuivusLogger.Info("Calling LoadExternalPatients");

                ExternalPatientDTO externalPatient = LoadExternalPatients(FromRecord, ToRecord);

                if (externalPatient != null && externalPatient.PatientList != null)
                {

                    if (EPrescriptionBL == null)
                    {
                        EPrescriptionBL = new EPrescriptionBLImpl();
                    }

                    List<PatientInfoDTO> returnList = EPrescriptionBL.SynchronizePaitents(externalPatient.PatientList);

                    if (UserServiceClient == null)
                    {
                        UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                    }

                    if (returnList != null)
                    {
                        List<PatientRcopiaDTO> rcopiaList = new List<PatientRcopiaDTO>();
                        foreach (PatientInfoDTO src in returnList)
                        {
                            PatientRcopiaDTO dest = new PatientRcopiaDTO();
                            if (!string.IsNullOrEmpty(src.ExternalID))
                            {
                                try
                                {
                                    dest.PatientId = Int32.Parse(src.ExternalID);
                                }
                                catch
                                {

                                }
                            }
                            dest.RecGuid = src.RocpiaID;
                            rcopiaList.Add(dest);
                        }
                        response = UserServiceClient.UpdatePatientRCopiaId(rcopiaList);
                    }
                }
                /// Now Use this list to call update function
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 23 aug 2011
        /// verify email existance 
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public bool VerifyUserEmail(string email)
        {
            bool response = false;
            
            try
            {
                QuivusLogger.Info("Calling VerifyUserEmail");

                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Calling service :: UserService");

                QuivusLogger.Info("Dispatching VerifyUserEmail Call TO User service");

                response = UserServiceClient.VerifyUserEmail(email);

                //QuivusLogger.Info("Response Recieved  HistoryList::" + followUp);

                //if (response == null)
                //{
                //    response = new BaseUserResponse();
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}

                //if (responseData != null)
                //{
                //    response.ResponseData = responseData;
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}
                //else
                //{
                //    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                //}
            }
            catch
            {
                throw;
            }
            return response;
        }


        /// <summary>
        /// Author Muhammad ISmail
        /// Date 28 March 2011
        /// Method Used To load patient info by Id
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public int GetClinicBranchIdByClinicBranchCode(string branchCode)
        {
            int ClinicBranchId = 0;
            try
            {
                QuivusLogger.Info("Calling GetClinicBranchIdByClinicBranchCode");

                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Calling service :: UserManagerServiceClient");

                QuivusLogger.Info("Dispatching GetClinicBranchIdByClinicBranchCode Call TO UserManagerServiceClient");

                ClinicBranchId = UserServiceClient.GetClinicBranchIdByClinicBranchCode(branchCode);

                //QuivusLogger.Info("Response Recieved  HistoryList::" + followUp);

                //if (response == null)
                //{
                //    response = new PatientResponse();
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}

                //if (responseData != null)
                //{
                //    response.ResponseData = responseData;
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}
                //else
                //{
                //    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                //}
            }
            catch
            {
                throw;
            }
            return ClinicBranchId;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 06 Oct 2011       
        /// </summary>
        /// <param name="clinicUser"></param>
        /// <param name="userType"></param>
        /// <returns></returns>        
        public int ManageClinicUserProfile(ClinicUserDTO clinicUser, int userTypeId, int clinicBranchId)
        {
            int updatedID = 0;
            try
            {
                QuivusLogger.Info("Calling ManageClinicUserProfile");

                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Calling service :: UserManagerServiceClient");

                QuivusLogger.Info("Dispatching ManageClinicUserProfile Call TO UserManagerServiceClient");

                updatedID = UserServiceClient.ManageClinicUserProfile(clinicUser, userTypeId, clinicBranchId);

                //QuivusLogger.Info("Response Recieved  HistoryList::" + followUp);

                //if (response == null)
                //{
                //    response = new PatientResponse();
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}

                //if (responseData != null)
                //{
                //    response.ResponseData = responseData;
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}
                //else
                //{
                //    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                //}
            }
            catch
            {
                throw;
            }
            return updatedID;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 06 Oct 2011    
        /// </summary>        
        /// <param name="userId"></param>
        /// <param name="userTypeId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>        
        public ClinicUserDTO LoadClinicUserProfile(int userId, int profileId, int userTypeId, int clinicBranchId)
        {
            ClinicUserDTO cUserDTO = null;            
            try
            {
                QuivusLogger.Info("Calling LoadClinicUserProfile");

                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                if (AddressBL == null)
                {
                    AddressBL = new AddressBookBLImpl();
                }

                QuivusLogger.Info("Calling service :: UserManagerServiceClient");

                QuivusLogger.Info("Dispatching LoadClinicUserProfile Call TO UserManagerServiceClient");

                cUserDTO = UserServiceClient.LoadClinicUserProfile(userId, profileId, userTypeId, clinicBranchId);

                if (cUserDTO.DoctorDTO != null)
                {
                    QuivusLogger.Info("Calling LoadAddressBookByPartyRegGuid On Address BL Using GUID::" + cUserDTO.DoctorDTO.RecGuid);

                    AddressBookAddressResponse AddressResponse = AddressBL.LoadAddressBookByPartyRegGuid(cUserDTO.DoctorDTO.RecGuid);

                    QuivusLogger.Info("Response Recieved AddressResponse::" + AddressResponse);

                    if (AddressResponse != null && AddressResponse.ResponseData != null)
                    {
                        QuivusLogger.Info("Cloning Address Book ");

                        QuivusManager.IntegratedServies.UserManagerService.AddressBookDTO ManagerAB = new IntegratedServies.UserManagerService.AddressBookDTO();

                        CloneAddressBook.CloneMainABTOManagerAdressBook(AddressResponse.ResponseData, ManagerAB);
                        cUserDTO.UserAddressBookDTO = ManagerAB;
                    }
                }

                //QuivusLogger.Info("Response Recieved  HistoryList::" + followUp);

                //if (response == null)
                //{
                //    response = new PatientResponse();
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}

                //if (responseData != null)
                //{
                //    response.ResponseData = responseData;
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}
                //else
                //{
                //    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                //}
            }
            catch
            {
                throw;
            }
            return cUserDTO;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 06 Oct 2011
        /// Method Used To load Speciality Types
        /// </summary>
        /// <returns></returns>     
        public List<SpecialityTypeDTO> LoadSpecialityTypes()
        {
            List<SpecialityTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadSpecialityTypes");

                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Calling service :: UserManagerServiceClient");

                QuivusLogger.Info("Dispatching LoadSpecialityTypes Call TO UserManagerServiceClient");

                list = UserServiceClient.LoadSpecialityTypes();

                //QuivusLogger.Info("Response Recieved  HistoryList::" + followUp);

                //if (response == null)
                //{
                //    response = new PatientResponse();
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}

                //if (responseData != null)
                //{
                //    response.ResponseData = responseData;
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}
                //else
                //{
                //    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                //}
            }
            catch
            {
                throw;
            }
            return list;
        }



        /// <summary>
        /// Author Mansoor
        /// Date 31 Oct 2011        
        /// </summary>        
        /// <returns></returns>
        public List<PatientProviderAssociationDTO> ListAllPatientProvidersWithLocations(int patientId)
        {
            List<PatientProviderAssociationDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling ListAllPatientProvidersWithLocations");

                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Calling service :: UserManagerServiceClient");

                QuivusLogger.Info("Dispatching ListAllPatientProvidersWithLocations Call TO UserManagerServiceClient");

                list = UserServiceClient.ListAllPatientProvidersWithLocations(patientId);

                //QuivusLogger.Info("Response Recieved  HistoryList::" + followUp);

                //if (response == null)
                //{
                //    response = new PatientResponse();
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}

                //if (responseData != null)
                //{
                //    response.ResponseData = responseData;
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}
                //else
                //{
                //    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                //}
            }
            catch
            {
                throw;
            }
            return list;
        }

        public int CheckProviderAuthorityForPatientInfoAccess(int providerUserId, int patientId, int clinicBranchId)
        {
            int flag = 0;
            try
            {
                QuivusLogger.Info("Calling CheckProviderAuthorityForPatientInfoAccess");

                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Calling service :: UserManagerServiceClient");

                QuivusLogger.Info("Dispatching CheckProviderAuthorityForPatientInfoAccess Call TO UserManagerServiceClient");

                flag = UserServiceClient.CheckProviderAuthorityForPatientInfoAccess(providerUserId, patientId, clinicBranchId);

                //QuivusLogger.Info("Response Recieved  HistoryList::" + followUp);

                //if (response == null)
                //{
                //    response = new PatientResponse();
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}

                //if (responseData != null)
                //{
                //    response.ResponseData = responseData;
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}
                //else
                //{
                //    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                //}
            }
            catch
            {
                throw;
            }
            return flag;
        }

        public int ManageProviderAuthorization(PatientProviderAssociationDTO patientProviderAssociationDTO)
        {
            int flag = 0;
            try
            {
                QuivusLogger.Info("Calling ManageProviderAuthorization");

                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Calling service :: UserManagerServiceClient");

                QuivusLogger.Info("Dispatching ManageProviderAuthorization Call TO UserManagerServiceClient");

                flag = UserServiceClient.ManageProviderAuthorization(patientProviderAssociationDTO);

                //QuivusLogger.Info("Response Recieved  HistoryList::" + followUp);

                //if (response == null)
                //{
                //    response = new PatientResponse();
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}

                //if (responseData != null)
                //{
                //    response.ResponseData = responseData;
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}
                //else
                //{
                //    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                //}
            }
            catch
            {
                throw;
            }
            return flag;
        }


        public List<ProviderSearchDTO> SearchProviderForAppointment(SearchProviderCriteriaDTO criteria)
        {
            List<ProviderSearchDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling SearchProviderForAppointment");

                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Calling service :: UserManagerServiceClient");

                QuivusLogger.Info("Dispatching SearchProviderForAppointment Call TO UserManagerServiceClient");

                list = UserServiceClient.SearchProviderForAppointment(criteria);

                //QuivusLogger.Info("Response Recieved  HistoryList::" + followUp);

                //if (response == null)
                //{
                //    response = new PatientResponse();
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}

                //if (responseData != null)
                //{
                //    response.ResponseData = responseData;
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}
                //else
                //{
                //    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                //}
            }
            catch
            {
                throw;
            }
            return list;
        }


        /// <summary>
        /// Author: Zahid Mehmood
        /// Created Date 13 jan 2012
        /// Method Used To Load Clinic Users for Provider Search in Communication
        /// </summary>
        /// <param name="ClinicBranchId"></param>
        /// <returns></returns>
        public List<ContactSearchDTO> LoadProviderByClinicBranchID(int ClinicBranchID)
        {
            
            List<ContactSearchDTO> ContactList = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }
                QuivusLogger.Info("Dispatching LoadUsersByClinicBranchID Call TO User-Service");
                ContactList = UserServiceClient.LoadUsersByClinicBranchID(ClinicBranchID);

                QuivusLogger.Info("Response Recieved ContactList::" + ContactList);

                return ContactList;
            }
            catch
            {
                throw;
            }
            return ContactList;
        }



        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 17 Jan 2012
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        public ClinicProviderSearchDTO LoadClinicUserBasicInfo(int userId, int clinicBranchId)
        {
            ClinicProviderSearchDTO response = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }
                QuivusLogger.Info("Dispatching LoadClinicUserBasicInfo Call TO User-Service");

                response=UserServiceClient.LoadClinicUserBasicInfo(userId, clinicBranchId);   
                if(response != null &&  !string.IsNullOrEmpty(response.RecGuid))
                {
                     try
                     {
                         QuivusManager.IntegratedServies.AddressBookService.AddressBookDTO objAddressBook = AddressBookClient.LoadAddressBookByPartyRegGuid(System.Guid.Parse(response.RecGuid));
                         if (AddressBookClient == null)
                         {
                             AddressBookClient = (AddressBookServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(AddressBookServiceClient));
                             if (objAddressBook != null && objAddressBook.AddressDTOlist.Count > 0)
                             {
                                 response.City = objAddressBook.AddressDTOlist[0].CityName;
                                 response.State = objAddressBook.AddressDTOlist[0].StateName;
                                 response.Zip = objAddressBook.AddressDTOlist[0].ZipCode;
                                 response.Address = objAddressBook.AddressDTOlist[0].AddressTypeName;
                                 response.Phone = objAddressBook.AddressDTOlist[0].AddressPhoneDTOlist[0].PhoneDTO.PhoneNumber;
                             }
                         }
                     }
                     catch (Exception ex)
                     {
                         QuivusLogger.Info("ERROR:AddressBookService:Dispatched LoadClinicUserBasicInfo Call TO AddressBook-Service: "+ex.Message);
                     }
                 
                 
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return response;
        }
    }
}
