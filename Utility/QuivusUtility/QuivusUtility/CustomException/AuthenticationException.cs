using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quivus.Utility.CustomException
{
    /// <summary>
    /// Author Mansoor
    /// Date 14 Jan 2011
    /// </summary>
    public class AuthenticationException : Exception
    {
        public int Code { get; set; }

        public AuthenticationException(string message, int code)
            : base(message)
        {
            Code = code;
        }
    }
}