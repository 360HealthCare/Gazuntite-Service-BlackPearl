using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Utility;
using Quivus.Model.Clinic;
using Quivus.Model.TypeDTOs;
using Quivus.Model.Visit;
using Quivus.Model.QuickInput;
using Quivus.Model.QuickInput.Notes;
using Quivus.Model.QuickInput.MainQuickInput;
using QuivusUtility.ProjectConstants;
using Quivus.Utility.Logger;
using Quivus.Model.QuickInput.Dictation;

namespace Visit.Business.Validators
{
    /// <summary>
    /// Author Mansoor
    /// Date 17 Feb 2011
    /// methods to Valiadate the visit related info
    /// </summary>

    public class VisitValidator : QBaseValidator
    {
        public static bool ValidateVisit(VisitDTO visitDTO, int managedByUserId, ref StringBuilder sb)
        {
            bool flag = true;

            //M TODO: implement it           

            if (visitDTO != null)
            {
                flag = ValidateUserId(managedByUserId, sb, flag);

                flag = ValidatePatientId(visitDTO.PatientId, sb, flag);

                flag = ValidateClinic(visitDTO.ClinicBranch, sb, flag);

                flag = ValidateDate(visitDTO.VisitDate, sb, flag);

                //not in use
                //flag = ValidateRecordStatus(visitDTO.RecordStatus, sb, flag);

                flag = ValidateUserId(visitDTO.UserId, sb, flag);

                flag = ValidateUserType(visitDTO.UserType, sb, flag);

                flag = ValidateVisitStatus(visitDTO.VisitStatus, sb, flag);

                if (visitDTO.VisitId > 0)
                {
                    flag = ValidateKeyId(visitDTO.EncounterTypeId, sb, flag, ProjectConstant.INVALID_ENCOUNTER_TYPE_MSG);
                    flag = ValidateTemplateId(visitDTO.TemplateId, sb, flag);
                }

                if (visitDTO.OwnerId > 0)
                {
                    if (visitDTO.OwnerUserType != null && visitDTO.OwnerUserType.UserTypeId <= 0)
                    {                    
                        sb.Append("\n" + ErrorMessages.VISIT_OWNER_TYPE_ID_INVALID_ERROR_MSG);
                        flag = false;
                    }
                }
                //M flag = ValidateOwnerId(visitDTO.OwnerId, sb, flag);

                // flag = ValidateTemplateId(visitDTO.TemplateId, sb, flag);

                //not in use
                //  flag = ValidateEncounterTypeId(visitDTO.EncounterTypeId, sb, flag);
            }
            else
            {
                sb.Append("\n" + ErrorMessages.VISIT_DTO_INVALID_ERROR_MSG);
                flag = false;
            }

            //flag = ValidateKeyId(visitDTO.CreatedStationID, sb, flag, ProjectConstant.INVALID_CREATED_STATION_ID);
            return flag;
        }

        public static bool ValidateVisitForSign(int VisitId, int SignedByUserID, int SignedByUserTypeId, DateTime OperationDate, ref StringBuilder sb)
        {
            bool flag = true;

            //M TODO: implement it           


            flag = ValidateUserId(SignedByUserID, sb, flag);

            //flag = ValidateRecordStatus(visitDTO.RecordStatus, sb, flag);

            flag = ValidateUserId(SignedByUserID, sb, flag);

            flag = ValidateKeyId(SignedByUserTypeId, sb, flag, ProjectConstant.USER_TYPE_INVALID_ERROR_MSG);

            flag = ValidateDate(OperationDate, sb, flag);

            return flag;
        }

        public static bool ValidateImmunization(List<ImmunizationDTO> immunizationList, int managedByUserId, ref StringBuilder sb)
        {
            bool flag = true;

            //M TODO: implement it           

            if (immunizationList != null)
            {
                foreach (ImmunizationDTO obj in immunizationList)
                {
                    if (obj != null)
                    {
                        flag = ValidateImmunizationType(obj.ImmunizationType, sb, flag);

                        //    flag = ValidateVisitId(obj.VisitId, sb, flag);

                        //   flag = ValidateUserId(obj.ManagedBy, sb, flag);
                    }
                    else
                    {
                        sb.Append("\n" + ErrorMessages.MODULE_NOTE_DTO_INVALID_ERROR_MSG);
                        flag = false;
                    }
                    return flag;
                }
            }
            else
            {
                sb.Append("\n" + ErrorMessages.IMMUNIZATION_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        public static bool ValidateVisitComplete(VisitDTO visitDTO, List<ImmunizationDTO> immunizationList, List<MedicationPrescribedDTO> chronicMedicationList, int managedByUserId, ref StringBuilder sb)
        {
            //TODO
            throw new NotImplementedException();
        }

        public static bool ValidateModuleNotes(List<ModuleNoteDTO> moduleNotes, ref StringBuilder sb)
        {
            bool flag = true;

            if (moduleNotes != null)
            {
                foreach (ModuleNoteDTO obj in moduleNotes)
                {
                    if (obj != null)
                    {
                        flag = ValidateModuleType(obj.ModuleTypeId, sb, flag);

                        //    flag = ValidateVisitId(obj.VisitID, sb, flag);

                        // flag = ValidateNote(obj.Note, sb, flag); //validation is removed on this field Requested By Waqas Baber On Tuesday 24 May 2011 8:24 Pm Before Giving  Release.... Done By Ismail
                        
                        //  flag = ValidateUserId(obj.ManagedByUserId, sb, flag);
                    }
                    else
                    {
                        sb.Append("\n" + ErrorMessages.MODULE_NOTE_DTO_INVALID_ERROR_MSG);
                        flag = false;
                    }
                }
            }
            else
            {
                sb.Append("\n" + ErrorMessages.MODULE_NOTE_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateUserId(int userId, StringBuilder sb, bool flag)
        {
            if (userId <= 0)
            {
                sb.Append("\n" + ErrorMessages.USER_ID_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateUserTYpeId(int userId, StringBuilder sb, bool flag)
        {
            if (userId <= 0)
            {
                sb.Append("\n" + ErrorMessages.USER_TYPE_ID_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateUserTYpeId(int userId, StringBuilder sb, bool flag, String ErrorMSg)
        {
            if (userId <= 0)
            {
                sb.Append("\n" + ErrorMSg);
                flag = false;
            }
            return flag;
        }

        private static bool ValidatePatientId(int patientId, StringBuilder sb, bool flag)
        {
            if (patientId <= 0)
            {
                sb.Append("\n" + ErrorMessages.PATIENT_ID_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateClinic(ClinicBranchDTO clinic, StringBuilder sb, bool flag)
        {
            if (clinic == null || clinic.ClinicBranchIdSeq <= 0)
            {
                sb.Append("\n" + ErrorMessages.CLINIC_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }



        private static bool ValidateRecordStatus(string recordStatus, StringBuilder sb, bool flag)
        {
            if (string.IsNullOrEmpty(recordStatus))
            {
                sb.Append("\n" + ErrorMessages.VISIT_RECORD_STATUS_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateUserType(UserTypeDTO userType, StringBuilder sb, bool flag)
        {
            if (userType == null || userType.UserTypeId <= 0)
            {
                sb.Append("\n" + ErrorMessages.USER_TYPE_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateVisitStatus(string visitStatus, StringBuilder sb, bool flag)
        {
            if (string.IsNullOrEmpty(visitStatus))
            {
                sb.Append("\n" + ErrorMessages.VISIT_STATUS_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateOwnerId(int ownerId, StringBuilder sb, bool flag)
        {
            if (ownerId <= 0)
            {
                sb.Append("\n" + ErrorMessages.VISIT_OWNER_ID_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateTemplateId(int templateId, StringBuilder sb, bool flag)
        {
            if (templateId <= 0)
            {
                sb.Append("\n" + ErrorMessages.VISIT_TEMPLATE_ID_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateEncounterTypeId(int encounterTypeId, StringBuilder sb, bool flag)
        {
            if (encounterTypeId <= 0)
            {
                sb.Append("\n" + ErrorMessages.VISIT_TYPE_ID_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateModuleType(int moduleTypeId, StringBuilder sb, bool flag)
        {
            if (moduleTypeId <= 0)
            {
                sb.Append("\n" + ErrorMessages.MODULE_TYPE_ID_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateVisitId(int visitId, StringBuilder sb, bool flag)
        {
            if (visitId <= 0)
            {
                sb.Append("\n" + ErrorMessages.VISIT_ID_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateNote(string note, StringBuilder sb, bool flag)
        {
            if (string.IsNullOrEmpty(note))
            {
                sb.Append("\n" + ErrorMessages.NOTE_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateImmunizationType(ImmunizationTypeDTO ImmunizationType, StringBuilder sb, bool flag)
        {
            if (ImmunizationType == null || ImmunizationType.ImmunizationTypeId <= 0)
            {
                sb.Append("\n" + ErrorMessages.IMMUNIZATION_TYPE_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 21 March 2011
        /// Method Will be used to verfiy quick input basic stuff rest will get validate on each method call :)
        /// </summary>
        /// <param name="QuickInput"></param>
        /// <param name="managedByUserId"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        internal static bool ValidateVisitComplete(QuickInputDTO QuickInput, int managedByUserId, ref StringBuilder sb, bool flag)
        {
            QuivusLogger.Info("Start Validating Quick Input");

            if (QuickInput != null)
            {
                if (QuickInput.PMH == null && QuickInput.Physical == null && QuickInput.Assessment == null &&
                    QuickInput.ModuleDictations == null && QuickInput.ModuleNotes == null)
                {
                    sb.Append("\n" + ErrorMessages.PATIENT_QUICKINPUT_DTO_EMPTY_ERROR_MSG);
                    flag = false;
                }
                else
                {
                    flag = ValidateKeyId(QuickInput.VisitId, sb, flag, ErrorMessages.INVALID_VISIT_ID_ERROR_MSG);
                    flag = ValidateKeyId(QuickInput.PatientId, sb, flag, ErrorMessages.INVALID_PATIENT_ID_ERROR_MSG);
                    flag = ValidateDate(QuickInput.ActionDate, sb, flag, ErrorMessages.INVALID_ACTION_DATE_ERROR_MSG);

                    flag = ValidateKeyId(QuickInput.ClinicBranchId, sb, flag, ErrorMessages.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                }

            }
            else
            {
                sb.Append("\n" + ErrorMessages.PATIENT_QUICKINPUT_DTO_INVALID_ERROR_MSG);
                flag = false;
            }

            QuivusLogger.Info("End  Validating Quick Input with Status :: " + flag);
            return flag;
        }

        internal static bool ValidateVisitComplete(QuickInputDTO QuickInput, int managedByUserId, int VisitId, int PatientId, DateTime ModificationDate,
            ref StringBuilder sb, bool flag)
        {
            QuivusLogger.Info("Start Validating Quick Input");

            if (QuickInput != null)
            {
                if (QuickInput.PMH == null && QuickInput.Physical == null && QuickInput.Assessment == null &&
                    QuickInput.ModuleDictations == null && QuickInput.ModuleNotes == null)
                {
                    sb.Append("\n" + ErrorMessages.PATIENT_QUICKINPUT_DTO_EMPTY_ERROR_MSG);
                    flag = false;
                }
                else
                {
                    flag = ValidateKeyId(QuickInput.VisitId, sb, flag, ErrorMessages.INVALID_VISIT_ID_ERROR_MSG);
                    flag = ValidateKeyId(QuickInput.VisitId, sb, flag, ErrorMessages.INVALID_VISIT_ID_ERROR_MSG);
                    flag = ValidateKeyId(QuickInput.VisitId, sb, flag, ErrorMessages.INVALID_VISIT_ID_ERROR_MSG);
                    flag = ValidateKeyId(QuickInput.VisitId, sb, flag, ErrorMessages.INVALID_VISIT_ID_ERROR_MSG);
                    flag = ValidateKeyId(QuickInput.VisitId, sb, flag, ErrorMessages.INVALID_VISIT_ID_ERROR_MSG);
                }

            }
            else
            {
                sb.Append("\n" + ErrorMessages.PATIENT_QUICKINPUT_DTO_INVALID_ERROR_MSG);
                flag = false;
            }

            QuivusLogger.Info("End  Validating Quick Input with Status :: " + flag);
            return flag;
        }

        internal static bool ValidateVisitForCOSign(int VisitId, int ProviderId, int ProviderUserId, int ProviderTypeId, DateTime OperationDate, ref StringBuilder sb)
        {
            bool flag = true;

            //M TODO: implement it           


            flag = ValidateKeyId(VisitId, sb, flag, ErrorMessages.INVALID_VISIT_ID_ERROR_MSG);
            flag = ValidateKeyId(ProviderId, sb, flag, ErrorMessages.INVALID_PROVIDER_ID_ERROR_MSG);

            flag = ValidateKeyId(ProviderUserId, sb, flag, ErrorMessages.INVALID_USER_ID_ERROR_MSG);

            flag = ValidateKeyId(ProviderTypeId, sb, flag, ErrorMessages.INVALID_USER_TYPE_ID_ERROR_MSG);


            flag = ValidateDate(OperationDate, sb, flag, ErrorMessages.INVALID_COSIGN_DATE_ERROR_MSG);

            return flag;
        }

        public static bool ValidateModuleDictation(List<ModuleDictationDTO> ModuleDictationDTOList, ref StringBuilder sb)
        {
            bool flag = true;

            if (ModuleDictationDTOList != null)
            {
                foreach (ModuleDictationDTO obj in ModuleDictationDTOList)
                {
                    if (obj != null)
                    {
                        flag = ValidateModuleType(obj.ModuleTypeID, sb, flag);

                        //   flag = ValidateVisitId(obj.VisitID, sb, flag);

                        flag = ValidateDictationID(obj.DictationID, sb, flag);

                        //   flag = ValidateUserId(obj.ManagedByUserId, sb, flag);

                        if (obj.ModuleDictationId > 0 && obj.IsDirty)
                            flag = ValidateUserTYpeId(obj.ManagedByUserTypeId, sb, flag, ErrorMessages.MODULE_DICATION_USER_TYPEID_INVALID_ERROR_MSG);
                    }
                    else
                    {
                        sb.Append("\n" + ErrorMessages.MODULE_NOTE_DTO_INVALID_ERROR_MSG);
                        flag = false;
                    }
                }
            }
            else
            {
                sb.Append("\n" + ErrorMessages.MODULE_NOTE_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        private static bool ValidateDictationID(Guid guid, StringBuilder sb, bool flag)
        {
            if (guid == ProjectConstant.EMPTY_GUID)
            {
                sb.Append("\n" + ErrorMessages.INVALID_MODULE_DICTATION_GUID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        internal static bool ValidateVBImmunization(List<ImmunizationDTO> immunizationList, ref StringBuilder sb)
        {
            bool flag = true;

            //M TODO: implement it           

            if (immunizationList != null)
            {
                foreach (ImmunizationDTO obj in immunizationList)
                {
                    if (obj != null)
                    {
                        flag = ValidateImmunizationType(obj.ImmunizationType, sb, flag);

                        //flag = ValidateVisitId(obj.VisitId, sb, flag);

                     //   if (obj.PatientImmunizationId > 0 && obj.IsDirty)
                        if (obj.PatientImmunizationId > 0)
                        {
                            flag = ValidateKeyId(obj.ModifiedBy, sb, flag, ErrorMessages.IMMUNIZATION_MODIFIED_BY_INVALID_ERROR_MSG);
                        }
                        //   flag = ValidateUserId(obj.ManagedBy, sb, flag);
                    }
                    else
                    {
                        sb.Append("\n" + ErrorMessages.IMMUNIZATION_DTO_INVALID_ERROR_MSG);
                        flag = false;
                    }
                    return flag;
                }
            }
            else
            {
                sb.Append("\n" + ErrorMessages.IMMUNIZATION_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        internal static bool ValidateVBModuleDictation(List<ModuleDictationDTO> ModuleDictationDTOList, ref StringBuilder sb)
        {
            bool flag = true;

            if (ModuleDictationDTOList != null)
            {
                foreach (ModuleDictationDTO obj in ModuleDictationDTOList)
                {
                    if (obj != null)
                    {
                        flag = ValidateModuleType(obj.ModuleTypeID, sb, flag);

                        //   flag = ValidateVisitId(obj.VisitID, sb, flag);

                        flag = ValidateDictationID(obj.DictationID, sb, flag);

                        //   flag = ValidateUserId(obj.ManagedByUserId, sb, flag);

                        flag = ValidateUserTYpeId(obj.ManagedByUserTypeId, sb, flag, ErrorMessages.MODULE_DICATION_USER_TYPEID_INVALID_ERROR_MSG);
                    }
                    else
                    {
                        sb.Append("\n" + ErrorMessages.MODULE_NOTE_DTO_INVALID_ERROR_MSG);
                        flag = false;
                    }
                }
            }
            else
            {
                sb.Append("\n" + ErrorMessages.MODULE_NOTE_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }
    }
}
