using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using Dictation.Business.BL;
using Dictation.Business.BL.Dictation;
using Quivus.Utility.Logger;
using Quivus.Util;
using QuivusUtility;
using System.Web.Configuration;

namespace QuivusDictationService
{

    public class QuivusDictationService : QuivusBaseService, IQuivusDictationService
    {
        IDictationManager objDictationManager = null;

        public void UploadFile(Stream fileContents, string fileName)
        {
            QuivusLogger.Info("UploadFile called with file name " + fileName);
            byte[] buffer = new byte[10000];
            int bytesRead, totalBytesRead = 0;
            do
            {
                bytesRead = fileContents.Read(buffer, 0, buffer.Length);
                totalBytesRead += bytesRead;
            } while (bytesRead > 0);
            Console.WriteLine("Uploaded file {0} with {1} bytes", "abc", totalBytesRead);

            QuivusLogger.Info("UploadFile call ended with file name " + fileName);
        }

        /// <summary>
        /// Uploads the dictation file.
        /// </summary>
        /// <param name="FileContents">The file contents.</param>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="RecordTimeStamp">The record time stamp.</param>
        /// <param name="SessionID">The session ID.</param>
        /// <param name="CreatedAt">The created at.</param>
        /// <param name="CreatedBy">The created by.</param>
        public void UploadDictationFile(Stream FileContents, string FileName, string RecordTimeStamp, string SessionID, string CreatedAt, string CreatedBy)
        {
            try
            {
                QuivusLogger.Info("UploadDictationFile bytes copied" + FileName);

                byte[] buffer = QuivusUtility.Converter.ReadToEnd(FileContents);

                FileStream fs = File.Create(@"D:\UploadedFiles\"+FileName+".mp3");

                fs.Write(buffer, 0, buffer.Length);

                fs.Close();

                //QuivusLogger.Info("UploadDictationFile call begin" + FileName);

                ////byte[] buffer = new byte[FileContents.Length];

                //int bytesRead = 0, totalBytesRead = 0, chunkSize = 10000;
                //int startIndex = 0;
                //long bytesLeftToRead = FileContents.Length;

                ////using (MemoryStream ms = new MemoryStream())
                ////{

                //do
                //{
                //    startIndex = totalBytesRead;

                //    //endIndex += bytesRead;
                //    if(bytesLeftToRead > chunkSize)
                //        bytesRead = FileContents.Read(buffer, startIndex, chunkSize);
                //    else
                //        bytesRead = FileContents.Read(buffer, startIndex, (int)bytesLeftToRead);

                //    totalBytesRead += bytesRead;

                //    bytesLeftToRead -= bytesRead;

                //} while (bytesRead > 0);

                //}

                //Buffer.BlockCopy(FileContents., 0, buffer, 0, buffer.Length);

                QuivusLogger.Info("UploadDictationFile bytes copied" + FileName);
                if (objDictationManager == null)
                    objDictationManager = new DictationManagerImpl();
                QuivusLogger.Info("UploadDictationFile savwe called initiated " + FileName);

                //MultipartParser mp = new MultipartParser(FileContents);

                //QuivusLogger.Info("mp.FileContents " + mp.Success);

                QuivusLogger.Info("file size " + buffer.Length);

                objDictationManager.SaveDictationFile(FileName, RecordTimeStamp, SessionID, CreatedAt, CreatedBy, buffer);

                //fs.Close();
            }
            catch (Exception ex)
            {
                QuivusLogger.Info("Error " + ex.ToString());
            }
        }

        public Stream GetDictationFile(string DictationFileID, string SessionID)
        {
            QuivusLogger.Info("GetDictationFile called for" + DictationFileID);
            if (objDictationManager == null)
                objDictationManager = new DictationManagerImpl();

            byte[] resultBytes = objDictationManager.StreamDictationByID(DictationFileID, SessionID);
            QuivusLogger.Info("StreamDictationByID bytes read: " + resultBytes.Length);
            //Change the content Type accordingly for the dictation Files.
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";

            return new MemoryStream(resultBytes);
        }


        public string Test(string FileName, string RecordTimeStamp, string SessionID, string CreatedAt, string CreatedBy)
        {
            QuivusLogger.Info("Test called with file name " + FileName);

            return "Test is Workinggggggggggggggg with param complete walayyyy ";
        }



        public string GetSomeData(string SessionID, string FileName)
        {
            return "GetSomeData is Working";
        }


        public Stream GetAudioStream(string DictationFileID)
        {
            throw new NotImplementedException();
        }
    }
}
