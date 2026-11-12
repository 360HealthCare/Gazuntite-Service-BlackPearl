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
    /// This class will save uploaded file info.
    /// </summary>
   
    public class UploadedFileInfo
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public Guid VirtualFileName { get; set; }
        public PatientDocumentDTO DocmentInfo { get; set; } 
    }
}
