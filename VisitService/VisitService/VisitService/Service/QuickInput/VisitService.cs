using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quivus.Model.QuickInput.Notes;

using Quivus.Utility.CustomException;
using Quivus.Utility.Logger;
using QuivusUtility.Utilities;
using Quivus.Model.QuickInput;
using Quivus.Model.Visit;
using Quivus.Model.TypeDTOs;
using Visit.Business.Common;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.MedicalHistory;
using Quivus.Model.QuickInput.ComprehensiveHistory.Medical;
using Quivus.Model.QuickInput.MainQuickInput;
using Visit.Business.BL.Encounter.Impl;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.FamilyHistory;
using Visit.Business.BL.QuickInput.Impl;
using Visit.Business.BL.QuickInput;
using Quivus.Model.QuickInput.ComprehensiveHistory.Family;
using Quivus.Model.QuickInput.Vitals;
using Quivus.Model.QuickInput.Labs;
using System.ServiceModel;
using Quivus.Model.QuickInput.ROS;
using Quivus.Model.QuickInput.ComprehensiveHistory.Social;
using Quivus.Model.QuickInput.Physical;
using Quivus.Model.QuickInput.Recommendation;
using Quivus.Model.User.Patient;
using Quivus.Model.QuickInput.Procedure;
using Quivus.Model.QuickInput.Medication;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp;
using Quivus.Model.QuickInput.Dictation;
using Quivus.Model.QuickInput.HPI;
using Quivus.Model.QuickInput.Diagnosis;
using Quivus.Model.QuickInput.FollowUp;
using QuivusUtility.ProjectConstants;
using QuivusUtility.CustomException;
using Quivus.Model.QuickInput.Prescription;
using Visit.Business;

namespace VisitService.Service
{
    public partial class VisitService 
    {

        IQuickInputManager QuickInputManager = null;
        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// manage module notes, insert/ update/ delete
        /// </summary>
        /// <param name="moduleNotes"></param>
        /// <returns></returns>        
        public bool ManageModuleNotes(List<ModuleNoteDTO> moduleNotes)
        {
            bool result = false;

            try
            {
                if (VisitImpl == null)
                {
                    VisitImpl = new VisitManagerImpl();
                }

                result = VisitImpl.ManageModuleNotes(moduleNotes);
            }
            catch (RecordNotInsertException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Message, exp.Code);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                // Compose Genral Error Code For Exception
                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                      ErrorCodes.MODULE_NOTES_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                      ErrorCodes.GENERAL_ERROR_CODE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
            }
            return result;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// save/update patient immunization with visit
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>      
        public bool ManagePatientImmunization(List<ImmunizationDTO> immunizationList, int cbId, int patientId)
        {
            bool result = false;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }


                result = QuickInputManager.ManagePatientImmunization(immunizationList,cbId, patientId);
            }
            catch (RecordNotInsertException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Message, exp.Code);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                       ErrorCodes.MODULE_NOTES_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                       ErrorCodes.GENERAL_ERROR_CODE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
                //  HandleFault.HandleFautException(exp);
            }
            return result;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// create visit
        /// </summary>
        /// <returns></returns>      
        public int CreateVisit(VisitDTO visitDTO, int managedByUserId)
        {
            int visitId = 0;
            try
            {
                if (VisitImpl == null)
                {
                    VisitImpl = new VisitManagerImpl();
                }

                visitId = VisitImpl.CreateVisit(visitDTO, managedByUserId);
            }
            catch (RecordNotInsertException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Message, exp.Code);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);

                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                       ErrorCodes.VISIT_PREFIX, ErrorCodes.OPERATION_SAVE_PREFIX,
                                       ErrorCodes.GENERAL_ERROR_CODE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
                //  HandleFault.HandleFautException(exp);
            }
            return visitId;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// update visit       
        /// </summary>
        /// <returns></returns>      
        public bool SaveVisit(VisitDTO visitDTO, int managedByUserId)
        {
            bool result = false;
            try
            {
                if (VisitImpl == null)
                {
                    VisitImpl = new VisitManagerImpl();
                }

                result = VisitImpl.SaveVisit(visitDTO, managedByUserId);
            }
            catch (RecordNotInsertException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Message, exp.Code);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);

                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                       ErrorCodes.VISIT_PREFIX, ErrorCodes.OPERATION_UPDATE_PREFIX,
                                       ErrorCodes.GENERAL_ERROR_CODE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
                //  HandleFault.HandleFautException(exp);
            }
            return result;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>        
        public bool SignVisit(int VisitId, int SignedByUserID, int SignedByUserTypeId, DateTime OperationDate)
        {
            bool result = false;
            try
            {
                if (VisitImpl == null)
                {
                    VisitImpl = new VisitManagerImpl();
                }

                result = VisitImpl.SignVisit(VisitId, SignedByUserID, SignedByUserTypeId, OperationDate);
            }
            catch (RecordNotInsertException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Message, exp.Code);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);

                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                       ErrorCodes.VISIT_PREFIX, ErrorCodes.OPERATION_SIGN_PREFIX,
                                       ErrorCodes.GENERAL_ERROR_CODE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
                //  HandleFault.HandleFautException(exp);
            }
            return result;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the template types
        /// </summary>
        /// <returns></returns>        
        public List<SoapTemplateTypeDTO> LoadSoapTemplateByClinicBranchId(int clinicBranchId)
        {
            List<SoapTemplateTypeDTO> response = null;
            try
            {
                if (VisitImpl == null)
                {
                    VisitImpl = new VisitManagerImpl();
                }

                response = VisitImpl.LoadSoapTemplateByClinicBranchId(clinicBranchId);
            }

            catch (DTOConversionException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, String.Empty, exp.Code);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);

                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                       ErrorCodes.SOAP_TEMPLATE_PREFIX, ErrorCodes.OPERATION_LOAD_PREFIX,
                                       ErrorCodes.GENERAL_ERROR_CODE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
                //  HandleFault.HandleFautException(exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the encounter types
        /// </summary>
        /// <returns></returns>
        public List<EncounterTypeDTO> LoadEncounterTypes()
        {
            List<EncounterTypeDTO> response = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                response = objVisitSDManager.LoadEncounterTypes();
            }
            catch (DTOConversionException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, String.Empty, exp.Code);
            }           
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);

                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                       ErrorCodes.ENCOUNTER_TYPES_PREFIX, ErrorCodes.OPERATION_LOAD_PREFIX,
                                       ErrorCodes.GENERAL_ERROR_CODE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
                //  HandleFault.HandleFautException(exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used to load Medical History Types Used 
        /// Under Comprehensive History Section
        /// </summary>
        /// <returns></returns>
        public List<MedicalHistoryTypeDTO> LoadMedicalHistoryTypes()
        {
            List<MedicalHistoryTypeDTO> MedicalHistoryList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                MedicalHistoryList = objVisitSDManager.LoadMedicalHistoryTypes();

                QuivusLogger.Info("LoadMedicalHistoryTypes Return List ::" + MedicalHistoryList);
            }
            catch (DTOConversionException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, String.Empty, exp.Code);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);

                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                       ErrorCodes.MEDICAL_HISTORY_TYPES_PREFIX, ErrorCodes.OPERATION_LOAD_PREFIX,
                                       ErrorCodes.GENERAL_ERROR_CODE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
                //  HandleFault.HandleFautException(exp);
            }
            return MedicalHistoryList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used to Insert/Update Patient Medical History
        /// </summary>
        /// <returns></returns>
        public int ManagePatientMedicalHistory(MedicalHistoryDTO PateintMedicalHistoryObject, int cbID, int patId)
        {
            int Result = 0;
            try
            {
              //  M to remove error
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Result = QuickInputManager.ManagePatientMedicalHistory(PateintMedicalHistoryObject,  cbID,  patId);

                QuivusLogger.Info("ManagePatientMedicalHistory Return Result ::" + Result);
            }
            catch (ParameterPopulatorException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Detail, exp.Code);
            }
            catch (RecordNotInsertException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Message, exp.Code);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                // Compose Genral Error Code For Exception
                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                      ErrorCodes.MEDICAL_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                      ErrorCodes.GENERAL_ERROR_CODE_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
            }
            return Result;
        }

        

        /// <summary>
        /// Author Mansoor
        /// Date 11 Mar 2011
        /// create/ save visit and save the relative infor like immunization and chronic medication
        /// used only in case of operation performed in Quick Input Container
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="immunizationList"></param>
        /// <param name="chronicMedicationList"></param>
        /// <returns></returns>

        public bool ManageQuickInputData(QuickInputDTO QuickInput, int managedByUserId, int UserTypeId)
        {
            bool Status = false;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.ManageQuickInputData(QuickInput, managedByUserId, UserTypeId);
            }
            catch (ParameterPopulatorException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Detail, exp.Code);
            }
            catch (TableConversionException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.Message, exp.Code);
            }
            catch (RecordNotInsertException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Message, exp.Code);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.QUICK_INPUT_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.GENERAL_ERROR_CODE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
               // HandleFault.HandleFautException(exp);
            }
            return Status;
        }

         /// <summary>
        /// Author Ismail
        /// Date 12 july 2011
        /// create/ save visit and save the relative infor like immunization and chronic medication
        /// used only in case of operation performed in Quick Input Container
        /// This is overloaded method it save PMH,Physical & Assessment One By One In Last call front end make IsFinal True to
        /// Indicate that this is last call & on last call we will update visit status & calculate visit score
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="immunizationList"></param>
        /// <param name="chronicMedicationList"></param>
        /// <returns></returns>
        public bool ManageQuickInputData(QuickInputDTO QuickInput, int managedByUserId, int UserTypeId, bool IsFinal)
        {
            bool Status = false;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.ManageQuickInputData(QuickInput, managedByUserId, UserTypeId,IsFinal);
            }
            catch (ParameterPopulatorException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Detail, exp.Code);
            }
            catch (TableConversionException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Message, exp.Code);
            }
            catch (RecordNotInsertException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Message, exp.Code);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.QUICK_INPUT_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.GENERAL_ERROR_CODE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
                // HandleFault.HandleFautException(exp);
            }
            return Status;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to load Family History Types Used 
        /// Under Comprehensive History Section
        /// </summary>
        /// <returns></returns>
        public List<FamilyHistoryTypeDTO> LoadFamilyHistoryTypes()
        {
            List<FamilyHistoryTypeDTO> HistoryList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                HistoryList = objVisitSDManager.LoadFamilyHistoryTypes();

                QuivusLogger.Info("LoadMedicalHistoryTypes Return List ::" + HistoryList);
            }
            catch (DTOConversionException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, String.Empty, exp.Code);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);

                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                       ErrorCodes.FAMILY_HISTORY_TYPES_PREFIX, ErrorCodes.OPERATION_LOAD_PREFIX,
                                       ErrorCodes.GENERAL_ERROR_CODE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
                //  HandleFault.HandleFautException(exp);
            }
            return HistoryList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to Insert/Update Patient Family History
        /// </summary>
        /// <returns></returns>
        public int ManagePatientFamilyHistory(FamilyHistoryDTO PateintFamilyHistoryObject,int UserTypeId,int ClinicBranchId,int patID)
        {
            int Result = 0;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Result = QuickInputManager.ManagePatientFamilyHistory(PateintFamilyHistoryObject,UserTypeId,ClinicBranchId,patID);

                QuivusLogger.Info("ManagePatientFamilyHistory Return Result ::" + Result);
            }
            catch (ParameterPopulatorException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Detail, exp.Code);
            }
            catch (RecordNotInsertException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Message, exp.Code);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                // Compose Genral Error Code For Exception
                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                      ErrorCodes.FAMILY_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                      ErrorCodes.GENERAL_ERROR_CODE_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
            }
            return Result;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to Insert/Update Patient Vitals
        /// </summary>
        /// <returns></returns>

        public int ManagePatientVitals(VitalDTO VitalObject)
        {
            int Result = 0;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Result = QuickInputManager.ManagePatientVitals(VitalObject);

                QuivusLogger.Info("ManagePatientVitals Return Result ::" + Result);
            }
            catch (ParameterPopulatorException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Detail, exp.Code);
            }
            catch (RecordNotInsertException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Message, exp.Code);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                // Compose Genral Error Code For Exception
                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                      ErrorCodes.VITALS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                      ErrorCodes.GENERAL_ERROR_CODE_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
            }
            return Result;
        }


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to Insert/Update Patient Vitals
        /// </summary>
        /// <returns></returns>
        public int ManagePatientLabs(LabDTO LabObject)
        {
            int Result = 0;
            try
            {
                QuivusLogger.Info("ManagePatientLabs 1");
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Result = QuickInputManager.ManagePatientLabs(LabObject);

                QuivusLogger.Info("ManagePatientLabs Return Result ::" + Result);
            }
            catch (ParameterPopulatorException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Detail, exp.Code);
            }
            catch (RecordNotInsertException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Message, exp.Code);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                // Compose Genral Error Code For Exception
                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                      ErrorCodes.LABS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                      ErrorCodes.GENERAL_ERROR_CODE_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
            }
            return Result;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 19 Feb 2011
        /// save/update patient ROS
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        public bool ManagePatientROS(List<ROSDTO> ROSList)
        {
            bool Status = false;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.ManagePatientROS(ROSList);
            }
            catch (ParameterPopulatorException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Detail, exp.Code);
            }
            catch (RecordNotInsertException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Message, exp.Code);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                // Compose Genral Error Code For Exception
                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                      ErrorCodes.ROS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                      ErrorCodes.GENERAL_ERROR_CODE_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
            }
            return Status;
        }



        /// <summary>
        /// Author Mansoor
        /// Date 19 Feb 2011
        /// save/updateSocialHistory Addiction
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        public int ManageSocialHistoryAddiction(SocialHistoryAddictionDTO SocialHistoryAddiction, int cbID, int patID)
        {
            int Status = 0;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.ManageSocialHistoryAddiction(SocialHistoryAddiction,cbID,patID);
            }
            catch (ParameterPopulatorException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Detail, exp.Code);
            }
            catch (RecordNotInsertException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Message, exp.Code);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                // Compose Genral Error Code For Exception
                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                      ErrorCodes.ROS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                      ErrorCodes.GENERAL_ERROR_CODE_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
            }
            return Status;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 19 Feb 2011
        /// save/updateSocialHistory Addiction
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        public bool DeleteSocialHistoryAddiction(SocialHistoryAddictionDTO DeletedObject)
        {
            bool Status = false;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.DeleteSocialHistoryAddiction(DeletedObject);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }

        /// <summary>
        /// Author Ismail
        /// Date 19 Feb 2011
        /// Delete SocialHistory 
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        public bool DeleteSocialHistory(SocialHistoryDTO DeletedObject)
        {
            bool Status = false;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.DeleteSocialHistory(DeletedObject);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }

        

        /// <summary>
        /// Author Ismail
        /// Date 19 Feb 2011
        /// save/updateSocial History 
        /// </summary>
        /// <param name="SocialHistoryObject"></param>
        /// <returns></returns>
        public int ManagePatientSocialHistory(SocialHistoryDTO SocialHistoryObject, int cbID, int patId)
        {
            int Status = 0;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.ManagePatientSocialHistory(SocialHistoryObject,cbID,  patId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }


        /// <summary>
        /// Author Ismail
        /// Date 22 March 2011
        /// save/update patient ROS
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        public bool ManagePatientPhysical(List<PhysicalsDTO> PhysicalList)
        {
            bool Status = false;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.ManagePatientPhysical(PhysicalList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }

        /// <summary>
        /// Author Ismail
        /// Date 22 March 2011
        /// save/update patient Recommendation
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        public bool ManagePatientRecommendation(List<PatientRecommendationDTO> RecommendationList)
        {
            bool Status = false;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.ManagePatientRecommendation(RecommendationList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }

        /// <summary>
        /// Author Ismail
        /// Date 26 March 2011
        /// save patient allergies
        /// </summary>
        /// <param name="PatientAllergyDTO"></param>
        /// <returns></returns>
        public bool ManagePatientAllergy(AllergyDTO AllergyObj)
        {
            bool Status = false;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.ManagePatientAllergy(AllergyObj);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }

        /// <summary>
        /// Author Ismail
        /// Date 26 March 2011
        /// save patient allergies List
        /// </summary>
        /// <param name="Allergylist"></param>
        /// <returns></returns>
        public bool ManagePatientAllergies(List<AllergyDTO> Allergylist)
        {
            bool Status = false;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.ManagePatientAllergies(Allergylist);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }


        /// <summary>
        /// Author Ismail
        /// Date 26 March 2011
        /// save patient chrnoc medication
        /// </summary>
        /// <param name="ChronicMedicationObj"></param>
        /// <returns></returns>
        public bool ManagePatientChronicMedication(MedicationPrescribedDTO ChronicMedicationObj)
        {
            bool Status = false;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.ManagePatientChronicMedication(ChronicMedicationObj);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }


        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// save/update patient procedure
        /// </summary>
        /// <param name="ProcedureList"></param>
        /// <returns></returns>
        public bool ManagePatientProcedure(List<ProcedureDTO> InputList)
        {
            bool Status = false;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.ManagePatientProcedure(InputList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }


        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// save/update patient in house labs
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public bool ManagePatientInHouseLabs(List<InHouseLabDTO> InputList)
        {
            bool Status = false;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.ManagePatientInHouseLabs(InputList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient Diagnosis data
        /// </summary>
        /// <returns></returns>
        public bool ManagePatientDiagnosis(List<PatientDiagnosisDTO> diagnosisList, int cbID, int patID)
        {
            bool Status = false;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.ManagePatientDiagnosis(diagnosisList, cbID, patID);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 20 July 2011
        /// This Method Will be used to Insert/Update/Delete Patient Diagnosis data
        /// </summary>
        /// <returns></returns>
        public bool ManagePatientDiagnosisByRcopia(List<PatientDiagnosisDTO> diagnosisList)
        {
            bool Status = false;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.ManagePatientDiagnosisByRcopia(diagnosisList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }


        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient Diagnosis data
        /// </summary>
        /// <returns></returns>
        public bool ManagePatientFollowUp(PatientFollowUpDTO patientFollowUp)
        {
            bool Status = false;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.ManagePatientFollowUp(patientFollowUp);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }

        /// <summary>
        /// Author Baber Waqas at 1/24/2012
        /// </summary>
        /// <param name="customICDCodeDTO"></param>
        /// <param name="managedByUserIDForVisit"></param>
        /// <param name="clinicBranchID"></param>
        /// <returns></returns>
        public bool ManageCustomICDCode(ICDCodeDTO customICDCodeDTO, int managedByUserIDForVisit, int clinicBranchID)
        {
            bool Status = false;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.ManageCustomICDCode(customICDCodeDTO, managedByUserIDForVisit, clinicBranchID);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }

        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// save/update patient in house labs results
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public bool ManagePatientInHouseLabResults(int PatientInHouseLabTestID, String TestResult, byte[] ResultAttachment, int ModifiedBy, DateTime ModifiedAt)
        {
            bool Status = false;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.ManagePatientInHouseLabResults(PatientInHouseLabTestID, TestResult, ResultAttachment, ModifiedBy, ModifiedAt);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }


        /// <summary>
        /// Author Ismail
        /// Date 07 April 2011
        /// save/update patient medications
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public int ManagePatientMedication(MedicationDTO InputObj)
        {
            int Status = 0;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.ManagePatientMedication(InputObj);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }

        /// <summary>
        /// Author Ismail
        /// Date 07 April 2011
        /// Delete patient medications
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
       public bool DeletePatientMedication(int PatientMedicationID, DateTime ModifiedAt)
        {
            bool Status = false;
            try
            {
                if (QuickInputManager == null)
                {
                    QuickInputManager = new QuickInputManagerImpl();
                }

                Status = QuickInputManager.DeletePatientMedication(PatientMedicationID, ModifiedAt);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }

       /// <summary>
       /// Atta 
       /// Date 14 April 2011
       /// </summary>
       /// <returns></returns>
       public List<InstructionAgeTypeDTO> LoadFollowUpInstructionAgeTypes()
       {
           List<InstructionAgeTypeDTO> TypeList = null;
           try
           {
               if (objVisitSDManager == null)
               {
                   objVisitSDManager = new VisitStaticDataManager();
               }

               TypeList = objVisitSDManager.LoadFollowUpInstructionAgeTypes();

               QuivusLogger.Info("LoadFollowUpInstructionAgeTypes Return List ::" + TypeList);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
           }
           return TypeList;
       }


       /// <summary>
       /// Atta 
       /// Date 14 April 2011
       /// </summary>
       /// <returns></returns>
       public List<InstructionDurationTypeDTO> LoadFollowUpInstructionDurationTypes()
       {
           List<InstructionDurationTypeDTO> TypeList = null;
           try
           {
               if (objVisitSDManager == null)
               {
                   objVisitSDManager = new VisitStaticDataManager();
               }

               TypeList = objVisitSDManager.LoadFollowUpInstructionDurationTypes();

               QuivusLogger.Info("LoadFollowUpInstructionDurationTypes Return List ::" + TypeList);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
           }
           return TypeList;
       }



       /// <summary>
       /// Atta 
       /// Date 14 April 2011
       /// </summary>
       /// <returns></returns>
       public List<ServiceTypeDTO> LoadFollowUpServiceTypes(int ClinicBranchID)
       {
           List<ServiceTypeDTO> TypeList = null;
           try
           {
               if (objVisitSDManager == null)
               {
                   objVisitSDManager = new VisitStaticDataManager();
               }

               TypeList = objVisitSDManager.LoadFollowUpServiceTypes(ClinicBranchID);

               QuivusLogger.Info("LoadFollowUpInstructionDurationTypes Return List ::" + TypeList);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
           }
           return TypeList;
       }

       /// <summary>
       /// Atta 
       /// Date 14 April 2011
       /// </summary>
       /// <returns></returns>
       public List<InstructionAgeValueTypeDTO> LoadFollowUpInstructionAgeValueTypes()
       {
           List<InstructionAgeValueTypeDTO> TypeList = null;
           try
           {
               if (objVisitSDManager == null)
               {
                   objVisitSDManager = new VisitStaticDataManager();
               }

               TypeList = objVisitSDManager.LoadFollowUpInstructionAgeValueTypes();

               QuivusLogger.Info("LoadFollowUpInstructionAgeValueTypes Return List ::" + TypeList);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
           }
           return TypeList;
       }


       public bool ManageModuleDictation(List<ModuleDictationDTO> ModuleDictationDTOList, int ManagedByUserID, int ManagedByUserTypeID, int ClinicBranchID,int VisitId)
       {
           bool result = false;

           try
           {
               if (VisitImpl == null)
               {
                   VisitImpl = new VisitManagerImpl();
               }

               result = VisitImpl.ManageModuleDictation(ModuleDictationDTOList, ManagedByUserID, ManagedByUserTypeID, ClinicBranchID, VisitId);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
           }
           return result;
       }

       /// <summary>
       /// Author Mansoor 
       /// Date 21 Mar 2011
       /// This Method Will be used to Insert/Update/Delete Patient HPI data
       /// </summary>
       /// <returns></returns>
       public bool ManagePatientHPI(List<HPI_CCDTO> hpiList)
       {
           bool Status = false;
           try
           {
               if (QuickInputManager == null)
               {
                   QuickInputManager = new QuickInputManagerImpl();
               }

               Status = QuickInputManager.ManagePatientHPI(hpiList);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
           }
           return Status;
       }

       /// <summary>
       /// Author Mansoor 
       /// Date 22 Mar 2011
       /// This Method Will be used to Insert/Update Patient follow up instructions
       /// </summary>
       /// <returns></returns>
       public bool ManagePatientFollowupInstruction(List<PatientFollowupInstructionDTO> InputList)
       {
           bool Status = false;
           try
           {
               if (QuickInputManager == null)
               {
                   QuickInputManager = new QuickInputManagerImpl();
               }

               Status = QuickInputManager.ManagePatientFollowupInstruction(InputList);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
           }
           return Status;
       }

       /// <summary>
       /// Author Ismail
       /// Date 21 July 2011
       /// save/update patient medications
       /// </summary>
       /// <param name="InputList"></param>
       /// <returns></returns>
       public bool ManagePatientMedicationList(List<MedicationDTO> MedicationList)
       {
           bool Status = false;
           try
           {
               if (QuickInputManager == null)
               {
                   QuickInputManager = new QuickInputManagerImpl();
               }

               Status = QuickInputManager.ManagePatientMedication(MedicationList);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
           }
           return Status;
       }


       public bool ManagePatientPrescriptionMedication(List<PrescriptionMedicationDTO> PrescriptionMedicationlist)
       {
           bool Status = false;
           try
           {
               if (QuickInputManager == null)
               {
                   QuickInputManager = new QuickInputManagerImpl();
               }

               Status = QuickInputManager.ManagePatientPrescriptionMedication(PrescriptionMedicationlist);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
           }
           return Status;
       }

       /// <summary>
       /// Author Muhammad Ismail 
       /// Date 07 October 2011
       /// This Method Will be used to Delete Patient Family History
       /// </summary>
       /// <returns></returns>
       public bool DeleteFamilyHistory(FamilyHistoryDTO DeletedObject)
       {
           bool Status = false;
           try
           {
               if (QuickInputManager == null)
               {
                   QuickInputManager = new QuickInputManagerImpl();
               }

               Status = QuickInputManager.DeleteFamilyHistory(DeletedObject);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
           }
           return Status;
       }

       /// <summary>
       /// Author Muhammad Ismail 
       /// Date 07 October 2011
       /// This Method Will be used to Delete Patient medical History
       /// PatientMedicalHistory_Delete
       /// </summary>
       /// <returns></returns>

       public bool DeleteMedicalHistory(MedicalHistoryDTO DeletedObject)
       {
           bool Status = false;
           try
           {
               if (QuickInputManager == null)
               {
                   QuickInputManager = new QuickInputManagerImpl();
               }

               Status = QuickInputManager.DeleteMedicalHistory(DeletedObject);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
           }
           return Status;
       }

    }
}