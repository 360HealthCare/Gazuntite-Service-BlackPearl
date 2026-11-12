using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BulkUploadingUtility.DocumentManagerService;

namespace BulkUploadingUtility.Utility
{
    public class ServiceOperator
    {

        public static void ManageLog(string message, string stackTrace, bool isError)
        {
            DocumentManagerServiceClient client = new DocumentManagerServiceClient();
            client.ManageLog(message, stackTrace, isError);
        }

        public static bool SaveDocument(PatientDocumentDTO patientDocumentObject)
        {
            bool response = false;
            try
            {
               
            }
            catch
            {
                throw;
            }
            return response;
        }
    }
}
