using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.DocumentManagerService;
using System.ServiceModel;
using QuivusManager.ServiceResponse.ServiceCustomSession;
using QuivusManager.ServiceResponse.ServiceCustomResponse.LiveProvider.DocumentManagement;


namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    public partial interface IQuivusManagerService
    {
        [OperationContract]
        PatientDocumentResponse LoadPatientDocumentByClinicIdAndPatientID(int clinicBranchID, int patientID, int pageNo, UserSessionDTO userSession);

        [OperationContract]
        SearchFieldTypeResponse LoadSearchField(UserSessionDTO userSession);
        
    }
}
