using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Utility;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;
using Quivus.Utility.Factory;

using Visit.DataAccess;
using Visit.Business.Validators;

using Quivus.Model.QuickInput;
using Quivus.Model.Visit;
using Quivus.Model.PatientBackground;
using Quivus.Model.QuickInput.Notes;
using Quivus.Model.QuickInput.ComprehensiveHistory.Medical;
using Visit.Business.Validators.QuickInput.PMH.ComprehensiveHistory;
using Quivus.Model.QuickInput.MainQuickInput;
using Visit.Business.BL.Encounter;
using Visit.DataAccess.DAO;
using Quivus.Model.QuickInput.Dictation;
using QuivusUtility.ProjectConstants;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.Followup;
using QuivusModel.User.Patient;
//using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.Recommendation;


namespace Visit.Business.BL.Encounter.Impl
{
    /// <summary>
    /// Author Mansoor
    /// Date 17 Feb 2011   
    /// </summary>
    public class VisitManagerImpl : IVisitManager
    {
        private VisitDAO visitDAO;

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011   
        /// Get soap templates bases on clinic branch id
        /// </summary>
        public List<SoapTemplateTypeDTO> LoadSoapTemplateByClinicBranchId(int clinicBranchId)
        {
            List<SoapTemplateTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadSoapTemplateByClinicBranchId PARAM :: clinicBranchId " + clinicBranchId);

                if (clinicBranchId <= 0)
                {
                  //  throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);

                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG,ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG,
                       ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                   ErrorCodes.SOAP_TEMPLATE_PREFIX, ErrorCodes.OPERATION_LOAD_PREFIX,
                                   ErrorCodes.INVALID_AURGUMENTS_PREFIX)
                                   );

                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadSoapTemplateByClinicBranchId(clinicBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// create visit        
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <returns></returns>
        public int CreateVisit(VisitDTO visitDTO, int managedByUserId)
        {
            int visitId = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling CreateVisit visit :: " + visitDTO + " managedByUserId " + managedByUserId);

                if (VisitValidator.ValidateVisit(visitDTO, managedByUserId, ref sb))
                {
                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }

                    visitDTO.RecGuid = System.Guid.NewGuid(); // create guid

                    visitId = visitDAO.CreateVisit(visitDTO, managedByUserId);
                }
                else
                {
                    //  throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                     ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                 ErrorCodes.VISIT_PREFIX, ErrorCodes.OPERATION_SAVE_PREFIX,
                                 ErrorCodes.INVALID_AURGUMENTS_PREFIX)
                                 );
                }
            }
            catch
            {
                throw;
            }
            return visitId;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// update visit        
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <returns></returns>
        public bool SaveVisit(VisitDTO visitDTO, int managedByUserId)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling SaveVisit visit :: " + visitDTO + " managedByUserId " + managedByUserId);

                if (VisitValidator.ValidateVisit(visitDTO, managedByUserId, ref sb))
                {
                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }

                    result = visitDAO.SaveVisit(visitDTO, managedByUserId);
                }
                else
                {
                    // throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                    ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                ErrorCodes.VISIT_PREFIX, ErrorCodes.OPERATION_UPDATE_PREFIX,
                                ErrorCodes.INVALID_AURGUMENTS_PREFIX)
                                );
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed       
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <returns></returns>
        public bool SignVisit(int VisitId, int SignedByUserID, int SignedByUserTypeId, DateTime OperationDate)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling SignVisit VisitId :: " + VisitId + " SignedByUserID " + SignedByUserID + "SignedByUserTypeId" + SignedByUserTypeId
                    + "OperationDate" + OperationDate);

                if (VisitValidator.ValidateVisitForSign(VisitId, SignedByUserID, SignedByUserTypeId, OperationDate, ref sb))
                {
                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }
                    result = visitDAO.SignVisit(VisitId, SignedByUserID, SignedByUserTypeId, OperationDate);
                }
                else
                {
                    // throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                   ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                               ErrorCodes.VISIT_PREFIX, ErrorCodes.OPERATION_SIGN_PREFIX,
                               ErrorCodes.INVALID_AURGUMENTS_PREFIX)
                               );
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed
        /// </summary>
        /// <param name="patinetId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="showAll"></param>
        /// <returns></returns> 
        public PatientEncounterResult LoadPatientVisitsByPatientIdAndClinicId(int patientId, int clinicBranchId, bool showAll, int UserId, int UserTypeId)
        {
            PatientEncounterResult result = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientVisitsByPatientIdAndClinicId PARAM :: patientId" + patientId + " clinicBranchId " + clinicBranchId + " showAll " + showAll);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                else if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                else if (UserId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                else if (UserTypeId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_TYPE_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                else
                {
                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }
                    result = visitDAO.LoadPatientVisitsByPatientIdAndClinicId(patientId, clinicBranchId, showAll, UserId, UserTypeId);
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return result;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization lot number
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>  
        public List<string> LoadImmunizationLotNumberByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationLotNumberByClinicId PARAM :: clinicBranchId " + clinicBranchId + " searchCriteria " + searchCriteria);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadImmunizationLotNumberByClinicId(searchCriteria, clinicBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization lot number
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>  
        public List<string> LoadImmunizationDoseByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationDoseByClinicId PARAM :: clinicBranchId " + clinicBranchId + " searchCriteria " + searchCriteria);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadImmunizationDoseByClinicId(searchCriteria, clinicBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Reason
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>     
        public List<string> LoadImmunizationRefusalReasonByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationRefusalReasonByClinicId PARAM :: clinicBranchId " + clinicBranchId + " searchCriteria " + searchCriteria);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadImmunizationRefusalReasonByClinicId(searchCriteria, clinicBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Site
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns> 
        public List<string> LoadImmunizationSiteByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationSiteByClinicId PARAM :: clinicBranchId " + clinicBranchId + " searchCriteria " + searchCriteria);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadImmunizationSiteByClinicId(searchCriteria, clinicBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Series
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns> 
        public List<string> LoadImmunizationSeriesByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationSeriesByClinicId PARAM :: clinicBranchId " + clinicBranchId + " searchCriteria " + searchCriteria);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadImmunizationSeriesByClinicId(searchCriteria, clinicBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }





        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns> 
        public bool ManageModuleNotes(List<ModuleNoteDTO> moduleNotes)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManageModuleNotes moduleNotes :: " + moduleNotes);

                if (VisitValidator.ValidateModuleNotes(moduleNotes, ref sb))
                {
                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }

                    result = visitDAO.ManageModuleNotes(moduleNotes);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                        ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.MODULE_NOTES_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.INVALID_AURGUMENTS_PREFIX)
                                    );
                }
            }
            catch
            {
                throw;
            }
            return result;
        }



        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get immunization manufacturer
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns> 
        public List<string> LoadImmunizationManufacturerByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationManufacturerByClinicId PARAM :: clinicBranchId " + clinicBranchId + " searchCriteria " + searchCriteria);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadImmunizationManufacturerByClinicId(searchCriteria, clinicBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get immunization rout
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>                
        public List<string> LoadImmunizationRoutByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationRoutByClinicId PARAM :: clinicBranchId " + clinicBranchId + " searchCriteria " + searchCriteria);

                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadImmunizationRoutByClinicId(searchCriteria, clinicBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// get immunization list of patient        
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>     
        public List<ImmunizationDTO> LoadPatientImmunizationsByPatientId(int patientId, int providerUserId, int userTypeId, int clinicBranchID, List<int> stationIDs, bool isAllSelected)
        {
            List<ImmunizationDTO> ImmunizationList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientImmunizationsByPatientId PARAM :: patientId " + patientId + " providerUserId " + providerUserId + " userTypeId " + userTypeId);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                else if (providerUserId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                else if (userTypeId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_TYPE_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                else if (clinicBranchID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                ImmunizationList = visitDAO.LoadPatientImmunizationsByPatientId(patientId, clinicBranchID, stationIDs, isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ImmunizationList;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 21 Mar 2011
        /// get hpi details for auto complete suggestion box, by type id and clinic branch
        /// </summary>
        /// <param name="hpiTypeId"></param>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>       
        public List<string> LoadHPIDetailAutoCompleteByTypeIdAndClinicId(int hpiTypeId, int clinicBranchId, string searchCriteria)
        {
            List<string> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadHPIDetailAutoCompleteByTypeIdAndClinicId PARAM :: hpiTypeId " + hpiTypeId + " searchCriteria " + searchCriteria + " clinicBranchId " + clinicBranchId);

                if (hpiTypeId < 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.HPI_TYPE_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                // Not in use
                //else if (string.IsNullOrEmpty(searchCriteria))
                //{
                //    throw new InvalidArgumentException(ProjectConstant.SEARCH_STRING_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                //}
                else if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadHPIDetailAutoCompleteByTypeIdAndClinicId(hpiTypeId, clinicBranchId, searchCriteria);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>

        public bool CoSignVisit(int VisitId, int ProviderId, int ProviderUserId, int ProviderTypeId, DateTime OperationDate)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling SignVisit VisitId :: " + VisitId + " ProviderId " + ProviderId
                    + "ProviderUserId" + ProviderUserId + "ProviderTypeId" + ProviderTypeId + "OperationDate" + OperationDate);

                if (VisitValidator.ValidateVisitForCOSign(VisitId, ProviderId, ProviderUserId, ProviderTypeId, OperationDate, ref sb))
                {
                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }
                    result = visitDAO.CoSignVisit(VisitId, ProviderId, ProviderUserId, ProviderTypeId, OperationDate);
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


        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns> 
        public bool ManageModuleDictation(List<ModuleDictationDTO> ModuleDictationDTOList, int ManagedByUserID, int ManagedByUserTypeID, int ClinicBranchID, int VisitId)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManageModuleDictation moduleDictations :: " + ModuleDictationDTOList);

                if (VisitValidator.ValidateVBModuleDictation(ModuleDictationDTOList, ref sb))
                {
                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }

                    result = visitDAO.ManageModuleDictation(ModuleDictationDTOList, ManagedByUserID, ClinicBranchID, VisitId);
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

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 8/4/2011
        /// </summary>
        /// <param name="hpiTypeId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>

        public List<FollowupDataControlTypeDTO> LoadFollowupDataControlType()
        {
            List<FollowupDataControlTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadFollowupDataControlType");

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadFollowupDataControlType();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 08/04/2011
        /// </summary>
        /// <param name="followupDataControlTypeID"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        public List<ServiceTypeDTO> LoadPatientFollowupAutoCompleteByTypeIdAndClinicId(int followupDataControlTypeID, int clinicBranchId)
        {
            List<ServiceTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientFollowupAutoCompleteByTypeIdAndClinicId PARAM :: followupDataControlTypeID " + followupDataControlTypeID + " clinicBranchId " + clinicBranchId);

                if (followupDataControlTypeID < 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.DATACONTROL_TYPE_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                else if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadFollowUpAutoCompleteByTypeIdAndClinicId(followupDataControlTypeID, clinicBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

         /// <summary>
        /// Author Ismail
        /// Date 08/25/2011
        /// </summary>
       
        /// <returns></returns>
        public List<PatientLastSeenDTO> LoadPatientLastSeenByPatientId(int PatientId)
        {
            List<PatientLastSeenDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientLastSeenByPatientId PARAM :: PatientId " + PatientId );

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (visitDAO == null)
                {
                    visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                }

                list = visitDAO.LoadPatientLastSeenByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }
    }
}
