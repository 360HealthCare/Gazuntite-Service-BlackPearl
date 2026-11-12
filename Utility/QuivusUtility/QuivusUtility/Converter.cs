using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QuivusUtility
{
    public static class Converter
    {
        public static byte[] ReadToEnd(System.IO.Stream input)
        {
            byte[] buffer = new byte[1024 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();



            }

        }
    }
}