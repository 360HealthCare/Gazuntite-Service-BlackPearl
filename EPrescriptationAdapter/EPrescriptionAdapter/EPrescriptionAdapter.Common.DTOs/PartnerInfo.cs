using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EprescriptionService.Common.DTOs
{
    /// <summary>
    ///   Class:          PartnerInfo
    ///   Created by:     Waqas Baber 
    ///   Description:    DTO for parnter info 
    ///   Notes:          Use for fetching vendor info
    ///   Copyright 2011 Zigron Inc. All rights reserved  
    /// </summary>
    public class PartnerInfo
    {
        public int PartnerID { get; set; }
        public String PartnerName { get; set; }
        public String AccountUserName { get; set; }
        public String EngineVersion { get; set; }
        public String SystemName { get; set; }
        public String Password { get; set; }

    }
}
