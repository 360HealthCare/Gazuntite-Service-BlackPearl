using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusUtility.ProjectConstants;
using Quivus.Model.QuickInput;

namespace Visit.Business.Validators.QuickInput.PMH.ChronicMedication
{
    public class ChronicMedicationValidator : QBaseValidator
    {
        internal static bool ValidateMedication(MedicationPrescribedDTO ChronicMedicationObj, ref StringBuilder sb, bool IsUpdate)
        {
            bool flag = true;

            //M TODO: implement it           

            if (ChronicMedicationObj != null)
            {
                if (!IsUpdate)
                {
                    flag = ValidateKeyId(ChronicMedicationObj.CreatedBy, sb, flag, ErrorMessages.CHRONIC_MEDICATION_INVALID_CREATEDBY_ERROR_MSG);
                    flag = ValidateDate(ChronicMedicationObj.CreatedAt, sb, flag, ErrorMessages.CHRONIC_MEDICATION_INVALID_CREATEDAT_ERROR_MSG);
                }
                if (IsUpdate)
                {
                    flag = ValidateKeyId(ChronicMedicationObj.ModifiedBy, sb, flag, ErrorMessages.CHRONIC_MEDICATION_INVALID_MODIFIEDBY_ERROR_MSG);
                }

                flag = ValidateKeyId(ChronicMedicationObj.VisitID, sb, flag, ErrorMessages.CHRONIC_MEDICATION_INVALID_VISIT_ID_ERROR_MSG);

            }
            else
            {
                sb.Append("\n" + ErrorMessages.VITAL_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        
        }
    }
}
