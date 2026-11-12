using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Model.User;
using Quivus.Model.User.Patient;

using Quivus.Model.TypeDTOs;


namespace UserManager.Business.Registration
{
    public interface IQuivusRegistartionManager
    {
        UserDTO RegisterNewUser(RegistrationDTO registrationDTO);

        int RegisterBaseUser(BaseUserDTO baseUserDTO);


        List<ImageTypeDTO> LoadImageTypes();
    }
}
