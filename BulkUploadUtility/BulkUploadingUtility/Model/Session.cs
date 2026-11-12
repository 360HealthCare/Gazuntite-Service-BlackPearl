using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BulkUploadingUtility.UserManagerService;

namespace BulkUploadingUtility.Model
{
    public class Session
    {
        public static LoginDTO CurrentUser { get; set; }

        public static int CurrentClinicBranchID { get; set; }

        public static string CurrentClinicBranchName { get; set; }

        public static int CurrentProviderID { get; set; }

        public static int CurrentUserTypeID { get; set; }

        public static List<StationDTO> StationList { get; set; }

        // Share Date

        public static List<Document> DocSearchList { get; set; }
    }
}
