using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Quivus.Model.Communication;
using StreamingService.Buisness.EmailManager;
using StreamingService.Buisness.EmailManager.Impl;
using Quivus.Utility.CustomException;
using Quivus.Utility.Logger;
using QuivusUtility.Utilities;
using System.Configuration;
using System.Reflection;
using System.Data.SqlClient;
using Quivus.Model.Image;
using StreamingService.Buisness.ImageManager;
using StreamingService.Buisness.ImageManager.Impl;

namespace QuivusStreamingService.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "StreamingService" in code, svc and config file together.
    public class StreamingService : QuivusBaseService, IStreamingService 
    {

        IImageManager ImagerManager = null;

        /// <summary>
        /// Author Ali Noor
        /// Created Date 1st Feb 2011
        /// This method is used to implement send email functionality including attachement
        /// </summary>
        /// <param name=""></param>
        public int SendEmail(EmailDTO data)
        {
            int response = 0;
            IEmailManager emailMangerImpl = null;
            try
            {
                if (emailMangerImpl == null)
                {
                    emailMangerImpl = new EmailManagerImpl();
                }
                response = emailMangerImpl.SendEmailWithAttachment(data);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (ConfigurationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (TargetInvocationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }

            catch (SqlException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        public override string GetServiceVersion()
        {
            return "Development";
        }

        
        /// <summary>
        /// Author Muhammad Ismail
        /// Date Feb 23 2011
        /// This Method Will used to manager Image
        /// </summary>
        /// <param name="imageDTO"></param>
        /// <returns></returns>
        public int InsertUserImage(ImageDTO imageDTO)
        {
            int response = 0;

           
            try
            {
                if (ImagerManager == null)
                {
                    ImagerManager = new ImageManagerImpl();
                }
                response = ImagerManager.InsertUserImage(imageDTO);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (ConfigurationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (TargetInvocationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }

            catch (SqlException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }


        public ImageDTO LoadUserImage(int UserID, int UserTypeID)
        {
            ImageDTO response = null;
            
            try
            {
                if (ImagerManager == null)
                {
                    ImagerManager = new ImageManagerImpl();
                }
                response = ImagerManager.LoadUserImage(UserID, UserTypeID);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (ConfigurationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (TargetInvocationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }

            catch (SqlException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        public string CallHelloWorld()
        {
            return "Hello World";
        }


        public string CallMyName(string MyName)
        {
            return "Hello" + MyName;
        }

    }
}
