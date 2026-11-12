using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess
{
    /// <summary>
    ///   Class:          DBAccess
    ///   Created by:     Waqas Baber on 12/30/2010
    ///   Description:    DB Access Layer
    ///   Notes:          
    ///   Revision History:
    ///   Name:           Date:        Description:
    ///   Copyright 2010 Zigron inc. All rights reserved  
    /// </summary>
    public class DBAccess
    {
        string _connString = "";
        
        /// <summary>
        /// TODO: connection string will be moved to enums
        /// </summary>
        public const string _configLocation = "ConnectionString";
        public const string _configLocationWF = "QuivusMainConnectionString";

        public enum connectionTypeEnum
        {
            Comlayer,
            QuivusMain
        }
        public DBAccess()
        {
            _connString = ConfigurationManager.AppSettings[_configLocation];
        }

        public DBAccess(connectionTypeEnum connectionType)
        {
            switch (connectionType)
            {
                case connectionTypeEnum.Comlayer:

                    _connString = ConfigurationManager.AppSettings[_configLocation];
                    break;
                case connectionTypeEnum.QuivusMain:
                    _connString = ConfigurationManager.AppSettings[_configLocationWF];
                    break;
            }
        }

        public void SaveSingleField(string tableName, string keyFieldName, string fieldName, string value, string rcopiaID)
        {
            string sql = "UPDATE " + tableName + " SET " + fieldName + "='" + value + "' WHERE " + keyFieldName + "='" + rcopiaID + "'";
            UpdateData(sql);
        }

        public void InsertData(string sql)
        {
            UpdateData(sql);
        }

        public void UpdateData(string sql)
        {
            SqlConnection conn = new SqlConnection(_connString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.ExecuteNonQuery();


            conn.Close();
        }

        public void RunStoredProc(string storedProcName)
        {
            RunInternalStoredProc(storedProcName, null);
        }

        public DataSet GetDataFromStoredProc(string storedProcName)
        {
            return GetDataInternalFromStoredProc(storedProcName, null);
        }

        public void RunStoredProc(string storedProcName, Dictionary<string, string> args)
        {
            object[] paramArray = GetParamArrayFromArgs(args);
            RunInternalStoredProc(storedProcName, paramArray);
        }

        private void RunInternalStoredProc(string storedProcName, object[] c)
        {

            SqlConnection conn = new SqlConnection(_connString);
           //Open the connection
            conn.Open();

            //Create a command 
            SqlCommand cmd = new SqlCommand(storedProcName, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (c != null)
                cmd.Parameters.AddRange(c);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        private object[] GetParamArrayFromArgs(Dictionary<string, string> args)
        {
            object[] paramArray = new object[args.Count()];
            int i = 0;
            foreach (KeyValuePair<string, string> keyVal in args)
            {
                SqlParameter p = null;
                if (keyVal.Value == null)
                    p = new SqlParameter(keyVal.Key, DBNull.Value);
                else
                    p = new SqlParameter(keyVal.Key, keyVal.Value);
                paramArray.SetValue(p, i++);
            }
            
            return paramArray;
        }

        public DataSet GetDataFromStoredProc(string storedProcName, Dictionary<string, string> args)
        {
            object[] paramArray = GetParamArrayFromArgs(args);
            return GetDataInternalFromStoredProc(storedProcName, paramArray);
        }

        private DataSet GetDataInternalFromStoredProc(string storedProcName, object[] c)
        {
            SqlConnection conn = new SqlConnection(_connString);

            //Open the connection
            conn.Open();

            //Create a command 
            SqlCommand cmd = new SqlCommand(storedProcName, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (c != null)
                cmd.Parameters.AddRange(c);
            SqlDataAdapter sqlData = new SqlDataAdapter(cmd);

            // Create the dataset and fill it
            DataSet ds = new DataSet();
            sqlData.Fill(ds);

            // Close connection
            conn.Close();

            return ds;
        }

        public DataSet GetData(string sql)
        {
            SqlConnection conn = new SqlConnection(_connString);

            //Open the connection
            conn.Open();

            //Create a command 
            SqlCommand selectSqlCommand = new SqlCommand(sql, conn);
            SqlDataAdapter sqlData = new SqlDataAdapter(selectSqlCommand);
            DataSet ds = new DataSet();
            sqlData.Fill(ds);

            conn.Close();
            return ds;
        }
    }
}
