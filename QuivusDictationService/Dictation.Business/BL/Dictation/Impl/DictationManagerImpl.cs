using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dictation.DataAccess.DAO;
using Quivus.Utility.Logger;

namespace Dictation.Business.BL.Dictation
{
    public class DictationManagerImpl : IDictationManager
    {
        public void SaveDictationFile(string FileName, string RecordTimeStamp, string SessionID, string CreatedAt, string CreatedBy, byte[] FileContents)
        {
            //DDMMYYYY, Vaslid Guid, 
            try
            {
                QuivusLogger.Info("UploadDictationFile save called in DictationManagerImpl " + FileName);
                bool IsDataValid = true;
                Guid dictationID;
                DateTime recordTimeStamp;
                Guid sessionID;
                DateTime createdAt;
                int createdBy;
                string message = string.Empty;
              

                if (!Guid.TryParse(FileName, out dictationID))
                {
                    IsDataValid = false;
                    message = "Filename is not a valid GUID.\n\r";
                }
                if (!int.TryParse(CreatedBy, out createdBy))
                {
                    IsDataValid = false;
                    message += "createdBy is not a valid INT.\n\r";
                }               
                if (!Guid.TryParse(SessionID, out sessionID))
                {
                    IsDataValid = false;
                    message += "SessionID is not a valid GUID.\n\r";
                }

                bool isSuccesful = false;
                createdAt = ParseDateString(CreatedAt, out isSuccesful);

                if (!isSuccesful)
                {
                    IsDataValid = false;
                    message += "createdAt is not a valid DATE.\n\r";
                }

                recordTimeStamp = ParseDateString(CreatedAt, out isSuccesful);
                if (!isSuccesful)
                {
                    IsDataValid = false;
                    message += "createdAt is not a valid DATE.\n\r";
                }

                QuivusLogger.Info("UploadDictationFile save called in DictationManagerImpl data is valid = " + IsDataValid.ToString() + "\n\r" + message);
                if (IsDataValid == true)
                {
                    DictationDAO.SaveDictation(dictationID, FileContents, recordTimeStamp, sessionID, createdAt, createdBy);
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                   
           
            }
        }

        private DateTime ParseDateString(string strDateTime, out bool isSuccesful)
        {
            isSuccesful = true;
            DateTime dt = DateTime.MinValue;
            if (!string.IsNullOrEmpty(strDateTime) && strDateTime.Length == 8)
            {
                int day;
                int month;
                int year;

                if (!int.TryParse(strDateTime.Substring(0, 2), out day))
                {
                    isSuccesful = false;
                }

                if (!int.TryParse(strDateTime.Substring(2, 2), out month))
                {
                    isSuccesful = false;
                }

                if (!int.TryParse(strDateTime.Substring(4, 4), out year))
                {
                    isSuccesful = false;
                }


                if (isSuccesful)
                {
                    dt = new DateTime(year, month, day);
                }


            }
            else
            {
                isSuccesful = false;
            }

            return dt;

        }

        public void UpdateDictationFile(string FileName, string RecordTimeStamp, string SessionID, string CreatedAt, string CreatedBy, byte[] FileContents)
        {
            bool IsDataValid = true;
            Guid dictationID;
            DateTime recordTimeStamp;
            Guid sessionID;
            DateTime createdAt;
            int createdBy;

            if (!Guid.TryParse(FileName, out dictationID))
            {
                IsDataValid = false;
            }
            if (!DateTime.TryParse(RecordTimeStamp, out recordTimeStamp))
            {
                IsDataValid = false;
            }
            if (!Guid.TryParse(SessionID, out sessionID))
            {
                IsDataValid = false;
            }
            if (!DateTime.TryParse(CreatedAt, out createdAt))
            {
                IsDataValid = false;
            }
            if (!int.TryParse(CreatedBy, out createdBy))
            {
                IsDataValid = false;
            }

            if (IsDataValid == true)
            {
                DictationDAO.UpdateDictation(dictationID, FileContents, recordTimeStamp, sessionID, createdAt, createdBy);
            }
        }

        public void MarkDictationAsDeleted(Guid DictationID, Guid SessionID, DateTime CreatedAt, int CreatedBy) { }

        public byte[] StreamDictationByID(string DictationID, string SessionID) 
        {
            byte[] data = null;
            bool IsDataValid = true;
            Guid dictationID;            
            Guid sessionID;
            if (!Guid.TryParse(DictationID, out dictationID))
            {
                IsDataValid = false;
            }
            if (!Guid.TryParse(DictationID, out sessionID))
            {
                IsDataValid = false;
            }

            if(IsDataValid)
            {
                 data = DictationDAO.StreamDictationByID(dictationID, sessionID);
            }
            return data;
        }
    }
}
