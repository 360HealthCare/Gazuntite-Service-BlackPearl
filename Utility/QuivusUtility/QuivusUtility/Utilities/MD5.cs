using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Quivus.Utility.Utilities
{
    public class MD5
    {
        public static string GetMD5Hash(string input)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            if (string.IsNullOrEmpty(input))
                input = ProjectConstant.DEFUALT_PASSWORD;
            byte[] bs = Encoding.UTF8.GetBytes(input);

            bs = x.ComputeHash(bs);
            
            StringBuilder sb = new StringBuilder();

            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2").ToLower());
            }
            string password = sb.ToString();
            return password;
        }
    }
}
