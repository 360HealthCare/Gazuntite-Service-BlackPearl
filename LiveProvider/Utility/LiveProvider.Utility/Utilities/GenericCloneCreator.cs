using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveProvider.Utility.Utilities
{
    public class GenericCloneCreator
    {
        public static void convertObject<T>(Object source, ref T dest)
        {
            CopyUtil.CloneObject(source, ref dest);
            //return dest;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="sourceList">List of Objects to be Copied From</param>
        /// <param name="destList">List of Objects to be Copied To</param>
        /// <param name="destType">Type of Objects to be stored in Destination List use typeof() method</param>
        public static void convertList<T, V>(List<T> sourceList, ref List<V> destList, Type destType)
        {

            try
            {
                if (sourceList != null)
                {
                    foreach (var sourceObj in sourceList)
                    {

                        V destObj = (V)Activator.CreateInstance(destType);


                        CopyUtil.CloneObject(sourceObj, ref destObj);
                        destList.Add(destObj);
                    }
                }
            }
            catch (Exception exp)
            {
                throw;
            }

        }

    }
}
