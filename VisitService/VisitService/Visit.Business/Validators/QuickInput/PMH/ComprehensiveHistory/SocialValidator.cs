using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.ComprehensiveHistory.Social;
using QuivusUtility.ProjectConstants;

namespace Visit.Business.Validators.QuickInput.PMH.ComprehensiveHistory
{
    public class SocialValidator : QBaseValidator
    {
        internal static bool ValidateSocial(SocialHistoryDTO SocialHistoryObject, ref StringBuilder sb, bool IsUpdate)
        {
            bool flag = true;

            if (SocialHistoryObject != null)
            {
                if (SocialHistoryObject.PatientSocialHistoryID > 0)
                    IsUpdate = true;

                if (!IsUpdate)
                {
                //    flag = ValidateKeyId(SocialHistoryObject.CreatedBy, sb, flag, ErrorMessages.SOCIAL_HISTORY_INVALID_CREATEDBY_ERROR_MSG);
                //    flag = ValidateDate(SocialHistoryObject.CreatedAt, sb, flag, ErrorMessages.SOCIAL_HISTORY_INVALID_CREATEDAT_ERROR_MSG);
                }
                if (IsUpdate)
                {
                //    flag = ValidateKeyId(SocialHistoryObject.ModifiedBy, sb, flag, ErrorMessages.SOCIAL_HISTORY_INVALID_MODIFIEDBY_ERROR_MSG);
                }


             //   flag = ValidateKeyId(SocialHistoryObject.VisitID, sb, flag, ErrorMessages.SOCIAL_HISTORY_VISIT_ID_INVALID_ERROR_MSG);

             //   flag = ValidateKeyId(SocialHistoryObject.PatientID, sb, flag, ErrorMessages.SOCIAL_HISTORY_PATIENT_ID_INVALID_ERROR_MSG);
            }
            else
            {
                sb.Append("\n" + ErrorMessages.SOCIAL_ADDICTION_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        internal static bool ValidateDeletedSocial(SocialHistoryDTO DeletedObject, ref StringBuilder sb)
        {
            bool flag = false;

            if (DeletedObject != null)
            {
                flag = ValidateKeyId(DeletedObject.ModifiedBy, sb, flag, ErrorMessages.INVALID_MODIFIEDBY_ERROR_MSG);
                flag = ValidateDate(DeletedObject.ModifiedAt, sb, flag, ErrorMessages.INVALID_MODIFIEDAT_ERROR_MSG);

                flag = ValidateKeyId(DeletedObject.PatientSocialHistoryID, sb, flag, ErrorMessages.INVALID_PATIENT_SOCIAL_HISTORY_ID_ERROR_MSG);
                
            }
            return flag;
        }

        internal static bool ValidateVBSocial(SocialHistoryDTO SocialHistoryObject, ref StringBuilder sb, bool IsUpdate)
        {
            bool flag = true;

            if (SocialHistoryObject != null)
            {
                if (SocialHistoryObject.PatientSocialHistoryID > 0)
                    IsUpdate = true;

                if (!IsUpdate)
                {
                        flag = ValidateKeyId(SocialHistoryObject.CreatedBy, sb, flag, ErrorMessages.SOCIAL_HISTORY_INVALID_CREATEDBY_ERROR_MSG);
                       flag = ValidateDate(SocialHistoryObject.CreatedAt, sb, flag, ErrorMessages.SOCIAL_HISTORY_INVALID_CREATEDAT_ERROR_MSG);
                }
                if (IsUpdate)
                {
                        flag = ValidateKeyId(SocialHistoryObject.ModifiedBy, sb, flag, ErrorMessages.SOCIAL_HISTORY_INVALID_MODIFIEDBY_ERROR_MSG);
                }


                   //flag = ValidateKeyId(SocialHistoryObject.VisitID, sb, flag, ErrorMessages.SOCIAL_HISTORY_VISIT_ID_INVALID_ERROR_MSG);
                   flag = ValidateKeyId(SocialHistoryObject.PatientID, sb, flag, ErrorMessages.SOCIAL_HISTORY_PATIENT_ID_INVALID_ERROR_MSG);
            }
            else
            {
                sb.Append("\n" + ErrorMessages.SOCIAL_ADDICTION_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }
    }
}
