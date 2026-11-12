using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkUploadingUtility.Model.Response
{
    public class DocumentsUploadResponse
    {
        public int TotalPatientScaned { get; set; }

        public int TotalPatientUploaded { get; set; }

        public int TotalFilesUploaded { get; set; }

        public bool IsError { get; set; }

        public string ErrorMessage { get; set; }
    }
    
}
