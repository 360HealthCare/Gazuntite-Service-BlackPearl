using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.User.Patient;
using Quivus.Model.QuickInput;
using QuivusUtility.ProjectConstants;

namespace Visit.Business.Validators.QuickInput.PMH.Allergy
{
    public class AllergyValidator : QBaseValidator
    {


        internal static bool ValidateAllergy(AllergyDTO AllergyObj, ref StringBuilder sb, bool IsUpdate)
        {
            bool flag = true;

            //M TODO: implement it           

            if (AllergyObj != null)
            {
                if (!IsUpdate)
                {
                    flag = ValidateKeyId(AllergyObj.CreatedBy, sb, flag, ErrorMessages.ALLERGY_INVALID_CREATEDBY_ERROR_MSG);
                    flag = ValidateDate(AllergyObj.CreatedAt, sb, flag, ErrorMessages.ALLERGY_INVALID_CREATEDAT_ERROR_MSG);
                }
                if (IsUpdate)
                {
                    flag = ValidateKeyId(AllergyObj.ModifiedBy, sb, flag, ErrorMessages.ALLERGY_INVALID_MODIFIEDBY_ERROR_MSG);
                }

                flag = ValidateKeyId(AllergyObj.VisitId, sb, flag, ErrorMessages.ALLERGY_INVALID_VISIT_ID_ERROR_MSG);

                flag = ValidateStringInfo(AllergyObj.Reaction, sb, flag, ErrorMessages.INVALID_ALLERGY_REACTION_ERROR_MSG);

                //flag = ValidateDate(AllergyObj.OnSet, sb, flag, ErrorMessages.ALLERGY_INVALID_ONSET_ERROR_MSG);
                flag = ValidateStringInfo(AllergyObj.OnSet, sb, flag, ErrorMessages.ALLERGY_INVALID_ONSET_ERROR_MSG);

            }
            else
            {
                sb.Append("\n" + ErrorMessages.VITAL_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        internal static bool ValidateAllergy(List<AllergyDTO> Allergylist, ref StringBuilder sb)
        {
            bool flag = true;
            bool IsUpdate = false;
            foreach (AllergyDTO obj in Allergylist)
            {
                if (obj.PatientAllergyId > 0)
                    IsUpdate = true;

                flag= ValidateAllergy(obj, ref sb, IsUpdate);
            }
            return flag;
        }
    }
}
