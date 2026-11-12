using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

using UserManagerService.Business.Authentication;
using UserManagerService.Business.Authentication.Impl;

/// <summary>
/// it implements the custom validation in order to provide userId based secuirty
/// </summary>

public class CustomValidator : UserNamePasswordValidator
{
    /// <summary>
    /// it validats the arguments and verify the user existence
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    public override void Validate(string userEmail, string password)
    {
        // validate arguments
        if (string.IsNullOrEmpty(userEmail))
            throw new ArgumentNullException("userName");
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException("password");        

        int userid = 0;
        IAuthenticationManager authenticationImpl = new AuthenticationManagerImpl();

        //M -  removed due to unavaliablility of security on flex
        //userid = authenticationImpl.VerifyUserEmailAndPassword(userEmail, password);
        if (userid == -1)
            throw new SecurityTokenException("Unknown username or password");
    }
}