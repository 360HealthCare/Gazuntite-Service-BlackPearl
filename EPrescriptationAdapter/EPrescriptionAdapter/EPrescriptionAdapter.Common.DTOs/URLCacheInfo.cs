using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EprescriptionService.Common.DTOs
{
    public class URLCacheInfo
    {

        
        public String URLBrowserWeb { get; set; }
        public String EngineUploadURL{ get; set; }
        public String EngineDownloadURL { get; set; }
        public String LinkUploadUrl { get; set; }
        public String LinKDownloadUrl { get; set; }

        public String ACWebBrowserURL { get; set; }
        public String ACEngineUploadURL { get; set; }
        public String ACEngineDownloadURL { get; set; }
        public String ACLinkDownloadURL { get; set; }
        public String ACLinkUploadURL { get; set; }
        public String PayorEngineURL { get; set; }
        public String RegistrationURL { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime LastActivatedAt { get; set; }
            
    }
}
