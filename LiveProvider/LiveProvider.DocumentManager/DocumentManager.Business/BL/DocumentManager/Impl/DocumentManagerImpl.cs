using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Model.TypeDTOs.DocumentManager;
using LiveProvider.Utility.Logger;
using DocumentManager.DataAccess.DocumentManager;
using LiveProvider.Utility.Factory;
using DocumentManager.Business.Validators;
using LiveProvider.Utility.CustomExceptions;
using LiveProvider.Utility.Constant.Errors;
using System.IO;
using LiveProvider.Utility.FileOperator;
using LiveProvider.Model.Response;
using DocumentManager.Business.UserManagerService;
using DocumentManager.Business.VisitService;
using DocumentManager.Business.PathCreater;
using LiveProvider.Utility.Constant.ProjectConstant;
using LiveProvider.Model.TypeDTOs;
using LiveProvider.Utility.MailTemplates;

namespace DocumentManager.Business.BL.DocumentManager.Impl
{
    /// <summary>
    /// Author Muhammad Ismail
    /// Date 04 July 2011
    /// THis Class will be serverd as buisness manager for document related operation :)
    /// </summary>
    public class DocumentManagerImpl : IDocumentManager
    {
        DocumentManagerDAO documentDAO = null;

        /// <summary>
        /// Author Muhammad Ismail
        /// Date 04 July 2011
        /// This Method Will be used to load the document types 
        /// </summary>
        /// <returns></returns>
        public List<DocumentTypeDTO> LoadDocumentTypes()
        {
            List<DocumentTypeDTO> documentList = null;
            try
            {
                LiveProviderLogger.Info("Loading LoadDocumentTypes");

                if (documentDAO == null)
                {
                    documentDAO = (DocumentManagerDAO)DAOFactory.GetDataAccessObject(typeof(DocumentManagerDAO));
                }

                documentList = documentDAO.LoadDocumentTypes();
            }
            catch 
            {
                throw;
            }
            return documentList;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Date 04 July 2011
        /// Method Will be used to load document Section Types
        /// </summary>
        /// <returns></returns>
        public List<DocumentSectionTypeDTO> LoadDocumentSectionTypes()
        {
            List<DocumentSectionTypeDTO> documentList = null;
            try
            {
                LiveProviderLogger.Info("Loading LoadDocumentSectionTypes");

                if (documentDAO == null)
                {
                    documentDAO = (DocumentManagerDAO)DAOFactory.GetDataAccessObject(typeof(DocumentManagerDAO));
                }

                documentList = documentDAO.LoadDocumentSectionTypes();
            }
            catch
            {
                throw;
            }
            return documentList;
        }

        /// <summary>
        /// Author:Sahibzada Sami ud Din
        /// Date:07/13/2011
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PaggingResponseDTO LoadPatientDocumentByClinicId(int clinicBranchID, int pageNo, int pageSize)
        {
            PaggingResponseDTO response = null;
            StringBuilder sb = new StringBuilder();
            try
            {
                LiveProviderLogger.Info("Uploading patient documents by clinic id");
                if (DocumentManagementInputValidator.ValidatePatientDocumentDTO(clinicBranchID, pageNo, pageSize, ref sb))
                {
                    if (documentDAO == null)
                    {
                        documentDAO = (DocumentManagerDAO)DAOFactory.GetDataAccessObject(typeof(DocumentManagerDAO));
                    }
                    response = documentDAO.LoadPatientDocumentByClinicId(clinicBranchID, pageNo, pageSize);
                    response.PatientDocumentsList = AddPatientInfo(response.PatientDocumentsList);
                }
                else
                {
                    throw new InvalidArgumentException(ErrorCodes.INVALID_AURGUMENTS_PREFIX, sb.ToString(), ErrorCodes.ComposeErrorCode
                        (ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX,
                        ErrorCodes.OPERATION_VALIDATOR_PREFIX, ErrorCodes.PARAMETER_NOT_POPULATE_PREFIX));
                }
            }
            catch
            {
                throw;
            }
            return response;
        }
       
        /// <summary>
        /// Author:Sahibzada Sami ud din
        /// Date:06/07/2011
        /// This method will manage patient documents.
        /// </summary>
        /// <param name="PatientDocumentObject"></param>
        /// <returns></returns>
        public int ManagePatientDocuments(PatientDocumentDTO PatientDocumentObject, string documentHostName)
        {
            LiveProviderLogger.Info("Managing Patient Documents Para::"+PatientDocumentObject.LastSeenDate+PatientDocumentObject.ModifiedAt);
            BaseUserDTO user = null;
            int Response = 0;
            bool IsUpdate = false;
            StringBuilder sb = new StringBuilder();
            LiveProviderLogger.Info("Managing Patient Documents 2");
            if (PatientDocumentObject != null && PatientDocumentObject.DocumentId > 0 && !PatientDocumentObject.IsDeleted)
            {
                IsUpdate = true;
                LiveProviderLogger.Info("Managing Patient Documents update");
            }
            try
            {
                LiveProviderLogger.Info("Managing Patient Documents 4");
                UserManagerServiceClient UserManagerClient = new UserManagerServiceClient();
                if (UserManagerClient == null)
                {
                    LiveProviderLogger.Info("User manager Client is null");
                }
                LiveProviderLogger.Info("Managing Patient Documents 5");
                if (PatientDocumentObject == null)
                {
                    LiveProviderLogger.Info("PatientDocumentObject is null");
                }
                LiveProviderLogger.Info("calling UserManagerClient.LoadBaseUserByUserId Para ::" + PatientDocumentObject.UploadedByUserId);
                if (!PatientDocumentObject.IsDeleted)
                {
                    user = UserManagerClient.LoadBaseUserByUserId(PatientDocumentObject.UploadedByUserId);
                    if (user != null)
                    {
                        PatientDocumentObject.UploadedByUserLastName = user.LastName;
                        PatientDocumentObject.UploadedByUserMiddleName = user.MiddleName;
                        PatientDocumentObject.UploadedByUserFirstName = user.FirstName;
                    }
                    string[] userName = null;
                    if (PatientDocumentObject.PatientFullName.IndexOf('|') > 0)
                    {
                        userName = PatientDocumentObject.PatientFullName.Split('|');
                    }
                    else
                    {
                        userName = PatientDocumentObject.PatientFullName.Split(' ');
                    }
                    if (userName.Length == 3)
                    {
                        PatientDocumentObject.PatientFirstName = userName[0];
                        PatientDocumentObject.PatientMiddleName = userName[1];
                        PatientDocumentObject.PatientLastName = userName[2];
                    }
                    else if (userName.Length == 2)
                    {
                        PatientDocumentObject.PatientFirstName = userName[0];
                        PatientDocumentObject.PatientLastName = userName[1];
                    }

                }
                if (DocumentManagementInputValidator.ValidatePatientDocumentDTO(PatientDocumentObject, ref sb, IsUpdate))
                {
                    if (!PatientDocumentObject.IsDeleted)
                    {
                        if(string.IsNullOrEmpty(PatientDocumentObject.ServerFilePath))
                            this.createServerFilePath(PatientDocumentObject, documentHostName);
                        LPPatientLastSeenDTO patientLastSeenObj = LoadPatientLastSeen(PatientDocumentObject.PatientID);
                        if (patientLastSeenObj != null)
                        {
                            PatientDocumentObject.LastSeenByUserId = patientLastSeenObj.LastSeenByUserID;
                            PatientDocumentObject.LastSeenDate = patientLastSeenObj.LastSeenDate.ToString();
                        }
                    }
                    if (documentDAO == null)
                    {
                        documentDAO = (DocumentManagerDAO)DAOFactory.GetDataAccessObject(typeof(DocumentManagerDAO));
                    }
                    
                    
                    Response = documentDAO.ManagePatientDocuments(PatientDocumentObject, IsUpdate);
                }
                else
                {
                    throw new InvalidArgumentException(ErrorCodes.INVALID_AURGUMENTS_PREFIX,sb.ToString(),ErrorCodes.ComposeErrorCode
                        (ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX,
                        ErrorCodes.OPERATION_VALIDATOR_PREFIX,ErrorCodes.PARAMETER_NOT_POPULATE_PREFIX));
                }

            }
            catch
            {
                throw;
            }
            return Response;
        }
       
        /// <summary>
        /// Author:Sahibzada Sami ud din
        /// </summary>
        /// <param name="FileContents"></param>
        /// <param name="patientId"></param>
        /// <param name="sectionId"></param>
        /// <param name="SessionID"></param>
        /// <param name="documentVirtualName"></param>
        public void UploadDocument(Stream FileContents, string patientId, string sectionName, string documentVirtualName, string fileName, string practice, string station, string documentRootDirectory)
        {
            StringBuilder sb = new StringBuilder();
            LiveProviderLogger.Info("UploadDocument call recive");
            try
            {
                #region upload logic
                if (DocumentManagementInputValidator.ValidateUploadDocumentPara(FileContents, patientId, sectionName, documentVirtualName, ref sb))
                {
                    FileManager.FileHandler(FileContents, patientId, sectionName, documentVirtualName, fileName, practice, station, documentRootDirectory);
                }
                else
                {
                    throw new InvalidArgumentException(ErrorCodes.INVALID_AURGUMENTS_PREFIX, sb.ToString(), ErrorCodes.ComposeErrorCode
                        (ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX,
                        ErrorCodes.OPERATION_VALIDATOR_PREFIX, ErrorCodes.PARAMETER_NOT_POPULATE_PREFIX));
                } 
                #endregion
            }
            catch
            {
                throw;
            }
        }
        private PatientDocumentDTO createServerFilePath(PatientDocumentDTO patientDocumentObject,string documentHostName)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                if (patientDocumentObject != null)
                {
                    if (DocumentManagementInputValidator.ValidateUploadDocumentPara(patientDocumentObject.PatientID.ToString(), patientDocumentObject.SectionId.ToString(), patientDocumentObject.DocumentVirtualName.ToString(), ref sb))
                    {
                        patientDocumentObject.ServerFilePath = FileManager.ServerFileCreator(patientDocumentObject.PatientID.ToString(), patientDocumentObject.SectionName,
                            patientDocumentObject.DocumentVirtualName.ToString(), patientDocumentObject.DocumentName, patientDocumentObject.PracticeName, patientDocumentObject.StationName, documentHostName);
                    }
                }
                else
                {
                    throw new InvalidArgumentException(ErrorCodes.INVALID_AURGUMENTS_PREFIX, sb.ToString(), ErrorCodes.ComposeErrorCode
                      (ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX,
                      ErrorCodes.OPERATION_VALIDATOR_PREFIX, ErrorCodes.PARAMETER_NOT_POPULATE_PREFIX));
                }
            }
            catch
            {
                throw;
            }
            return patientDocumentObject;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 7/22/2011
        /// This method will update ser
        /// </summary>
        /// <param name="serverFilePath"></param>
        /// <param name="DocumentVirtualName"></param>
        /// <returns></returns>

        public bool UpdateServerFilePath(string serverFilePath, Guid DocumentVirtualName)
        {
            bool response = true;
            StringBuilder sb = new StringBuilder();
            try
            {
                LiveProviderLogger.Info("Uploading patient documents by clinic id");
                if (DocumentManagementInputValidator.ValidateServerFilePathPara(serverFilePath, DocumentVirtualName, ref sb))
                {
                    serverFilePath = DocumentManagerErrorConstant.DOCUMENT_ROOT_PATH + serverFilePath;
                    if (documentDAO == null)
                    {
                        documentDAO = (DocumentManagerDAO)DAOFactory.GetDataAccessObject(typeof(DocumentManagerDAO));
                    }
                    response = documentDAO.UpdateServerFilePath(serverFilePath, DocumentVirtualName);
                }
                else
                {
                    throw new InvalidArgumentException(ErrorCodes.INVALID_AURGUMENTS_PREFIX, sb.ToString(), ErrorCodes.ComposeErrorCode
                        (ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX,
                        ErrorCodes.OPERATION_VALIDATOR_PREFIX, ErrorCodes.PARAMETER_NOT_POPULATE_PREFIX));
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 20/8/2011
        /// </summary>
        /// <returns></returns>

        public List<LiveProvider.Model.TypeDTOs.DocumentManager.SearchFieldTypeDTO> LoadSearchField()
        {
            List<LiveProvider.Model.TypeDTOs.DocumentManager.SearchFieldTypeDTO> searchList = null;
            try
            {
                LiveProviderLogger.Info("Loading LoadSearchField");

                if (documentDAO == null)
                {
                    documentDAO = (DocumentManagerDAO)DAOFactory.GetDataAccessObject(typeof(DocumentManagerDAO));
                }

                searchList = documentDAO.LoadSearchField();
            }
            catch
            {
                throw;
            }
            return searchList;
        }

        /// <summary>
        /// Author Sahibzada Sami uddin
        /// Date 20/08/2011
        /// This method will search patient documents
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="searchText"></param>
        /// <param name="fieldId"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>

        public PaggingResponseDTO SearchPatientDocumentByClinicId(int clinicBranchID, string searchText, int fieldID, int pageNo, int pageSize, int patientID)
        {
            PaggingResponseDTO response = null;
            StringBuilder sb = new StringBuilder();
            bool isFilter = false;
            DateTime fromDate=DateTime.Now, toDate=DateTime.Now;
            string sectionName = string.Empty;
            bool isDateFilter = false;
            bool isSectoinFilter = false;
            try
            {
                LiveProviderLogger.Info("Call SearchPatientDocumentByClinicId");
                if (DocumentManagementInputValidator.ValidateSearchingPara(searchText,clinicBranchID,pageNo, pageSize,ref sb))
                {
                    if (fieldID == 0)
                    {
                        isFilter = true;
                        string[] searchPara = searchText.Split(',');
                        sectionName = searchPara[0];
                        if (!string.IsNullOrEmpty(sectionName))
                        {
                            isFilter = false;
                            isDateFilter = false;
                            isSectoinFilter = true;
                        }
                        if (!string.IsNullOrEmpty(searchPara[1]))
                        {
                            isFilter = false;
                            isDateFilter = true;
                            isSectoinFilter = false;
                            if (!DocumentManagementInputValidator.IsValidSearchDate(searchPara[1], out fromDate))
                            {
                                throw new InvalidArgumentException(ErrorCodes.INVALID_AURGUMENTS_PREFIX, sb.ToString(), ErrorCodes.ComposeErrorCode
                                (ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX,
                                 ErrorCodes.OPERATION_VALIDATOR_PREFIX, ErrorCodes.PARAMETER_NOT_POPULATE_PREFIX));
                            }
                            //fromDate = DateTime.Parse(searchPara[1]);
                        }
                        if (!string.IsNullOrEmpty(searchPara[2]))
                        {
                            isFilter = false;
                            isDateFilter = true;
                            isSectoinFilter = false;
                            if (!DocumentManagementInputValidator.IsValidSearchDate(searchPara[2], out toDate))
                            {
                                throw new InvalidArgumentException(ErrorCodes.INVALID_AURGUMENTS_PREFIX, sb.ToString(), ErrorCodes.ComposeErrorCode
                                (ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX,
                                 ErrorCodes.OPERATION_VALIDATOR_PREFIX, ErrorCodes.PARAMETER_NOT_POPULATE_PREFIX));
                            }
                        }
                        if (!string.IsNullOrEmpty(searchPara[2]) && !string.IsNullOrEmpty(searchPara[1]) && !string.IsNullOrEmpty(searchPara[0]))
                        {
                            isFilter = true;
                            isDateFilter = false;
                            isSectoinFilter = false;
                        }
                    }
                    if (fieldID == DocumentManagerContants.DATE_SEARCH_VALUE)
                    {
                        if (!DocumentManagementInputValidator.IsValidSearchDate(searchText, out toDate))
                        {
                            throw new InvalidArgumentException(ErrorCodes.INVALID_AURGUMENTS_PREFIX, sb.ToString(), ErrorCodes.ComposeErrorCode
                            (ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX,
                             ErrorCodes.OPERATION_VALIDATOR_PREFIX, ErrorCodes.PARAMETER_NOT_POPULATE_PREFIX));
                        }
                    }

                    if (documentDAO == null)
                    {
                        documentDAO = (DocumentManagerDAO)DAOFactory.GetDataAccessObject(typeof(DocumentManagerDAO));
                    }
                    response = documentDAO.SearchPatientDocumentByClinicId(clinicBranchID, searchText, fieldID, pageNo, pageSize,isFilter,fromDate,toDate,sectionName,isDateFilter,isSectoinFilter,patientID);
                    response.PatientDocumentsList = AddPatientInfo(response.PatientDocumentsList);
                }
                else
                {
                    throw new InvalidArgumentException(ErrorCodes.INVALID_AURGUMENTS_PREFIX, sb.ToString(), ErrorCodes.ComposeErrorCode
                        (ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX,
                        ErrorCodes.OPERATION_VALIDATOR_PREFIX, ErrorCodes.PARAMETER_NOT_POPULATE_PREFIX));
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Sahizada Sami uddin
        /// Date 22/8/2011
        /// This method will laod patient document with respect to clinicid and patientid
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="patientID"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>

        public PaggingResponseDTO LoadPatientDocumentByClinicIdAndPatientID(int clinicBranchID, int patientID, int pageNo, int pageSize)
        {
            PaggingResponseDTO response = null;
            StringBuilder sb = new StringBuilder();
            try
            {
                LiveProviderLogger.Info("Uploading patient documents by clinic id");
                if (DocumentManagementInputValidator.ValidatePatientDocumentDTO(clinicBranchID, pageNo, pageSize, ref sb))
                {
                    if (documentDAO == null)
                    {
                        documentDAO = (DocumentManagerDAO)DAOFactory.GetDataAccessObject(typeof(DocumentManagerDAO));
                    }
                    response = documentDAO.LoadPatientDocumentByClinicIdAndPatientID(clinicBranchID, patientID, pageNo, pageSize);
                    
                    if (response == null)
                        response = new PaggingResponseDTO();

                    response.PatientDocumentsList = AddPatientInfo(response.PatientDocumentsList);
                }
                else
                {
                    throw new InvalidArgumentException(ErrorCodes.INVALID_AURGUMENTS_PREFIX, sb.ToString(), ErrorCodes.ComposeErrorCode
                        (ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX,
                        ErrorCodes.OPERATION_VALIDATOR_PREFIX, ErrorCodes.PARAMETER_NOT_POPULATE_PREFIX));
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public LPatientSearchResultDTO SearchPatientbyClinicIdAndSearchString(string clinicBranchId, string providerId, string userTypeId, string searchString, string demandPageNo)
        {
            LPatientSearchResultDTO searchResult = null;
            UserManagerServiceClient searchClient = null;
            PatientSearchResultDTO userManagerSearchResult = null;
            try
            {
                int intClinicBranchId = int.Parse(clinicBranchId);
                int intProviderId = int.Parse(providerId);
                int intUserTypeId = int.Parse(userTypeId);
                int intDemandPageNo = int.Parse(demandPageNo);
                LiveProviderLogger.Info("Calling LoadPatientbyClinicIdAndSearchString PARAM:: clinicId " + clinicBranchId + " providerId " + providerId + " userTypeId " + userTypeId + " searchString " + searchString + " demandPageNo " + demandPageNo);

                if (intClinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ErrorMessages.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                }
                if (intProviderId <= 0)
                {
                    throw new InvalidArgumentException(ErrorMessages.PROFILE_ID_INVALID_ERROR_MSG);
                }
                if (intUserTypeId <= 0)
                {
                    throw new InvalidArgumentException(ErrorMessages.USER_TYPE_INVALID_ERROR_MSG);
                }
                if (intDemandPageNo < 0)
                {
                    throw new InvalidArgumentException(ErrorMessages.DEMAND_PAGE_NO_INVALID_ERROR_MSG);
                }
                if (string.IsNullOrEmpty(searchString))
                {
                    throw new InvalidArgumentException(ErrorMessages.SEARCH_STRING_INVALID_ERROR_MSG);
                }
                if (searchClient == null)
                {
                    searchClient = new UserManagerServiceClient();
                }
                userManagerSearchResult = searchClient.SearchPatientbyClinicIdAndSearchString(intClinicBranchId, intProviderId, intUserTypeId, searchString, intDemandPageNo);
                if (userManagerSearchResult != null)
                {
                    if (searchResult == null)
                    {
                        searchResult = new LPatientSearchResultDTO();
                        searchResult.CurrentFirstRecordNo = userManagerSearchResult.CurrentFirstRecordNo;
                        searchResult.CurrentLastRecordNo = userManagerSearchResult.CurrentLastRecordNo;
                        searchResult.CurrentPageNo = userManagerSearchResult.CurrentPageNo;
                        searchResult.TotalNoOfRecords = userManagerSearchResult.TotalNoOfRecords;
                        List<LPatientSearchDTO> patientSerachList = new List<LPatientSearchDTO>();
                        PatientSearchDTO[] usrpatientSerachMngmntList = userManagerSearchResult.PatientSearchList;
                        foreach (PatientSearchDTO usrMngmntpatientSerachObj in usrpatientSerachMngmntList)
                        {
                            LPatientSearchDTO patientSearchObj = new LPatientSearchDTO();
                            patientSearchObj.FirstName = usrMngmntpatientSerachObj.FirstName;
                            patientSearchObj.LastName = usrMngmntpatientSerachObj.LastName;
                            patientSearchObj.MiddleName = usrMngmntpatientSerachObj.MiddleName;

                            //Below Check is added by Sami for correction of bug
                            if (string.IsNullOrEmpty(patientSearchObj.MiddleName))
                            {
                                patientSearchObj.MiddleName = string.Empty;
                            }

                            patientSearchObj.QuivusID = usrMngmntpatientSerachObj.QuivusID;
                            patientSearchObj.UserId = usrMngmntpatientSerachObj.UserId;
                            patientSearchObj.PatientId = usrMngmntpatientSerachObj.PatientId;
                            patientSearchObj.GenderId = usrMngmntpatientSerachObj.GenderId;
                            patientSearchObj.GenderName = usrMngmntpatientSerachObj.GenderName;
                            patientSearchObj.SSN = usrMngmntpatientSerachObj.SSN;
                            patientSearchObj.DOB = usrMngmntpatientSerachObj.DOB.ToShortDateString();
                            patientSerachList.Add(patientSearchObj);
                        }
                        searchResult.PatientSearchList = patientSerachList;
                    }
                }
            }
            catch
            {
                throw;
            }
            return searchResult;
        }

        public List<LProviderDTO> LoadAllProviderByClinicBranchId(string clinicBranchID)
        {
            List<LProviderDTO> providerList = null;
            ProviderDTO[] userManagerList = null;
            try
            {
                int intClinicBranchId = int.Parse(clinicBranchID);
                UserManagerServiceClient userManagerClient = new UserManagerServiceClient();
                userManagerList = userManagerClient.LoadAllProviderByClinicBranchId(intClinicBranchId);
                foreach (ProviderDTO userManagerProviderDTO in userManagerList)
                {
                    LProviderDTO providerObj = new LProviderDTO();
                    if (providerList == null)
                    {
                        providerList = new List<LProviderDTO>();
                    }
                    providerObj.FirstName = userManagerProviderDTO.FirstName;
                    providerObj.LastName = userManagerProviderDTO.LastName;
                    providerObj.MiddleName = userManagerProviderDTO.MiddleName;
                    if (string.IsNullOrEmpty(providerObj.MiddleName))
                    {
                        providerObj.MiddleName = "";
                    }
                    providerObj.UserId = userManagerProviderDTO.UserId;
                    providerList.Add(providerObj);
                }
            }
            catch
            {
                throw;
            }
            return providerList;
       }

        /// <summary>
        /// Author Sahibzada Sami uddin
        /// Date 26/08/2011
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public LPPatientLastSeenDTO LoadPatientLastSeen(int patientId)
        {
            LPPatientLastSeenDTO patientLastSeenLPObj = null;
            try
            {
                VisitServiceClient visitClient = new VisitServiceClient();
                PatientLastSeenDTO patientLastSeenObj = null;
                PatientLastSeenDTO[] patientLastSeenList = visitClient.LoadPatientLastSeenByPatientId(patientId);
                if (patientLastSeenList.Length > 0)
                {
                    patientLastSeenObj = patientLastSeenList[0];
                    if (patientLastSeenObj != null)
                    {
                        patientLastSeenLPObj = new LPPatientLastSeenDTO();
                        patientLastSeenLPObj.LastSeenByUserID = patientLastSeenObj.LastSeenByUserID;
                        patientLastSeenLPObj.LastSeenDate = patientLastSeenObj.LastSeen.ToString();
                    }
                }
            }
            catch
            {
                throw;
            }
            return patientLastSeenLPObj;
        }

        public List<PatientDocumentDTO> AddPatientInfo(List<PatientDocumentDTO> patientDocuments)
        {
            List<PatientDocumentDTO> documents = null;
            try
            {
                BaseUserDTO baseUserObj=new BaseUserDTO();
                UserManagerServiceClient userServiceClient = new UserManagerServiceClient();
                if (documents == null)
                    documents = new List<PatientDocumentDTO>();
                foreach (PatientDocumentDTO patDoc in patientDocuments)
                {
                    foreach (PatientDocumentDTO chdPatDoc in patDoc.ChildDocument)
                    {
                        baseUserObj = userServiceClient.LoadPatientBaseUserInfoByPatientIdForCompleteProfile(chdPatDoc.PatientID);
                        if (baseUserObj != null)
                        {
                            chdPatDoc.QuivusId = baseUserObj.QuivusUserID;
                            chdPatDoc.GenderName = baseUserObj.Gender.GenderName;
                        }
                        
                    }
                    documents.Add(patDoc);
                }
            
            }
            catch
            {
               throw;
            }
            return documents;
        }
        
        /// <summary>
        /// Author:Sahibzada Sami uddin
        /// Date:9/10/2011
        /// Description This method will create document file directory at server
        /// </summary>
        /// <param name="path"></param>
        /// <param name="ftpAddress"></param>
        /// <param name="ftpUser"></param>
        /// <param name="ftpPassword"></param>
        /// <returns></returns>
        

        public bool CreateServerPath(string path, string ftpAddress, string ftpUser, string ftpPassword)
        {
            ServerFileCreater pathManager = null;
            bool response = false;
            try
            {
                if (pathManager == null)
                {
                    pathManager = new ServerFileCreater();
                }

                response = pathManager.MakeDirectory(DocumentManagerContants.VENDOR_NAME, path, ftpAddress, ftpUser, ftpPassword);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami ud din
        /// Date 16 jan 2012
        /// This method will send email to user in case of total document uplaoded 
        /// sucessfully or not by the bulkupload.
        /// Note:: This method is temparay later on it will move to communication service.
        /// </summary>
        /// <param name="recipient"></param>
        /// <param name="feilds"></param>
        /// <returns></returns>
        public bool SendEmails(string[] recipient, string[] feilds, bool isSucess)
        {
            bool response = false;
            try
            {
                LiveProviderLogger.Info("Sending Email ::" + recipient);
                TemplateHandler handler = TemplateHandler.Instance;
                MailTemplate template=null;
                if (isSucess)
                {
                    template = handler[DocumentManagerContants.DOCUMENT_UPLAOD_EMAIL_TEMPLATE];
                    template.Body = String.Format(template.Body,
                                                feilds[0]
                                              , feilds[1]
                                              , feilds[2]
                                              , feilds[3]
                                              , feilds[4]
                                              , feilds[5]);
                }
                else
                {
                    template = handler[DocumentManagerContants.DOCUMENT_UPLAOD_EMAIL_FAILURE];
                    template.Body = String.Format(template.Body,
                                               feilds[0]
                                             , feilds[1]
                                             , feilds[2]
                                             , feilds[3]
                                             , feilds[4]
                                             , feilds[5]
                                             , feilds[6]);
                }

               

                handler.Send(template, recipient);
                response = true;
            }
            catch (Exception exp)
            {
                throw;
            }
            return response;
        }
    }
}
