using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Utility;

using Quivus.Model.Common;
using Quivus.Utility.CustomException;
using UserManagerService.DataAccess;
using Quivus.Utility.Logger;
using Quivus.Utility.Factory;

 
using Quivus.Model.User.Patient;
using UserManager.Business.Utility;

namespace UserManager.Business.Allergy.Impl
{
    /// <summary>
    /// Author Mansoor
    /// Date 20 Jan 2011
    /// </summary>
    public class AllergyManagerImpl : IAllergyManager
    {
        AllergyDAO alleryDAO = null;

        public List<AllergySearchDTO> SearchAllergyTypeByName(string allergyName)
        {
            List<AllergySearchDTO> response = null;
            try
            {
                QuivusLogger.Info("SearchAllergyTypeByName PARAM allergyName:: " + allergyName);

                if (String.IsNullOrEmpty(allergyName))
                {
                    throw new InvalidArgumentException(ProjectConstant.ALLERGY_NAME_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                if (alleryDAO == null)
                {
                    alleryDAO = (AllergyDAO)DAOFactory.GetDataAccessObject(typeof(AllergyDAO));
                }
                response = alleryDAO.SearchAllergyTypeByName(allergyName);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public bool SavePatientAllergy(int patientId, int createdBy, PatientAllergyDTO patientAllergy)
        {
            bool result = false;

            try
            {
                QuivusLogger.Info("SearchAllergyTypeByName PARAM PatientId:: " + patientId + " CreatedBy " + createdBy);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }
                if (createdBy <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }
                if (patientAllergy == null || patientAllergy.AllergyType == null || patientAllergy.AllergyType.AllergyTypeIdSeq <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.ALLERGY_INVALID_ERROR_MSG);
                }

                if (alleryDAO == null)
                {
                    alleryDAO = (AllergyDAO)DAOFactory.GetDataAccessObject(typeof(AllergyDAO));
                }

                result = alleryDAO.SavePatientAllergy(patientId, createdBy, patientAllergy);

            }
            catch     
            {
                throw;
            }
            return result;
        }
    }
}
