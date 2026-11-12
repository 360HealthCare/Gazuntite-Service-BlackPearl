using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.ComprehensiveHistory.Medical;
using Quivus.Utility;
using Quivus.Model.QuickInput.ComprehensiveHistory.Family;
using QuivusUtility.ProjectConstants;

namespace Visit.Business.Validators.QuickInput.PMH.ComprehensiveHistory
{
    public class ComprehensiveValidator : QBaseValidator
    {
        public static bool ValidateMedicalHistoryDTO(MedicalHistoryDTO HistoryDTO, ref StringBuilder sb,bool IsUpdate)
        {
            bool flag = true;

            //M TODO: implement it           
           
            if (HistoryDTO != null)
            {
                if (!IsUpdate)
                {
                 //   flag = ValidateKeyId(HistoryDTO.CreatedBy, sb, flag, ErrorMessages.MEDICAL_HISTORY_INVALID_CREATEDBY_ERROR_MSG);
                 //   flag = ValidateDate(HistoryDTO.CreatedAt, sb, flag, ErrorMessages.MEDICAL_HISTORY_INVALID_CREATEDAT_ERROR_MSG);
                }
                if (IsUpdate)
                {
                  //  flag = ValidateKeyId(HistoryDTO.ModifiedBy, sb, flag, ErrorMessages.MEDICAL_HISTORY_INVALID_MODIFIEDBY_ERROR_MSG);
                }
                
                flag = ValidateKeyId(HistoryDTO.MedicalHistoryTypeID, sb, flag, ErrorMessages.INVALID_MEDICAL_HISTORY_ERROR_MSG);
             //   flag = ValidateStringInfo(HistoryDTO.PatientMedicalHistoryDescription, sb, flag, ErrorMessages.INVALID_PATIENT_MEDICALHISTORY_DESCP_ERROR_MSG);

            }
            else
            {
                sb.Append("\n" + ErrorMessages.PATIENT_MEDICAL_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        internal static bool ValidateFamilyHistoryDTO(FamilyHistoryDTO HistoryDTO, ref StringBuilder sb, bool IsUpdate)
        {
            bool flag = true;

            //M TODO: implement it           

            if (HistoryDTO != null)
            {
                if (!IsUpdate)
                {
                 //   flag = ValidateKeyId(HistoryDTO.CreatedBy, sb, flag, ErrorMessages.FAMILY_HISTORY_INVALID_CREATEDBY_ERROR_MSG);
                 //   flag = ValidateDate(HistoryDTO.CreatedAt, sb, flag, ErrorMessages.FAMILY_HISTORY_INVALID_CREATEDAT_ERROR_MSG);
                }
                if (IsUpdate)
                {
                 //   flag = ValidateKeyId(HistoryDTO.ModifiedBy, sb, flag, ErrorMessages.FAMILY_HISTORY_INVALID_MODIFIEDBY_ERROR_MSG);
                }

          //      flag = ValidateKeyId(HistoryDTO.ReletionshipTypeID, sb, flag, VisitConstant.INVALID_RELATIONSHIP_ERROR_MSG);
                flag = ValidateKeyId(HistoryDTO.FamilyHistoryTypeID, sb, flag, ErrorMessages.INVALID_FAMILY_TYPE_ID_ERROR_MSG);

           //     flag = ValidateKeyId(HistoryDTO.DiseaseTypeID, sb, flag, VisitConstant.INVALID_DISEASE_TYPE_ID_ERROR_MSG);

            //    flag = ValidateKeyId(HistoryDTO.VisitID, sb, flag, ErrorMessages.FAMILY_HISTORY_INVALID_VISIT_ID_ERROR_MSG);
            }
            else
            {
                sb.Append("\n" + ErrorMessages.PATIENT_FAMILY_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        internal static bool ValidateDeleteFamilyHistoryDTO(FamilyHistoryDTO HistoryDTO, ref StringBuilder sb)
        {
            bool flag = true;

            //M TODO: implement it           

            if (HistoryDTO != null)
            {
                    flag = ValidateKeyId(HistoryDTO.PatientFamilyHistoryID, sb, flag, ErrorMessages.INVALID_PATIENT_FAMILY_TYPE_ID_ERROR_MSG);

            }
            else
            {
                sb.Append("\n" + ErrorMessages.PATIENT_FAMILY_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        internal static bool ValidateVBMedicalHistoryDTO(MedicalHistoryDTO HistoryDTO, ref StringBuilder sb,bool IsUpdate)
        {
            bool flag = true;

            //M TODO: implement it           

            if (HistoryDTO != null )
            {
                if (!IsUpdate)
                {
                       flag = ValidateKeyId(HistoryDTO.CreatedBy, sb, flag, ErrorMessages.MEDICAL_HISTORY_INVALID_CREATEDBY_ERROR_MSG);
                       flag = ValidateDate(HistoryDTO.CreatedAt, sb, flag, ErrorMessages.MEDICAL_HISTORY_INVALID_CREATEDAT_ERROR_MSG);
                }
               // if (IsUpdate && HistoryDTO.IsDirty)
                if (IsUpdate)
                {
                      flag = ValidateKeyId(HistoryDTO.ModifiedBy, sb, flag, ErrorMessages.MEDICAL_HISTORY_INVALID_MODIFIEDBY_ERROR_MSG);
                      flag = ValidateDate(HistoryDTO.ModifiedAt, sb, flag, ErrorMessages.MEDICAL_HISTORY_INVALID_MODIFIEDAT_ERROR_MSG);
                }

                   flag = ValidateKeyId(HistoryDTO.MedicalHistoryTypeID, sb, flag, ErrorMessages.INVALID_MEDICAL_HISTORY_ERROR_MSG);
                 //  flag = ValidateKeyId(HistoryDTO.VisitID, sb, flag, ErrorMessages.INVALID_VISIT_ID_MEDICAL_HISTORY_ERROR_MSG);
                //   flag = ValidateStringInfo(HistoryDTO.PatientMedicalHistoryDescription, sb, flag, ErrorMessages.INVALID_PATIENT_MEDICALHISTORY_DESCP_ERROR_MSG);
            }
            else
            {
                sb.Append("\n" + ErrorMessages.PATIENT_MEDICAL_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }


        internal static bool ValidateVBFamilyHistoryDTO(FamilyHistoryDTO HistoryDTO, ref StringBuilder sb, bool IsUpdate)
        {
            bool flag = true;

            //M TODO: implement it           

            if (HistoryDTO != null)
            {
                if (!IsUpdate)
                {
                       flag = ValidateKeyId(HistoryDTO.CreatedBy, sb, flag, ErrorMessages.FAMILY_HISTORY_INVALID_CREATEDBY_ERROR_MSG);
                       flag = ValidateDate(HistoryDTO.CreatedAt, sb, flag, ErrorMessages.FAMILY_HISTORY_INVALID_CREATEDAT_ERROR_MSG);
                }
               // if (IsUpdate && HistoryDTO.IsDirty) // Updated Record & IsDirty Must Be True
                if (IsUpdate) // Updated Record & IsDirty Must Be True
                {
                       flag = ValidateKeyId(HistoryDTO.ModifiedBy, sb, flag, ErrorMessages.FAMILY_HISTORY_INVALID_MODIFIEDBY_ERROR_MSG);
                       flag = ValidateDate(HistoryDTO.ModifiedAt, sb, flag, ErrorMessages.FAMILY_HISTORY_INVALID_MODIFIEDAT_ERROR_MSG);
                }

                //      flag = ValidateKeyId(HistoryDTO.ReletionshipTypeID, sb, flag, VisitConstant.INVALID_RELATIONSHIP_ERROR_MSG);
                flag = ValidateKeyId(HistoryDTO.FamilyHistoryTypeID, sb, flag, ErrorMessages.INVALID_FAMILY_TYPE_ID_ERROR_MSG);

                //     flag = ValidateKeyId(HistoryDTO.DiseaseTypeID, sb, flag, VisitConstant.INVALID_DISEASE_TYPE_ID_ERROR_MSG);

               //     flag = ValidateKeyId(HistoryDTO.VisitID, sb, flag, ErrorMessages.FAMILY_HISTORY_INVALID_VISIT_ID_ERROR_MSG);
            }
            else
            {
                sb.Append("\n" + ErrorMessages.PATIENT_FAMILY_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        internal static bool ValidateDeleteMedicalHistoryDTO(MedicalHistoryDTO HistoryDTO, ref StringBuilder sb)
        {
            bool flag = true;
         

            if (HistoryDTO != null)
            {
                flag = ValidateKeyId(HistoryDTO.PatientMedicalHistoryID, sb, flag, ErrorMessages.INVALID_PATIENT_MEDICAL_ID_ERROR_MSG);
            }
            else
            {
                sb.Append("\n" + ErrorMessages.PATIENT_FAMILY_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }
    }
}
