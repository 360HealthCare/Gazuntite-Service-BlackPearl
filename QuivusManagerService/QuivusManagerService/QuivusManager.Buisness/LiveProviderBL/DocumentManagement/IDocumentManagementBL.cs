using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.ServiceResponse.ServiceCustomResponse.LiveProvider.DocumentManagement;

namespace QuivusManager.Buisness.LiveProviderBL.DocumentManagement
{
    public interface IDocumentManagementBL
    {
        PatientDocumentResponse LoadPatientDocumentByClinicIdAndPatientID(int clinicBranchID, int patientID, int pageNo);

        SearchFieldTypeResponse LoadSearchField();
    }
}
