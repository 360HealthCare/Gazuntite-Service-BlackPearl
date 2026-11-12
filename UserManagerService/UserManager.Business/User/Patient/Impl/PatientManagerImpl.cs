using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Quivus.Utility.Logger;
using Quivus.Utility;
using Quivus.Utility.CustomException;
 
using UserManagerService.DataAccess;
using UserManagerService.DataAccess.Patient;
using Quivus.Utility.Factory;
using UserManager.Business.Validators;

using Quivus.Model.RecentPatient;
using Quivus.Model.Search;
using Quivus.Model.PatientProfileCustomization;
using Quivus.Model.AddressBook;
using UserManager.Business.Utility;
using Quivus.Model.PatientProfileCustomization;
using Quivus.Model.User.Patient;
using QuivusModel.User.Patient;
using QuivusModel.Clinic;

namespace UserManager.Business.User.Patient.Impl
{
    public class PatientManagerImpl : IPatientManager
    {
        /// <summary>
        /// Author Mansoor
        /// Date 1 Feb 2011
        /// </summary>       
        PatientDAO patientDAO = null;
        //IAddressBookService addressBookServiceManager = null;

        public bool SaveRecentPatient(int clinicBranchId, int providerUserId, int patientId)
        {
            bool result;

            try
            {
                QuivusLogger.Info("Calling SaveRecentPatient PARAM:: clinicBranchId" + clinicBranchId + " providerUserId" + providerUserId + " patientId" + patientId);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                }
                if (providerUserId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PROFILE_ID_INVALID_ERROR_MSG);
                }
                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }

                result = patientDAO.SaveRecentPatient(clinicBranchId, providerUserId, patientId);

            }
            catch 
            {
                throw;
            }
            return result;
        }

        public List<RecentPatientDTO> LoadRecentPatientsByProviderIdAndBranchId(int providerUserId, int clinicBranchId)
        {
            List<RecentPatientDTO> result;

            try
            {
                QuivusLogger.Info("Calling SaveRecentPatient PARAM:: clinicBranchId" + clinicBranchId + " providerUserId" + providerUserId);

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

                result = patientDAO.LoadRecentPatientsByProviderIdAndBranchId(providerUserId, clinicBranchId);
            }
            catch 
            {
                throw;
            }
            return result;
        }

        public PatientSearchResultDTO LoadPatientbyClinicIdAndSearchString(int clinicBranchId, int providerId, int userTypeId, string searchString, int demandPageNo)
        {
            PatientSearchResultDTO searchResult = null;

            bool isDateSearch = false;
            bool isSSNSearch = false;

            string SSN, dashSSN;


            DateTime DOB = new DateTime();

            try
            {
                QuivusLogger.Info("Calling LoadPatientbyClinicIdAndSearchString PARAM:: clinicId " + clinicBranchId + " providerId " + providerId + " userTypeId " + userTypeId + " searchString " + searchString + " demandPageNo " + demandPageNo);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                }
                if (providerId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PROFILE_ID_INVALID_ERROR_MSG);
                }
                if (userTypeId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_TYPE_INVALID_ERROR_MSG);
                }
                if (demandPageNo < 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.DEMAND_PAGE_NO_INVALID_ERROR_MSG);
                }
                if (string.IsNullOrEmpty(searchString))
                {
                    throw new InvalidArgumentException(ProjectConstant.SEARCH_STRING_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }            

                isDateSearch = Validations.IsValidSearchDate(searchString, out DOB);

                isSSNSearch = Validations.IsValidateSearchSSN(searchString, out SSN, out dashSSN);

                int pageSize = ProjectConstant.PATIENT_SEARCH_PAGE_SIZE;

                searchResult = patientDAO.LaodPatientbyClinicIdAndSearchString(clinicBranchId, providerId, userTypeId, searchString, demandPageNo, pageSize, isDateSearch, DOB, isSSNSearch, SSN, dashSSN);
            }
            catch
            {
                throw;
            }
            return searchResult;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the patient info to show in the left window panel, also log in the most recent patients
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <returns></returns> 
        public PatientProfileTaggedDTO LoadPatientProfileInfoForProvider(int patientId, int clinicBranchId, int providerUserId)
        {
            PatientProfileTaggedDTO response = null;

            PatientProfileBasicInfoDTO patientProfileBasicInfoObj = null;
            PatientInsuracneInfoDTO patientInsuracneInfoObj = null;
            PhoneDTO phoneDTOobj = null;

            try
            {
                QuivusLogger.Info("LoadPatientProfileInfoForProvider Called PARAM patientId::" + patientId);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }

                patientProfileBasicInfoObj = patientDAO.LoadPatientProfileBasicInfo(patientId);
                patientInsuracneInfoObj = patientDAO.LoadPatientInsuranceInfo(patientId);

                //if (addressBookServiceManager == null)
                //{
                //    addressBookServiceManager = new AddressBookServiceImpl();
                //}

                //if (patientProfileBasicInfoObj != null)
                //{
                //    phoneDTOobj = addressBookServiceManager.GetPartyPrimaryPhoneInfoByGuid(patientProfileBasicInfoObj.RecGuid);
                //}

                patientDAO.SaveRecentPatient(clinicBranchId,providerUserId, patientId);

                if (patientProfileBasicInfoObj != null)
                {
                    if (response == null)
                    {
                        response = new PatientProfileTaggedDTO();
                    }
                    response.PatientId = patientId;
                    response.PatientProfileBasicInfoData = patientProfileBasicInfoObj;
                    response.PatientInsuracneInfoData = patientInsuracneInfoObj;
                  //  response.PrimaryPhoneNo = phoneDTOobj;
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
        /// load the patient info to show in the left window panel, also log in the most recent patients
        /// <param name="patientId"></param>
        /// <returns></returns>
        public PatientProfileAllergyDTO LoadPatientAllergiesForProvider(int patientId)
        {
            PatientProfileAllergyDTO response = null;
            List<PatientAllergyInfoDTO> PatientAllergyInfoList = null;

            try
            {
                QuivusLogger.Info("LoadPatientAllergiesForProvider Called PARAM patientId::" + patientId);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }

                PatientAllergyInfoList = patientDAO.LoadPatientAllergiesByPatientId(patientId);

                if (PatientAllergyInfoList != null)
                {
                    if (response == null)
                    {
                        response = new PatientProfileAllergyDTO();
                    }
                    response.PatientId = patientId;
                    response.PatientAllergyInfoData = PatientAllergyInfoList;                    
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
        public PatientProfileDiagnosisDTO LoadPatientDiagnosisForProvider(int patientId)
        {
            PatientProfileDiagnosisDTO response = null;
            List<PatientDiagnosisInfoDTO> PatientDiagnosisInfoList = null;

            try
            {
                QuivusLogger.Info("LoadPatientDiagnosisForProvider Called PARAM patientId::" + patientId);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }

                PatientDiagnosisInfoList = patientDAO.LoadPatientDiagnosisByPatientId(patientId);

                if (PatientDiagnosisInfoList != null)
                {
                    if (response == null)
                    {
                        response = new PatientProfileDiagnosisDTO();
                    }
                    response.PatientId = patientId;
                    response.PatientDiagnosisInfoData = PatientDiagnosisInfoList;
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
        public PatientProfileImmuniztionDTO LoadPatientImmunizationForProvider(int patientId)
        {
            PatientProfileImmuniztionDTO response = null;
            List<PatientImmunizationInfoDTO> PatientImmunizationInfoList = null;

            try
            {
                QuivusLogger.Info("LoadPatientDiagnosisForProvider Called PARAM patientId::" + patientId);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }

                PatientImmunizationInfoList = patientDAO.LoadPatientImmunizationByPatientId(patientId);

                if (PatientImmunizationInfoList != null)
                {
                    if (response == null)
                    {
                        response = new PatientProfileImmuniztionDTO();
                    }
                    response.PatientId = patientId;
                    response.PatientImmunizationInfoData = PatientImmunizationInfoList;
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
        public PatientProfileChronicMedicationDTO LoadPatientMedicationForProvider(int patientId)
        {
            PatientProfileChronicMedicationDTO response = null;
            List<PatientChronicMedicationInfoDTO> PatientChronicMedicationInfoList = null;
            try
            {
                QuivusLogger.Info("LoadPatientMedicationForProvider Called PARAM patientId::" + patientId);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }

                PatientChronicMedicationInfoList = patientDAO.LoadPatientMedicationByPatientId(patientId);

                if (PatientChronicMedicationInfoList != null)
                {
                    if (response == null)
                    {
                        response = new PatientProfileChronicMedicationDTO();
                    }
                    response.PatientId = patientId;
                    response.PatientChronicMedicationInfoData = PatientChronicMedicationInfoList;
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
        /// Date 4 feb 2011
        /// Method Used to load insurance Info
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public PatientProfileInsuranceDTO LoadPatientInsuranceForProvider(int patientId) 
        {
            PatientProfileInsuranceDTO response = null;
            List<PatientInsuracneInfoDTO> PatientInsuracneInfoList = null;
            try
            {
                QuivusLogger.Info("LoadPatientInsuranceForProvider Called PARAM patientId::" + patientId);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }

                PatientInsuracneInfoList = patientDAO.LoadPatientInsuranceForProvider(patientId);

                if (PatientInsuracneInfoList != null)
                {
                    if (response == null)
                    {
                        response = new PatientProfileInsuranceDTO();
                       
                    }
                    response.PatientId = patientId;
                    response.PatientInsuranceInfoData = PatientInsuracneInfoList;
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

        public PatientDTO LoadPatientByPatientId(int PatientId)
        {
            PatientDTO response = null;
         
            try
            {
                QuivusLogger.Info("LoadPatientInsuranceForProvider Called PARAM patientId::" + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }

                response = patientDAO.LoadPatientInfoByPatientIdForCompleteProfile(PatientId);

            }
            catch
            {
                throw;

            }
            return response;
        }

          /// <summary>
        /// Author Muhammad ISmail
        /// Date 26 august 2011
        /// Method Used To load patient basic info by Id
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public PatientBasicInfoDTO LoadPatientBasicInfoByPatientId(int PatientId)
        {
            PatientBasicInfoDTO response = null;

            try
            {
                QuivusLogger.Info("LoadPatientInsuranceForProvider Called PARAM patientId::" + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }

                response = patientDAO.LoadPatientBasicInfoByPatientId(PatientId);

            }
            catch
            {
                throw;

            }
            return response;
        }
        
         /// <summary>
        /// Author Muhammad ISmail
        /// Date 27 august 2011
        /// Method Used To load patient basic info by Id
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public PatientUserInfo LoadPatientUserInfoByQuivusId(String QuivusId)
        {
            PatientUserInfo response = null;
            try
            {
                QuivusLogger.Info("LoadPatientUserInfoByQuivusId Called PARAM QuivusId::" + QuivusId);                

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }

                response = patientDAO.LoadPatientUserInfoByQuivusId(QuivusId);

            }
            catch
            {
                throw;

            }
            return response;
        }

        /// <summary>
        /// Author Muhammad ISmail
        /// Date 27 august 2011
        /// Method Used To load clinic brach Id
        /// </summary>
        /// <param name="ClicniBranchName"></param>
        /// <returns></returns>
        public int LoadClinicBranchIdByClinicBranchName(String ClicniBranchName)
        {
            int response = 0;
            try
            {
                QuivusLogger.Info("LoadClinicBranchIdByClinicBranchName Called PARAM ClicniBranchName::" + ClicniBranchName);

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }

                response = patientDAO.LoadClinicBranchIdByClinicBranchName(ClicniBranchName);
            }
            catch
            {
                throw;

            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami uddin
        /// Date 25/11/2011
        /// This method will search all patient for folder creation
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PatientSearchResultDTO LoadUsersByPracticeId(int clinicBranchID, int pageNo, int pageSize)
        {
            PatientSearchResultDTO searchResult = null;

            bool isDateSearch = false;
            bool isSSNSearch = false;

            string SSN = "" , dashSSN = "";
            int providerId = 0, userTypeId = 0;
            string searchString = "";

            DateTime DOB = new DateTime();

            try
            {
                QuivusLogger.Info("LoadUsersByPracticeId PARAM:: clinicId " + clinicBranchID + " providerId " + providerId + " userTypeId " + userTypeId + " searchString " + searchString + " demandPageNo " + pageNo);

                if (clinicBranchID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                }

                if (pageNo < 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.DEMAND_PAGE_NO_INVALID_ERROR_MSG);
                }
                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }

                isDateSearch = false; //Validations.IsValidSearchDate(searchString, out DOB);

                isSSNSearch = false; //Validations.IsValidateSearchSSN(searchString, out SSN, out dashSSN);

                searchResult = patientDAO.LaodPatientAll(clinicBranchID, providerId, userTypeId, searchString, pageNo, pageSize, isDateSearch, DOB, isSSNSearch, SSN, dashSSN);
            }
            catch
            {
                throw;
            }
            return searchResult;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Create Date 8 Dec 2011
        /// This mthod will return list of clinic room by stationId
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public List<ClinicRoomDTO> LoadClinicRoomByStationId(int stationId)
        {
            List<ClinicRoomDTO> clinicRoomList = null;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling LoadClinicRoomByPatientId PARAM:: stationId " + stationId);

                if (stationId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.STATION_ID_INVALID_ERROR_MSG);
                }

                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }
                clinicRoomList = patientDAO.LoadClinicRoomByStationtId(stationId);
            }
            catch
            {
                throw;
            }
            return clinicRoomList;
        }
    }

}
