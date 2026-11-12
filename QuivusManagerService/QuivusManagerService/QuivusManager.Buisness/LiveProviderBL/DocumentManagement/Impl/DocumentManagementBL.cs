using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.ServiceResponse.ServiceCustomResponse.LiveProvider.DocumentManagement;
using QuivusManager.IntegratedServies.DocumentManagerService;
using QuivusManager.IntegratedServies.ReferenceFactory;
using Quivus.Utility.Logger;
using Quivus.Utility;
using QuivusManager.ServiceResponse.ServiceCustomSession;

namespace QuivusManager.Buisness.LiveProviderBL.DocumentManagement.Impl
{
    public class DocumentManagementBL : IDocumentManagementBL
    {
        DocumentManagerServiceClient DocumentServiceClient = null;
        public PatientDocumentResponse LoadPatientDocumentByClinicIdAndPatientID(int clinicBranchID, int patientID, int pageNo)
        {
            QuivusLogger.Info("LoadPatientDocumentByClinicIdAndPatientID in BL");
            PatientDocumentResponse response = null;
            PaggingResponseDTO serviceResponse = null;
            try
            {
                if(DocumentServiceClient == null)
                {
                    DocumentServiceClient=(DocumentManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(DocumentManagerServiceClient));
                }
             
                serviceResponse = DocumentServiceClient.LoadPatientDocumentByClinicIdAndPatientID(clinicBranchID, patientID, pageNo);
                if (response == null)
                {
                    response = new PatientDocumentResponse();
                }
                if (serviceResponse != null)
                {
                    response.PatientDocument = serviceResponse;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                // TODO: remove comments before deployments
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date August 28 2011 :)
        /// </summary>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public SearchFieldTypeResponse LoadSearchField()
        {
            QuivusLogger.Info("LoadSearchField in BL");
            SearchFieldTypeResponse response = null;
            List<SearchFieldTypeDTO> serviceResponse = null;
            try
            {
                if (DocumentServiceClient == null)
                {
                    DocumentServiceClient = (DocumentManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(DocumentManagerServiceClient));
                }

                serviceResponse = DocumentServiceClient.LoadSearchField();
                if (response == null)
                {
                    response = new SearchFieldTypeResponse();
                }
                if (serviceResponse != null)
                {
                    response.ResponseData = serviceResponse;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }
    }
}
