using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EprescriptionService.Utility
{

    public static class SystemConverters
    {
       /// <summary>
       /// Used to convert server message to string which may contains xml messages
       /// </summary>
       /// <param name="strBuffer"></param>
       /// <returns></returns>
       public static byte[] StringToByteConverter(String strBuffer)
       {
           var encoding = new System.Text.UTF8Encoding();
           return encoding.GetBytes(strBuffer);

       }
    }
}
