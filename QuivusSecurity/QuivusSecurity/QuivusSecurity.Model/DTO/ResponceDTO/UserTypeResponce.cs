using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusSecurity.Model.DTO.ResponceDTO
{
    [DataContract]
    public class UserTypeResponce: BaseDTO.BaseResponse
    {

        [DataMember]
        public List<UserTypeDTO> UserTypeDTOList { get; set; }

    }
}
