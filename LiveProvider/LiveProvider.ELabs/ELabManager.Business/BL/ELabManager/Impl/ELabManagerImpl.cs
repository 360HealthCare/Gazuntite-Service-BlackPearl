using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Model.QuickInput;
using ELabManager.DataAccess.ELabManager;
using LiveProvider.Utility.Factory;
using LiveProvider.Utility.Logger;
using ELabManager.Business.Validator.ELab;
using LiveProvider.Utility.CustomExceptions;
using LiveProvider.Utility.Constant.ProjectConstant;
using LiveProvider.Utility.Constant.Errors;
using LiveProvider.Model.Response.LabManagement;
using LiveProvider.Model.ELab;
using ELabManager.IntegratingServices.UserManagerService;
using ELabManager.Business.ReferenceFactory;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace ELabManager.Business.BL.ELabManager.Impl
{
    public class ELabManagerImpl : IELabManager
    {

        ELabManagerDAO eLabDAO = null;
        UserManagerServiceClient userSeriveClient = null;
        public int ManagePatientLabs(LabDTO LabObject)
        {
            int Result = 0;
            bool IsUpdate = false;
            try
            {
                LiveProviderLogger.Info("Calling ManagePatientLabs PARAM :: LabObject " + LabObject);

                if (LabObject != null && LabObject.PatientLabID > 0)
                {
                    IsUpdate = true;
                }

                StringBuilder sb = new StringBuilder();

                if (LabValidator.ValidatVBLabDTO(LabObject, ref sb, IsUpdate))
                {

                    if (eLabDAO == null)
                    {
                        eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                    }

                    Result = eLabDAO.ManagePatientLabs(LabObject, IsUpdate);
                }
                else
                {
                    //   throw new InvalidArgumentException(sb.ToString()); // Validation Gets Failed Get Error MSG From String BUILDER & throw

                    throw new InvalidArgumentException(ELabContant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                                        ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.INVALID_AURGUMENTS_PREFIX,
                                                    ErrorCodes.SECTION_ELAB_MANAGEMENT_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                                    ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX)
                                                    );
                }
            }
            catch
            {
                throw;
            }
            return Result;
        }

        /// <summary>
        /// Author Sahibzada Sami
        /// Date 17 Oct 2011
        /// This Method Will be used to load Patient Labs by patrint Id
        /// </summary>
        /// <returns></returns>
        public List<LabDTO> LoadPatientLabsByPatientId(int PatientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected)
        {
            List<LabDTO> ReturnList = null;
            try
            {
                LiveProviderLogger.Info("Calling LoadPatientLabsByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ELabContant.PATIENT_ID_INVALID_ERROR_MSG, (int)ELabContant.ERROR_CODE.InvalidArguments);
                }
                else if (clinicBranchID <= 0)
                {
                    throw new InvalidArgumentException(ELabContant.CLINIC_ID_INVALID_ERROR_MSG, (int)ELabContant.ERROR_CODE.InvalidArguments);
                }

                if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }

                ReturnList = eLabDAO.LoadPatientLabsByPatientId(PatientId, clinicBranchID, false, stationIDs, isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }


        public List<LabDTO> LoadPatientLabsByVisit(int VisitId, bool IsTemplateVisit)
        {
            List<LabDTO> List = null;
            try
            {
                LiveProviderLogger.Info("Calling LoadPatientLabsByVisit PARAM :: VisitId " + VisitId);

                if (VisitId <= 0)
                {
                    throw new InvalidArgumentException(ELabContant.VISIT_ID_INVALID_ERROR_MSG, (int)ELabContant.ERROR_CODE.InvalidArguments);
                }

                if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }

                List = eLabDAO.LoadPatientLabsByVisit(VisitId, IsTemplateVisit);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return List;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 10 Oct 2011
        /// This Method Will be used to load Patient Labs by patient Id
        /// </summary>
        /// <returns></returns>                
        public List<LabDTO> LoadPatientLabsAllByPatientId(int PatientId)
        {
            List<LabDTO> ReturnList = null;
            try
            {
                LiveProviderLogger.Info("Calling LoadPatientLabsAllByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ELabContant.PATIENT_ID_INVALID_ERROR_MSG, (int)ELabContant.ERROR_CODE.InvalidArguments);
                }
                if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }

                ReturnList = eLabDAO.LoadPatientLabsAllByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Sami
        /// Date 17 Oct 2011
        /// This Method Will be used to load presription medication by patient  Id
        /// </summary>
        /// <returns></returns>
        public List<FollowupLabsDTO> LoadPatientLabTestsForFollowupByPatient(int PatientId)
        {
            List<FollowupLabsDTO> ReturnList = null;
            try
            {
                LiveProviderLogger.Info("Calling LoadPatientLabTestsForFollowupByPatient PARAM :: PatientPrescriptionId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ELabContant.PATIENT_ID_INVALID_ERROR_MSG, (int)ELabContant.ERROR_CODE.InvalidArguments);
                }

                if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }

                ReturnList = eLabDAO.LoadPatientLabTestsForFollowupByPatient(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 13 Feb 2012
        /// This method will load all lab order on the bases of clinic branch ids
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public LabPaggingResponseDTO LoadOrderByClinicBranch(int clinicBranchID, int pageNo)
        {
            LabPaggingResponseDTO response = null;
            try
            {
                if (clinicBranchID <= 0)
                {
                    throw new InvalidArgumentException(ELabContant.CLINIC_ID_INVALID_ERROR_MSG, (int)ELabContant.ERROR_CODE.InvalidArguments);
                }
                else if (pageNo <= 0)
                {
                    throw new InvalidArgumentException(ELabContant.PAGE_NO_INVALID_ERROR_MSG, (int)ELabContant.ERROR_CODE.InvalidArguments);
                }

                if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }
                response = eLabDAO.LoadLabOrdersByClinicBranch(clinicBranchID, pageNo, ELabContant.PAGE_SIZE);
                if (response != null && response.LabList.Count > 0)
                {
                    response.LabList = _getProviderCompleteInfo(response.LabList);
                    response.LabList = _getpatientCompleteInfo(response.LabList);
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
        /// Date 13 Feb 2012
        /// This method will get detail of patient on the basis of provider from user service
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<OrderManagementDTO> _getpatientCompleteInfo(List<OrderManagementDTO> list)
        {
            List<OrderManagementDTO> response = new List<OrderManagementDTO>();
            try
            {
                List<int> patientIds = _getPatientIdsFromList(list);
                if (userSeriveClient == null)
                {
                    userSeriveClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }
                List<PatientBasicUserDTO> patientBaseUserList = userSeriveClient.GetUserBasicInfoByPatientIds(patientIds);
                foreach (OrderManagementDTO order in list)
                {
                    foreach (PatientBasicUserDTO basePatientUser in patientBaseUserList)
                    {
                        if (order.Patient.PatientId == basePatientUser.PatientId)
                        {
                            response.Add(_copyBaseUserInfoToPatient(basePatientUser, order));
                        }
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
        /// Author Sahibzada Sami ud Din
        /// Date 13 Feb 2012
        /// This method will copy baseuser properities to provider of order
        /// </summary>
        /// <param name="baseUser"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private OrderManagementDTO _copyBaseUserInfoToPatient(PatientBasicUserDTO baseUser, OrderManagementDTO order)
        {
            try
            {
                order.Patient.PatientFirstName = baseUser.FirstName;
                order.Patient.PatientLastName = baseUser.LastName;
                if (!string.IsNullOrEmpty(baseUser.MiddleName))
                    order.Patient.PatientMiddleName = baseUser.MiddleName;
            }
            catch
            {
                throw;
            }
            return order;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 13 Feb 2012</summary>
        /// This method will get distinct patientids from order management list.
        /// <param name="list"></param>
        /// <returns></returns>
        private List<int> _getPatientIdsFromList(List<OrderManagementDTO> list)
        {
            List<int> response = new List<int>();
            bool isExits = false;
            try
            {
                foreach (OrderManagementDTO obj in list)
                {
                    //if (response.Count <= 0)
                        isExits = false;
                    foreach (int id in response)
                    {
                        if (obj.Patient.PatientId == id)
                        {
                            isExits = true;
                            break;
                        }
                    }
                    if (!isExits)
                    {
                        response.Add(obj.Patient.PatientId);
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
        /// Author Sahibzada Sami ud Din
        /// Date 13 Feb 2012
        /// This method will get detail of provider on the basis of provider from user service
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<OrderManagementDTO> _getProviderCompleteInfo(List<OrderManagementDTO> list)
        {
            List<OrderManagementDTO> response = new List<OrderManagementDTO>();
            try
            {
                List<int> providerIds = _getProviderIdsFromList(list);
                if (userSeriveClient == null)
                {
                    userSeriveClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }
                List<UserBasicInfoDTO> baseUserList = userSeriveClient.GetUserBasicInfoById(providerIds);
                foreach (OrderManagementDTO order in list)
                {
                    foreach (UserBasicInfoDTO baseUser in baseUserList)
                    {
                        if (order.Provider.ProviderID == baseUser.UserId)
                        {
                            response.Add(_copyBaseUserInfoToProvider(baseUser, order));
                        }
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
        /// Author Sahibzada Sami ud Din
        /// Date 13 Feb 2012
        /// This method will copy baseuser properities to provider of order
        /// </summary>
        /// <param name="baseUser"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private OrderManagementDTO _copyBaseUserInfoToProvider(UserBasicInfoDTO baseUser, OrderManagementDTO order)
        {
            try
            {
                order.Provider.ProviderFirstName = baseUser.FirstName;
                order.Provider.ProviderLastName = baseUser.LastName;
                if (!string.IsNullOrEmpty(baseUser.MiddleName))
                    order.Provider.ProviderMiddleName = baseUser.MiddleName;
            }
            catch
            {
                throw;
            }
            return order;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 13 Feb 2012
        /// This method will get distinct userids from order management list.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<int> _getProviderIdsFromList(List<OrderManagementDTO> list)
        {
            List<int> response = new List<int>();
            bool isExits = false;
            try
            {
                foreach (OrderManagementDTO obj in list)
                {
                    //if (response.Count <= 0)
                        isExits = false;
                    foreach (int id in response)
                    {
                        if (obj.Provider.ProviderID == id)
                        {
                            isExits = true;
                            break;
                        }
                    }
                    if (!isExits)
                    {
                        response.Add(obj.Provider.ProviderID);
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
        /// Author Sahibzada Sami ud Din
        /// Date 20 Feb 2012
        /// This method will return search fields for order and result management on the bases of parameters.
        /// </summary>
        /// <param name="IsLabOrder"></param>
        /// <param name="IsLabResult"></param>
        /// <returns></returns>
        public StaticDataResponse LoadStaticData()
        {
            StaticDataResponse response = null;
            try
            {
                if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }
                response = eLabDAO.LoadSearchFeilds();
                response.Labs = eLabDAO.LoadLabTypes(); 
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 13 Feb 2012
        /// This method will load all lab results on the bases of clinic branch ids
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public ResultPagingResponse LoadResultsByClinicBranch(int clinicBranchID, int pageNo)
        {
            ResultPagingResponse response = null;
            try
            {
                if (clinicBranchID <= 0)
                {
                    throw new InvalidArgumentException(ELabContant.CLINIC_ID_INVALID_ERROR_MSG, (int)ELabContant.ERROR_CODE.InvalidArguments);
                }
                else if (pageNo <= 0)
                {
                    throw new InvalidArgumentException(ELabContant.PAGE_NO_INVALID_ERROR_MSG, (int)ELabContant.ERROR_CODE.InvalidArguments);
                }

                if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }
                response = eLabDAO.LoadLabResultsByClinicBranch(clinicBranchID, pageNo, ELabContant.PAGE_SIZE);
                if (response != null)
                {
                    if (response.LabList != null && response.LabList.Count > 0)
                    {
                        response.LabList = _getProviderCompleteInfo(response.LabList);
                        response.LabList = _getpatientCompleteInfo(response.LabList);
                    }
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public bool ProcessLabOrder(List<string> orderNumbers, string date)
        {
            LiveProviderLogger.Info("LoadResultByClinicBranch call recived in business");
            bool response = false;
            try
            {
                if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }
                if (orderNumbers != null && orderNumbers.Count > 0)
                {
                    response = eLabDAO.ProcessLabOrder(orderNumbers);
                }
                else
                {
                    response = eLabDAO.ProcessLabOrder(DateTime.Parse(date));
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public LabPaggingResponseDTO SearchLabOrder(int clinicBranchID, int pageNo, string searchText, int fieldID, int providerId, int userTypeId)
        {
            LabPaggingResponseDTO response = null;
            try
            {
                if (clinicBranchID <= 0)
                {
                    throw new InvalidArgumentException(ELabContant.CLINIC_ID_INVALID_ERROR_MSG, (int)ELabContant.ERROR_CODE.InvalidArguments);
                }
                else if (pageNo <= 0)
                {
                    throw new InvalidArgumentException(ELabContant.PAGE_NO_INVALID_ERROR_MSG, (int)ELabContant.ERROR_CODE.InvalidArguments);
                }
                else if (fieldID <= 0)
                {
                    throw new InvalidArgumentException(ELabContant.FIELD_ID_INVALID_ERROR_MSG, (int)ELabContant.ERROR_CODE.InvalidArguments);
                }
                if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }
                if (fieldID == ELabContant.PATIENT_SEARCH_FIELD_ID || fieldID == ELabContant.PROVIDER_SEARCH_FIELD_ID)
                {
                    if (fieldID == ELabContant.PATIENT_SEARCH_FIELD_ID)
                    {
                        List<int> patientIDs = _searchPatient(clinicBranchID, searchText, providerId, userTypeId);
                        response = eLabDAO.SearchLabOrderByPatienOrProvidertIDs(clinicBranchID, pageNo, ELabContant.PAGE_SIZE, fieldID, patientIDs);
                    }
                    else if(fieldID == ELabContant.PROVIDER_SEARCH_FIELD_ID)
                    {
                        List<int> providerIDs = _searchProvider(searchText);
                        response = eLabDAO.SearchLabOrderByPatienOrProvidertIDs(clinicBranchID, pageNo, ELabContant.PAGE_SIZE, fieldID, providerIDs);
                    }
                }
                else
                {
                    if(fieldID == ELabContant.DATE_SEARCH_FIELD_ID)
                        response = eLabDAO.LoadLabOrdersByClinicBranchAndSearchString(clinicBranchID, pageNo, ELabContant.PAGE_SIZE, searchText,DateTime.Parse(searchText),fieldID);
                    else
                        response = eLabDAO.LoadLabOrdersByClinicBranchAndSearchString(clinicBranchID, pageNo, ELabContant.PAGE_SIZE, searchText, DateTime.Now, fieldID);
                }
                if (response != null && response.LabList != null && response.LabList.Count > 0)
                {
                    response.LabList = _getProviderCompleteInfo(response.LabList);
                    response.LabList = _getpatientCompleteInfo(response.LabList);
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public ResultPagingResponse SearchLabResult(int clinicBranchID, int pageNo, string searchText, int fieldID, int providerId, int userTypeId)
        {
            ResultPagingResponse response = null;
            try
            {
                if (clinicBranchID <= 0)
                {
                    throw new InvalidArgumentException(ELabContant.CLINIC_ID_INVALID_ERROR_MSG, (int)ELabContant.ERROR_CODE.InvalidArguments);
                }
                else if (pageNo <= 0)
                {
                    throw new InvalidArgumentException(ELabContant.PAGE_NO_INVALID_ERROR_MSG, (int)ELabContant.ERROR_CODE.InvalidArguments);
                }
                else if (fieldID <= 0)
                {
                    throw new InvalidArgumentException(ELabContant.FIELD_ID_INVALID_ERROR_MSG, (int)ELabContant.ERROR_CODE.InvalidArguments);
                }
                if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }
                if (fieldID == ELabContant.PATIENT_SEARCH_FIELD_ID || fieldID == ELabContant.PROVIDER_SEARCH_FIELD_ID)
                {
                    if (fieldID == ELabContant.PATIENT_SEARCH_FIELD_ID)
                    {
                        List<int> patientIDs = _searchPatient(clinicBranchID, searchText, providerId, userTypeId);
                        response = eLabDAO.SearchLabResultByPatientOrProviderIDs(clinicBranchID, pageNo, ELabContant.PAGE_SIZE, fieldID, patientIDs);
                    }
                    else if(fieldID == ELabContant.PROVIDER_SEARCH_FIELD_ID)
                    {
                        List<int> providerIDs = _searchProvider(searchText);
                        response = eLabDAO.SearchLabResultByPatientOrProviderIDs(clinicBranchID, pageNo, ELabContant.PAGE_SIZE, fieldID, providerIDs);
                    }
                }
                else
                {
                    if(fieldID == ELabContant.DATE_SEARCH_FIELD_ID)
                        response = eLabDAO.LoadLabResultsByClinicBranchAndSearchString(clinicBranchID, pageNo, ELabContant.PAGE_SIZE, searchText,DateTime.Parse(searchText),fieldID);
                    else
                        response = eLabDAO.LoadLabResultsByClinicBranchAndSearchString(clinicBranchID, pageNo, ELabContant.PAGE_SIZE, searchText, DateTime.Now, fieldID);
                }
                if (response != null && response.LabList!=null && response.LabList.Count > 0)
                {
                    response.LabList = _getProviderCompleteInfo(response.LabList);
                    response.LabList = _getpatientCompleteInfo(response.LabList);
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
        /// Date 22 Feb 2012
        /// This method will search patient from user service and return patient ids list
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        private List<int> _searchPatient(int clinicBranchId,string searchText,int providerId,int userTypeId)
        {
            List<int> response = null;
            try
            {
                if (userSeriveClient == null)
                {
                    userSeriveClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }
                PatientSearchResultDTO patientSearchResult = userSeriveClient.SearchPatientbyClinicIdAndSearchString(clinicBranchId, providerId, userTypeId, searchText, 1);
                response = _getPatientIDs(patientSearchResult);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 22 Feb 2012
        /// This method will search provider from user service and return provider ids list
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        private List<int> _searchProvider(string searchText)
        {
            List<int> response = null;
            try
            {
                if (userSeriveClient == null)
                {
                    userSeriveClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }
                SearchProviderCriteriaDTO criteria = new SearchProviderCriteriaDTO();
                criteria.FirstName = searchText;
                List<ProviderSearchDTO> lstSearch = userSeriveClient.SearchProviderForAppointment(criteria);
                response = _getProviderIDs(lstSearch);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 22 Feb 2012
        /// This method will get user ids of provider from search List
        /// </summary>
        /// <param name="lstSearch"></param>
        /// <returns></returns>
        private List<int> _getProviderIDs(List<ProviderSearchDTO> lstSearch)
        {
            List<int> providerIDs = null;
            try
            {
                if (lstSearch != null)
                    providerIDs = new List<int>();
                foreach (ProviderSearchDTO provider in lstSearch)
                {
                    providerIDs.Add(provider.ProviderUserId);
                }
            }
            catch
            {
                throw;
            }
            return providerIDs;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 22 Feb 2012
        /// This method will get patientIds from search result
        /// </summary>
        /// <param name="patientSearchResult"></param>
        /// <returns></returns>
        private List<int> _getPatientIDs(PatientSearchResultDTO patientSearchResult)
        {
            List<int> response = null;
            try
            {
                if (patientSearchResult != null)
                {
                    response = new List<int>();
                }
                foreach (PatientSearchDTO searchResult in patientSearchResult.PatientSearchList)
                {
                    response.Add(searchResult.PatientId);
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
        /// Date 06 March 2012
        /// This method will filter patient lab order on the bases of criteria.
        /// </summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="pageNo"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public LabPaggingResponseDTO FilterPatientLabOrders(int clinicBranchId, int pageNo, string searchString)
        {
            LabPaggingResponseDTO response = null;
            DateTime dateTo;
            DateTime dateFrom;
            string status =string.Empty;
            int patientId = 0;
            int labId = 0;
            bool isLabFilter = false;
            bool isPatientFilter = false;
            bool isDateFilter = false;
            try
            {
                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ELabContant.CLINIC_ID_INVALID_ERROR_MSG, (int)ELabContant.ERROR_CODE.InvalidArguments);
                }
                else if (pageNo <= 0)
                {
                    throw new InvalidArgumentException(ELabContant.PAGE_NO_INVALID_ERROR_MSG, (int)ELabContant.ERROR_CODE.InvalidArguments);
                }
                 if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }
                string[] searchCriteria = searchString.Split('|');
                if(!string.IsNullOrEmpty(searchCriteria[0]))
                {
                    dateFrom = DateTime.Parse(searchCriteria[0]);
                    isDateFilter=true;
                }
                else
                {
                    dateFrom = DateTime.MinValue;
                }
                if(!string.IsNullOrEmpty(searchCriteria[1]))
                {
                    dateTo = DateTime.Parse(searchCriteria[1]);
                    isDateFilter=true;
                }
                else
                {
                    dateTo = DateTime.MinValue;
                }
                status = searchCriteria[2];

                if(!string.IsNullOrEmpty(searchCriteria[3]))
                {
                    patientId=int.Parse(searchCriteria[3]);
                    isPatientFilter = true;
                }
                
                if(!string.IsNullOrEmpty(searchCriteria[4]))
                {
                    labId=int.Parse(searchCriteria[4]);
                    isLabFilter = true;
                }

                response = eLabDAO.FilterPatientLabOrders(clinicBranchId, pageNo, ELabContant.PAGE_SIZE, labId, patientId, 
                    isPatientFilter, isLabFilter, isDateFilter, dateTo, dateFrom);
                if (response != null && response.LabList != null && response.LabList.Count > 0)
                {
                    response.LabList = _getProviderCompleteInfo(response.LabList);
                    response.LabList = _getpatientCompleteInfo(response.LabList);
                }
            }
            catch
            {

            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 06 March 2012
        /// This method will filter patient lab result on the bases of criteria.
        /// </summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="pageNo"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public ResultPagingResponse FilterPatientLabResults(int clinicBranchId, int pageNo, string searchString)
        {
            ResultPagingResponse response = null;
            DateTime dateTo;
            DateTime dateFrom;
            string status = string.Empty;
            int patientId = 0;
            int labId = 0;
            bool isLabFilter = false;
            bool isPatientFilter = false;
            bool isDateFilter = false;
            bool isStatusFilter = false;
            try
            {
                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ELabContant.CLINIC_ID_INVALID_ERROR_MSG, (int)ELabContant.ERROR_CODE.InvalidArguments);
                }
                else if (pageNo <= 0)
                {
                    throw new InvalidArgumentException(ELabContant.PAGE_NO_INVALID_ERROR_MSG, (int)ELabContant.ERROR_CODE.InvalidArguments);
                }
                if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }
                string[] searchCriteria = searchString.Split('|');
                if (!string.IsNullOrEmpty(searchCriteria[0]))
                {
                    dateFrom = DateTime.Parse(searchCriteria[0]);
                    isDateFilter = true;
                }
                else
                {
                    dateFrom = DateTime.MinValue;
                }
                if (!string.IsNullOrEmpty(searchCriteria[1]))
                {
                    dateTo = DateTime.Parse(searchCriteria[1]);
                    isDateFilter = true;
                }
                else
                {
                    dateTo = DateTime.MinValue;
                }

                if (!string.IsNullOrEmpty(searchCriteria[2]))
                {
                    status = searchCriteria[2];
                    isStatusFilter = true;
                }

                if (!string.IsNullOrEmpty(searchCriteria[3]))
                {
                    patientId = int.Parse(searchCriteria[3]);
                    isPatientFilter = true;
                }

                if (!string.IsNullOrEmpty(searchCriteria[4]))
                {
                    labId = int.Parse(searchCriteria[4]);
                    isLabFilter = true;
                }

                response = eLabDAO.FilterPatientLabResults(clinicBranchId, pageNo, ELabContant.PAGE_SIZE, labId, 
                    patientId, isPatientFilter, isLabFilter,isStatusFilter, isDateFilter,status, dateTo, dateFrom);
                if (response != null && response.LabList != null && response.LabList.Count > 0)
                {
                    response.LabList = _getProviderCompleteInfo(response.LabList);
                    response.LabList = _getpatientCompleteInfo(response.LabList);
                }
            }
            catch
            {

            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 06 March 2012
        /// This method will process lab order on the bases of criteria.
        /// </summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="pageNo"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public bool ProcessLabOrderByCriteria(string searchString)
        {
            LiveProviderLogger.Info("ProcessLabOrderByCriteria call recived in business");
            bool response = false;
            DateTime dateTo;
            DateTime dateFrom;
            string status = string.Empty;
            int patientId = 0;
            int labId = 0;
            bool isLabFilter = false;
            bool isPatientFilter = false;
            bool isDateFilter = false;
            bool isStatusFilter = false;
            try
            {
                if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }
                string[] searchCriteria = searchString.Split('|');
                if (!string.IsNullOrEmpty(searchCriteria[0]))
                {
                    dateFrom = DateTime.Parse(searchCriteria[0]);
                    isDateFilter = true;
                }
                else
                {
                    dateFrom = DateTime.MinValue;
                }
                if (!string.IsNullOrEmpty(searchCriteria[1]))
                {
                    dateTo = DateTime.Parse(searchCriteria[1]);
                    isDateFilter = true;
                }
                else
                {
                    dateTo = DateTime.MinValue;
                }

                if (!string.IsNullOrEmpty(searchCriteria[2]))
                {
                    status = searchCriteria[2];
                    isStatusFilter = true;
                }

                if (!string.IsNullOrEmpty(searchCriteria[3]))
                {
                    patientId = int.Parse(searchCriteria[3]);
                    isPatientFilter = true;
                }

                if (!string.IsNullOrEmpty(searchCriteria[4]))
                {
                    labId = int.Parse(searchCriteria[4]);
                    isLabFilter = true;
                }
                response = eLabDAO.ProcessLabOrderByCriteria(labId,patientId, isPatientFilter, isLabFilter, isDateFilter, dateTo, dateFrom);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public bool ManageLabOrder(XmlDocument order)
        {
            LiveProviderLogger.Info("ManageLabOrder call recived in business");
            bool response = false;
            OrderManagementDTO OrderDto = new OrderManagementDTO();
            XmlDocument doc = new XmlDocument();
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Lab";
            xRoot.IsNullable = true;
            XmlSerializer serializer = new XmlSerializer(typeof(OrderManagementDTO), xRoot);
            string xml = order.InnerXml;
            OrderDto = (OrderManagementDTO)serializer.Deserialize(new StringReader(xml));
            try
            {
                if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }
                response = eLabDAO.ManageLabOrder(OrderDto);
            }
            catch
            {
                throw;
            }
            return response;
        }
    }
}
