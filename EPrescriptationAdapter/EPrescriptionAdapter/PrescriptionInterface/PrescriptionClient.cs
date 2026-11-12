using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Net.NetworkInformation;
using System.Text;
using EprescriptionService.Common.DTOs;
using EprescriptionService.Utility;


namespace PrescriptionInterface
{

    /// <summary>
    ///   Class:          PrescriptionClient
    ///   Created by:     Waqas Baber on 12/28/2010
    ///   Description:    PrescriptionClient is a proxy client for dr.first e-prescription portal,
    ///  we can change the vendor in future by only replacing this file   
    ///   Notes:          
    ///   Revision History:
    ///   Name:           Date:        Description:
    ///   Copyright 2010 Zigron Inc. All rights reserved  
    /// </summary>
    class PrescriptionClient
    {
        /// <summary>
        ///  Singleton Global instance of PrescriptionClient 
        /// </summary>
        private static volatile PrescriptionClient _prescriptionEngine;

        /// <summary>
        /// Use for Thread Save Singleton instance 
        /// </summary>
        private static Object _syncRoot = new Object();

        /// <summary>
        /// WebClient for sending requests to prescription engine
        /// </summary>
        private WebClient _webClient;

           /// Constructor initialze the Web Client and attach event 
        /// </summary>
        private PrescriptionClient()
        {
         //   _webClient = new WebClient();
            
         
        }

        /// <summary>
        /// checking and updating ans connectivity
        /// </summary>
        /// <param name="xmlRequest"></param>
        /// <returns></returns>
        public String ANS_Conectivity(string xmlRequest, String URL)
        {
             //Status check Event
            // ping url gona be change
            Uri networkURI;
          //  _webClient.DownloadDataCompleted += new DownloadStringCompletedEventHandler(this.webClient_DownloadStringCompleted);

            byte[] responseBytes = null;
            String response;
            using(_webClient = new WebClient())
            {

                responseBytes = _webClient.UploadData(URL, "POST", SystemConverters.StringToByteConverter(xmlRequest));
                response = Encoding.UTF8.GetString(responseBytes);


            }

            return response;
            
            ////try
            ////{
            ////    if (NetworkInterface.GetIsNetworkAvailable() == true)
            ////    {
            ////        if (System.Configuration.ConfigurationSettings.AppSettings != null)
            ////        {
            ////            if (System.Configuration.ConfigurationSettings.AppSettings["URIOne"])
            ////            {
            ////                networkURI = new Uri(MediaTypeNames.Application.Current.Resources["NetworkURI"].ToString());
            ////            }
            ////            else
            ////            {
            ////                // need to define 
            ////                networkURI = new Uri(@" ");
            ////            }
            ////        }
            ////        // MessageBox.Show("calling offline ping url");

            ////        this._webClient.DownloadStringAsync(networkURI);
            ////    }
            ////    else if (App.Current.IsRunningOutOfBrowser == false)
            ////    {

            ////        ConnectionEventArgs connEventArg = new ConnectionEventArgs();
            ////        connEventArg.ConnectionStrategyType = ConnectionStrategyTypeEnum.DC;
            ////        this.StrategyChangeToDC(connEventArg);
            ////        DefaultPopupDialog.Show(SystemMessages.UPMCNetworkNotAvailable);
            ////    }
            ////    else if (App.Current.IsRunningOutOfBrowser == true)
            ////    {
            ////        ConnectionEventArgs connEventArg = new ConnectionEventArgs();
            ////        connEventArg.ConnectionStrategyType = ConnectionStrategyTypeEnum.OFFLINE;
            ////        this.StrategyChangeToOffline(connEventArg);
            ////    }
            ////}
            ////catch (Exception ex)
            ////{
            ////    // Logging Exception
              
            ////}
        }


        /// <summary>
        /// Upload a paitient when to e-prescription
        /// </summary>
        /// <param name="patientInfoDTO"></param>
        /// <param name="URL"></param>
        /// <returns></returns>
        public String UPloadPatient(String xmlMessage, String URL)
        {
            byte[] responseBytes = null;
            String response;
            using (_webClient = new WebClient())
            {

                responseBytes = _webClient.UploadData(URL, "POST", SystemConverters.StringToByteConverter(xmlMessage));
                response = Encoding.UTF8.GetString(responseBytes);


            }
            return response;
        }


        public String UploadProblems(String xmlMessage,String URL)
        {
            byte[] responseBytes = null;
            String response;
            using (_webClient = new WebClient())
            {

                responseBytes = _webClient.UploadData(URL, "POST", SystemConverters.StringToByteConverter(xmlMessage));
                response = Encoding.UTF8.GetString(responseBytes);


            }
            return response;

        }

        /// <summary>
        /// Download all the allergies from the server
        /// </summary>
        /// <returns></returns>
        public String DownloadAlergies(string xmlMessage, String URL)
        {

            byte[] responseBytes = null;
            String response;
            using (_webClient = new WebClient())
            {

                responseBytes = _webClient.UploadData(URL, "POST", SystemConverters.StringToByteConverter(xmlMessage));
                response = Encoding.UTF8.GetString(responseBytes);


            }
            return response;
        }

        /// <summary>
        /// Download Medication from third party 
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public String DownloadMedications(string xmlMessage, String URL)
        {

            byte[] responseBytes = null;
            String response;
            using (_webClient = new WebClient())
            {

                responseBytes = _webClient.UploadData(URL, "POST", SystemConverters.StringToByteConverter(xmlMessage));
                response = Encoding.UTF8.GetString(responseBytes);


            }
            return response;
        }


         /// <summary>
         /// Download prescription from third party 
         /// </summary>
         /// <param name="patientID"></param>
         /// <returns></returns>
        public String DownloadPrescriptionForPatient(string xmlMessage, String URL)
        {
            byte[] responseBytes = null;
            String response;
            using (_webClient = new WebClient())
            {

                responseBytes = _webClient.UploadData(URL, "POST", SystemConverters.StringToByteConverter(xmlMessage));
                response = Encoding.UTF8.GetString(responseBytes);


            }
            return response;
        }


        /// <summary>
        /// Download Problems from third party 
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>

        public String DownloadProblemsForPatient(string xmlMessage, String URL)
        {
            byte[] responseBytes = null;
            String response;
            using (_webClient = new WebClient())
            {

                responseBytes = _webClient.UploadData(URL, "POST", SystemConverters.StringToByteConverter(xmlMessage));
                response = Encoding.UTF8.GetString(responseBytes);


            }
            return response;
        }



        

    }
}
