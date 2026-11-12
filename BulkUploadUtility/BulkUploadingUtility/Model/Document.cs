using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BulkUploadingUtility.DocumentManagerService;

namespace BulkUploadingUtility.Model
{
    
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 07/20/2011
    /// This class will save information about uploaded documents
    /// </summary>

    public class Document
    {
        public string Root { get; set; }

        public string VendorDocuments { get; set; }

        public string Pratice { get; set; }

        public string Station { get; set; }

        public string Patient { get; set; }

        public string Section { get; set; }

        public string SubSection { get; set; }

        public string DocumentName { get; set; }

        public string PatientID { get; set; }

        public List<UploadedFileInfo> FileList { get; set; }

        public List<PatientDocumentDTO> PatDocLst { get; set; }

        public override string ToString()
        {
            return Patient;
        }

    }
}
