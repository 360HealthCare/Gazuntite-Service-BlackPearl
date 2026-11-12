using System;

namespace EprescriptionService.Utility
{
    /// <summary>
    ///   Class:          SystemConstants
    ///   Created by:     Waqas Baber 
    ///   Description:    System wide constants
    ///   Notes:          
    ///   Copyright 2011 Zigron Inc. All rights reserved  
    /// </summary>
    public class SystemConstants
    {

        /* connection constants block */

        public const String NetworkUriKey = "NetworkURI";

        #region Physcical file paths 

        public const String UploadProblemsMessagePath = @"/xmlMessages/UploadProblemsReqeustXMLMessage.xml";
        public const String UploadPatientMessagePath = @"/xmlMessages/UploadPatientRequestXMLMessage.xml";
        public const String DownloadAllergiesMessagePath = @"/xmlMessages/DownloadAllergiesReqestXMLMessages.xml";
        public const String DownloadMeddicationMessagePath = @"/xmlMessages/DownloadMeddicationRequestXMLMessages.xml";
        public const String DownloadPrescriptionPatientMessagePath = @"/xmlMessages/DownloadPrescriptionRequestXMLMessage.xml";
        public const String DownloadProblemsMessagePath = @"/xmlMessages/DownloadProblemsRequestXMLMessage.xml";
        public const String GetNotificationsMessagePath = @"/xmlMessages/GetNotificationsXMMessage.xml";
        

        #endregion


       

    }
}
