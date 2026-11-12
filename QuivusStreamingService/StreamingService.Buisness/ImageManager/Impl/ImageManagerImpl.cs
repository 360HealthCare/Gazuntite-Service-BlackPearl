using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.Image;
using Quivus.Utility.Logger;
using Quivus.Utility.ProjectConstants;
using Quivus.Utility.Factory;
using StreamingService.DataAccess;
using Quivus.Utility.CustomException;
using Quivus.Utility;
using System.IO;
using Quivus.Utility.Utilities;
using System.Drawing;
using System.Drawing.Imaging;

namespace StreamingService.Buisness.ImageManager.Impl
{
    public class ImageManagerImpl : IImageManager
    {
        StreamingDAO streamingDAO = null;

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
            bool flag = true;
            StringBuilder sb = new StringBuilder();
            try
            {
                QuivusLogger.Info("ManageUserImage PARAM imageDTO:: " + imageDTO);

                if (ValidateUserImage(imageDTO, flag, sb))
                {

                    if (streamingDAO == null)
                    {
                        streamingDAO = (StreamingDAO)DAOFactory.GetDataAccessObject(typeof(StreamingDAO));
                    }

                    //M 24 Feb 2011
                    CreateThumbnail(imageDTO);

                    response = streamingDAO.InsertUserImage(imageDTO);
                }
                else
                {
                    // Validation Gets Failed Thorw Exception
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }

            }
            catch
            {
                throw;
            }
            return response;
        }


        public ImageDTO LoadUserImage(int UserID, int UserTypeID)
        {
            ImageDTO response = null;
            StringBuilder sb = new StringBuilder();

            try
            {
                QuivusLogger.Info("LoadUserImage PARAM UserID:: " + UserID + "UserTypeID:" + UserTypeID);
                if (UserID > 0 && UserTypeID > 0)
                {
                    if (streamingDAO == null)
                    {
                        streamingDAO = (StreamingDAO)DAOFactory.GetDataAccessObject(typeof(StreamingDAO));
                    }

                    response = streamingDAO.LoadUserImage(UserID, UserTypeID);
                }
                else
                {
                    // Validation Gets Failed Thorw Exception
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }

            }
            catch
            {
                throw;
            }
            return response;
        }


        /// <summary>
        /// Author Ismail :)
        /// Date 23 Feb 2011
        /// Method Used TO Validate User Image
        /// </summary>
        /// <param name="imageDTO"></param>
        /// <param name="flag"></param>
        /// <param name="sb"></param>
        /// <returns></returns>
        private static bool ValidateUserImage(ImageDTO imageDTO, bool flag, StringBuilder sb)
        {
            if (imageDTO != null) // Validate Image Only when Provided & Status Not Deleted
            {
                QuivusLogger.Info("Calling Validation On Image:: " + imageDTO);
                flag = ValidateImage(imageDTO, sb, flag);
            }
            return flag;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date Feb 14 2011
        /// Method Validate User Image Only When Provided & Status Not Deleted
        /// </summary>
        /// <param name="imageDTO"></param>
        /// <param name="sb"></param>
        /// <param name="flag"></param>
        /// <returns></returns>

        private static bool ValidateImage(ImageDTO imageDTO, StringBuilder sb, bool flag)
        {
            if (imageDTO != null && !imageDTO.IsDeleted)
            {

                if (imageDTO.UserTypeID <= 0)
                {
                    sb.Append("\n" + UserRegistrationConstant.IMAGE_USER_ID_EMPTY);
                    flag = false;
                }
                if (imageDTO.ImageTypeId <= 0)
                {
                    sb.Append("\n" + UserRegistrationConstant.IMAGE_TYPE_ID_EMPTY);
                    flag = false;
                }
                if (imageDTO.ImageData == null)
                {
                    sb.Append("\n" + UserRegistrationConstant.IMAGE_DATA_EMPTY);
                    flag = false;
                }
            }

            return flag;
        }

        /// <summary>
        /// Author Mansoor
        /// Date Feb 24 2011
        /// Create the image thumbnail in the image dto
        /// </summary>         
        /// <param name="imageDTO"></param>
        public static void CreateThumbnail(ImageDTO imageDTO)
        {
            try
            {
                Image image = System.Drawing.Image.FromStream(new MemoryStream(imageDTO.ImageData));

                Image pThumbnail = ImageUtility.ResizeImage(image, ProjectConstant.THUMBNAIL_WIDTH, ProjectConstant.THUMBNAIL_HEIGHT, true);

                using (MemoryStream ms = new MemoryStream())
                {
                    pThumbnail.Save(ms, ImageFormat.Png);

                    imageDTO.ImageThumbnailData = ms.ToArray();
                }

                pThumbnail.Dispose();
                image.Dispose();
            }
            catch 
            {
                throw;
            }
        }
    }
}
