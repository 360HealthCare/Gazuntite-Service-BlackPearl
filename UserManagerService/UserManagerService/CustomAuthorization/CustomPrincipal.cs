using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Threading;

using UserManagerService.Business.Authentication;
using UserManagerService.Business.Authentication.Impl;

namespace UserManagerService.CustomAuthorization
{
    /// <summary>
    /// it overrides the asp.net prinicipal security, enable custom authorization policy to demand user roles
    /// </summary>
  
    class CustomPrincipal : IPrincipal
    {
        IIdentity _identity;
        string[] _roles;

        public CustomPrincipal(IIdentity identity)
        {
            _identity = identity;
        }

        // helper method for easy access (without casting)
        public static CustomPrincipal Current
        {
            get
            {
                return Thread.CurrentPrincipal as CustomPrincipal;
            }
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        // return all roles
        public string[] Roles
        {
            get
            {
                EnsureRoles();
                return _roles;
            }
        }

        // IPrincipal role check
        public bool IsInRole(string role)
        {
            EnsureRoles();

            return _roles.Contains(role);
        }

        // read Role of user from database
        protected virtual void EnsureRoles()
        {
            IAuthenticationManager authenticationImpl = new AuthenticationManagerImpl();
            //M -  removed due to unavaliablility of security on flex
            //_roles = authenticationImpl.GetUserRoles(_identity.Name);               
        }
    }
}