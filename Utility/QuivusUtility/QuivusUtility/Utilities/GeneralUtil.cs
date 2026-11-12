using System;


namespace QuivusUtility.Utilities
{
    /// <summary>
    /// Code review and adjusted and added new methods by Baber Waqas on 1/20/2012 
    /// this is a helper class 
    /// </summary>
    public static class GeneralUtil
    {
        public static string GetUniqueNumber(int length)
        {
            Random r = new Random(GenerateId());
            string result = "" + r.Next(10000000, 99999999);
            if (result.Length > length)
            {
                result = result.Substring(0, length);
            }
            return result;

        }
        private static int GenerateId()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt16(buffer, 0);
        }

        public static bool ContainsData(Object instance)
        {

            //TODO: need to implement this one 
            return false;


        }
        /////// <summary>
        /////// vitals data checking 
        /////// </summary>
        /////// <param name="instance"></param>
        /////// <returns></returns>
        ////public static bool ContainsVitalsData(VitalDTO instance)
        ////{

        ////    try
        ////    {
        ////        var result = instance.Height + instance.Weight + instance.Temperature + instance.BPDys + instance.BPSys +
        ////                     instance.Pulse + instance.Respiration + instance.Oxygen + "";

        ////        if(result.Length >0)
        ////            return true;
        ////    }
        ////    catch
        ////    {
        ////        ;
        ////    }
         
        ////    return false;


        ////}
    }
}
