using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.User.Patient;
using Quivus.Utility.ProjectConstants;

namespace UserManager.Business.Validators
{
    public class QUserValidator : QValidator
    {
        public static bool ValidatePatientSettingInfo(PatientProfileSettingDTO patientSettingDTO, ref StringBuilder sb)
        {
            bool flag = true;

            if (patientSettingDTO != null)
            {
                flag = ValidateClinicBranch(patientSettingDTO.ClinicBranchID, sb, flag);

                if (patientSettingDTO.UserUserProfileCategoryID > 0)
                    flag = ValidateProviderId(patientSettingDTO.ModifiedBy, sb, flag);
                else
                    flag = ValidateProviderId(patientSettingDTO.CreatedBy, sb, flag);

                flag = ValidateProviderId(patientSettingDTO.UserID, sb, flag);
                flag = ValidateSectionNo(patientSettingDTO.SectionNo, sb, flag);
                flag = ValidateUserProfileCategoryID(patientSettingDTO.UserProfileCategoryID, sb, flag);
            }
            else
            {
                sb.Append("\n" + UserRegistrationConstant.PROFILE_SETTING_DTO_INVALID);
                flag = false;
            }
            return flag;
        }

      
    }
}
