using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using DataAccess;
using EprescriptionService.Common.DTOs;


namespace DataORM
{
    public class VendorORM
    {

        private DBAccess _dbAcces;
        private DataSet _dataset;
        public PartnerInfo PartnerInformation { get; set; }

        public  VendorORM()
        {
          _dbAcces = new DBAccess();

            
            this._dataset = this._dbAcces.GetDataFromStoredProc(DBConstants.LoadParnternInfo);

            if( this._dataset.Tables.Count < 0)
                throw new ArgumentException("VendorORM.LoadVendor: SystemConstants.LoadParnternInfo did not return correct number of tables");

            PartnerInformation.AccountUserName = this._dataset.Tables[1].Rows[1]["AccountUserName"].ToString();
            PartnerInformation.Password = this._dataset.Tables[1].Rows[1]["Password"].ToString();
            PartnerInformation.EngineVersion = this._dataset.Tables[1].Rows[1]["SecretKey"].ToString();
            PartnerInformation.SystemName = this._dataset.Tables[1].Rows[1]["SystemName"].ToString();
            PartnerInformation.SystemName = this._dataset.Tables[1].Rows[1]["SystemName"].ToString();
            PartnerInformation.PartnerID = Convert.ToInt32(this._dataset.Tables[1].Rows[1]["PartnerID"]);
           
        }

        

    }
}
