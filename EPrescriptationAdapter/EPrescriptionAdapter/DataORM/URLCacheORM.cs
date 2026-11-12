using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataAccess;
using EprescriptionService.Common.DTOs;

namespace DataORM
{
    public class URLCacheORM
    {
        private DBAccess _dbAcces;
        private DataSet _dataset;
        public URLCacheInfo UrlCacheInfo { get; set; }

        public URLCacheORM()
        {
          _dbAcces = new DBAccess();

         


        }

        public URLCacheInfo GetUrlFromCache()
        {

            this._dataset = this._dbAcces.GetDataFromStoredProc(DBConstants.LoadAllActiveURLCacheRecord);

            if (this._dataset.Tables.Count < 0)
                throw new ArgumentException("URLCache.LoadURL: SystemConstants.LoadAllActiveURLCacheRecord did not return correct number of tables");

            URLCacheInfo UrlCacheInfo = new URLCacheInfo();
            UrlCacheInfo.ACWebBrowserURL = this._dataset.Tables[1].Rows[1]["ACWebBrowserURL"].ToString();
            UrlCacheInfo.URLBrowserWeb = this._dataset.Tables[1].Rows[1]["WebURL"].ToString();
            UrlCacheInfo.EngineUploadURL = this._dataset.Tables[1].Rows[1]["EngineUploadURL"].ToString();
            UrlCacheInfo.EngineUploadURL = this._dataset.Tables[1].Rows[1]["EngineDownloadURL"].ToString();
            UrlCacheInfo.LinkUploadUrl = this._dataset.Tables[1].Rows[1]["LinkUploadURL"].ToString();
            UrlCacheInfo.LinKDownloadUrl = this._dataset.Tables[1].Rows[1]["LinkDownloadURL"].ToString();
            // = this._dataset.Tables[1].Rows[1]["AccountUserName"].ToString();
            UrlCacheInfo.ACEngineUploadURL = this._dataset.Tables[1].Rows[1]["ACEngineUploadURL"].ToString();
            UrlCacheInfo.ACEngineDownloadURL = this._dataset.Tables[1].Rows[1]["ACEngineDownloadURL"].ToString();
            UrlCacheInfo.ACLinkDownloadURL = this._dataset.Tables[1].Rows[1]["ACLinkDownloadURL"].ToString();
            UrlCacheInfo.ACLinkUploadURL = this._dataset.Tables[1].Rows[1]["ACLinkUploadURL"].ToString();
            UrlCacheInfo.PayorEngineURL = this._dataset.Tables[1].Rows[1]["PayorEngineURL"].ToString();
            UrlCacheInfo.RegistrationURL = this._dataset.Tables[1].Rows[1]["RegistrationURL"].ToString();
            UrlCacheInfo.IsActive = Convert.ToBoolean(this._dataset.Tables[1].Rows[1]["IsActive"]);
            UrlCacheInfo.LastActivatedAt = Convert.ToDateTime(this._dataset.Tables[1].Rows[1]["LastActivatedAt"]);
   
        }
        /// <summary>
        /// Update URlCach 
        /// </summary>
        /// <returns></returns>
        public void UpdateURLCache(URLCacheInfo UrlCacheInfo)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();

            args.Add("@ACWebBrowserURL", UrlCacheInfo.ACWebBrowserURL);
            args.Add("@WebURL", UrlCacheInfo.URLBrowserWeb);
            args.Add("@EngineUploadURL", UrlCacheInfo.EngineUploadURL);
            args.Add("@EngineDownloadURL", UrlCacheInfo.EngineDownloadURL);
            args.Add("@LinkUploadUrl", UrlCacheInfo.LinkUploadUrl);
            args.Add("@LinkDownloadURL", UrlCacheInfo.LinKDownloadUrl);
            args.Add("@ACEngineUploadURL", UrlCacheInfo.ACEngineUploadURL);
            args.Add("@ACEngineDownloadURL", UrlCacheInfo.ACEngineDownloadURL);
            args.Add("@ACLinkDownloadURL", UrlCacheInfo.ACLinkDownloadURL);
            args.Add("@ACLinkUploadURL", UrlCacheInfo.ACEngineUploadURL);


            args.Add("@PayorEngineURL", UrlCacheInfo.PayorEngineURL);
            args.Add("@RegistrationURL", UrlCacheInfo.RegistrationURL);


            args.Add("@IsActive", UrlCacheInfo.IsActive.ToString());
            args.Add("@RegistrationURL", UrlCacheInfo.RegistrationURL);

            //TODO :
            // Must change the returning mechanism
            this._dbAcces.RunStoredProc(DBConstants.InsertUrlCacheRecord, args);
           

        }
    }
}
