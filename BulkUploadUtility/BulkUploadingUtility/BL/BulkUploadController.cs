using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BulkUploadingUtility.Utility;
using BulkUploadingUtility.Model;
using System.IO;
using BulkUploadingUtility.DocumentManagerService;
using BulkUploadingUtility.Model.Response;

namespace BulkUploadingUtility.BL
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 07/20/2011
    /// This class control the utility opeartions 
    /// </summary>

    public class BulkUploadController
    {
        BulkDocumentUploader docUploader = null;
        public event EventHandler FileUpLoaded;
        public event EventHandler LogControl;

        public DocumentsUploadResponse startUtility(bool isAllUpload, string selectedPath, Document selectedDocument)
        {
            DocumentsUploadResponse response = null;
            ServiceOperator.ManageLog("Calling startUtility", "", false);
            try
            {
                if (docUploader == null)
                {
                    docUploader = new BulkDocumentUploader();
                }
                this.LogControl("STARING SCAN FOR FILES TO BE UPLOADED .....", EventArgs.Empty);
                response = docUploader.ManageFiles(isAllUpload, selectedPath, selectedDocument);
                this.LogControl("SCAN FOR FILES TO BE UPLOADED IS COMPLETED.....", EventArgs.Empty);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public int TotalFiles(List<Document> documentList)
        {
            int totalFiles = 0;
            foreach (Document doc in documentList)
            {
                if (doc.FileList != null)
                {
                    if (doc.FileList.Count > 0)
                        totalFiles = totalFiles + doc.FileList.Count;
                }
            }
            return totalFiles;
        }
    }
}
