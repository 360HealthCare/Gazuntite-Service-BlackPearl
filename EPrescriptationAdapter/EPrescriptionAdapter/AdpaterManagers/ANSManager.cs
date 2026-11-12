using System;
using System.Collections.Generic;
using DataORM;

namespace AdpaterManagers
{
    public class ANSManager
    {
        private VendorORM _vendorOrm;
        private URLCacheORM _urlCacheORM;


        public  ANSManager()
        {
            
            this._vendorOrm = new VendorORM();
            this._urlCacheORM = new URLCacheORM();
        }
        public String GetANSWebURL(String practiceUserName)
        {


            return @"https://cert.drfirst.com/sso/portalServices";
           

            //TODO:
            // Implement ans web url
            
            if(this._urlCacheORM.UrlCacheInfo.IsActive == true)
            {
                return this._urlCacheORM.UrlCacheInfo.URLBrowserWeb;
            }

            else
            {
                //TODO: implement database logic 

               
            }
        }

    }
}
