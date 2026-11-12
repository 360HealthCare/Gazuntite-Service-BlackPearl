using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


 
using Quivus.Model.User.Patient;
using Quivus.Model.Common;

namespace UserManager.Business.Allergy
{
    /// <summary>
    /// Author Mansoor
    /// Date 20 Jan 2011
    /// </summary>
    public interface IAllergyManager
    {
        List<AllergySearchDTO> SearchAllergyTypeByName(string allergyName);

        bool SavePatientAllergy(int patientId, int createdBy, PatientAllergyDTO patientAllergy);
    }
}
