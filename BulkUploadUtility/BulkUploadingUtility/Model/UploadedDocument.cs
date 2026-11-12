using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BulkUploadingUtility.DocumentManagerService;

namespace BulkUploadingUtility.Model
{
    public class UploadedDocument
    {
        public List<Document> SectionDocsList { get; set; }

        public List<PatientDocumentDTO> PatDocLst { get; set; }
    }
}
