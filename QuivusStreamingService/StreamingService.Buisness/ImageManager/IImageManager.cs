using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.Image;

namespace StreamingService.Buisness.ImageManager
{
    public interface IImageManager
    {
        int InsertUserImage(ImageDTO imageDTO);

        ImageDTO LoadUserImage(int UserID, int UserTypeID);
    }
}
