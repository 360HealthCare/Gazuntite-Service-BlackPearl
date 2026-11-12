using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuivusManager.ServiceResponse.ServiceCustomResponse.LiveProvider.DocumentManagement;
using QuivusManager.Buisness.ApplicationSession;
using Quivus.Utility.Logger;
using QuivusManager.ServiceResponse.ServiceCustomSession;
using Quivus.Utility.CustomException;
using QuivusManager.Buisness.Utility;
using System.ServiceModel;
using Quivus.Utility.Fault;
using QuivusManager.Buisness.LiveProviderBL.DocumentManagement.Impl;
using QuivusManager.Buisness.LiveProviderBL.DocumentManagement;

namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    public partial class QuivusManagerService
    {
        IDocumentManagementBL DocumentManagementBLObj = null;

        public PatientDocumentResponse LoadPatientDocumentByClinicIdAndPatientID(int clinicBranchID, int patientID, int pageNo, UserSessionDTO userSession)
        {
            PatientDocumentResponse response = null;
            try
            {
                QuivusLogger.Info("LoadPatientDocumentByClinicIdAndPatientID Called");
                if (DocumentManagementBLObj == null)
                {
                    DocumentManagementBLObj = new DocumentManagementBL();
                }
                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    response = DocumentManagementBLObj.LoadPatientDocumentByClinicIdAndPatientID(clinicBranchID, patientID, pageNo);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientDocumentResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientDocumentResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date August 28 2011 :)
        /// </summary>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public SearchFieldTypeResponse LoadSearchField(UserSessionDTO userSession)
        {
            SearchFieldTypeResponse response = null;
            try
            {
                QuivusLogger.Info("LoadPatientDocumentByClinicIdAndPatientID Called");
                if (DocumentManagementBLObj == null)
                {
                    DocumentManagementBLObj = new DocumentManagementBL();
                }
                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    response = DocumentManagementBLObj.LoadSearchField();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new SearchFieldTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new SearchFieldTypeResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

    }
}