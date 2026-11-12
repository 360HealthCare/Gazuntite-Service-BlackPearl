using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model.TypeDTOs;

namespace Quivus.Model.Image
{
    [DataContract]
    public class ImageDTO : QuivusBaseDTO
    {

        public ImageDTO()
        {
            //ImageType = new ImageTypeDTO();
        }
       
        [DataMember]
        public int UserTypeID { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int ImageId { get; set; }

        [DataMember]
        public string ImagePath { get; set; }

        [DataMember]
        public string ImageName { get; set; }

        [DataMember]
        public string ImageCaption { get; set; }

        [DataMember]
        public byte[] ImageData { get; set; }

        //M added to store image thumbnail data
        [DataMember]
        public byte[] ImageThumbnailData { get; set; }

        [DataMember]
        public int ImageTypeId { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

     

        //[DataMember]
        //private string ImagePath { get; set; }

        //[DataMember]
        //private ImageTypeDTO ImageType { get; set; }

        //[DataMember]
        //private bool IsDefault { get; set; }

        //[DataMember]
        //private DateTime CreatedDate { get; set; }
    }
}