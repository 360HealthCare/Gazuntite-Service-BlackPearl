using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.ComprehensiveHistory.Social;
using QuivusUtility.ProjectConstants;

namespace Visit.Business.Validators.QuickInput.PMH.ComprehensiveHistory
{
    public class SocialAddictionValidator : QBaseValidator
    {
        internal static bool ValidateSocialAddiction(SocialHistoryAddictionDTO obj, ref StringBuilder sb,bool IsUpdate)
        {
            bool flag = true;
            if (obj != null)
            {
                if (obj.SocialHistoryAddictionID > 0)
                    IsUpdate = true;

                if (!IsUpdate)
                {
                 //   flag = ValidateKeyId(obj.CreatedBy, sb, flag, ErrorMessages.SOCIAL_ADDICTION_INVALID_CREATEDBY_ERROR_MSG);
                 //   flag = ValidateDate(obj.CreatedAt, sb, flag, ErrorMessages.SOCIAL_ADDICTION_INVALID_CREATEDAT_ERROR_MSG);
                }
                if (IsUpdate)
                {
                 //   flag = ValidateKeyId(obj.ModifiedBy, sb, flag, ErrorMessages.SOCIAL_ADDICTION_INVALID_MODIFIEDBY_ERROR_MSG);
                  //  flag = ValidateDate(obj.ModifiedAt, sb, flag, ErrorMessages.INVALID_MODIFIEDAT_ERROR_MSG);
                }


                flag = ValidateKeyId(obj.PatientSocialHistoryID, sb, flag, ErrorMessages.PATIENT_SOCIAL_HISTORY_ID_ERROR_MSG);

                flag = ValidateKeyId(obj.SocialHistoryAddictionTypeID, sb, flag, ErrorMessages.SOCIAL_ADDICTION_TYPE_ID_ERROR_MSG);

             //   flag = ValidateKeyId(obj.MediumTypeID, sb, flag, ErrorMessages.SOCIAL_ADDICTION_INVALID_MEDIUM_TYPE_ID_ERROR_MSG);
             //   flag = ValidateKeyId(obj.UsageAmountID, sb, flag, ErrorMessages.SOCIAL_ADDICTION_INVALID_USAGEAMOUNT_ID_ERROR_MSG);

               // flag = ValidateDate(obj.OnSet, sb, flag, ErrorMessages.SOCIAL_ADDICTION_INVALID_ONSET_ERROR_MSG);
             //   flag = ValidateStringInfo(obj.OnSet, sb, flag, ErrorMessages.SOCIAL_ADDICTION_INVALID_ONSET_ERROR_MSG);
             //   flag = ValidateStringInfo(obj.EndDate, sb, flag, ErrorMessages.SOCIAL_ADDICTION_INVALID_ENDDATE_ERROR_MSG);
            }

            else
            {
                sb.Append("\n" + ErrorMessages.SOCIAL_ADDICTION_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        internal static bool ValidateDeletedSocialAddiction(SocialHistoryAddictionDTO DeletedObject, ref StringBuilder sb)
        {
            bool flag = false;

            if (DeletedObject != null)
            {
                flag = ValidateKeyId(DeletedObject.ModifiedBy, sb, flag, ErrorMessages.SOCIAL_ADDICTION_INVALID_MODIFIEDBY_ERROR_MSG);
                flag = ValidateDate(DeletedObject.ModifiedAt, sb, flag, ErrorMessages.SOCIAL_ADDICTION_INVALID_MODIFIEDAT_ERROR_MSG);
            }
            return flag;
        }
    }
}
