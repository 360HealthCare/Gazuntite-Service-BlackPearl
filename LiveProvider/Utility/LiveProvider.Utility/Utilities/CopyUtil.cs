using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace LiveProvider.Utility.Utilities
{
    public class CopyUtil
    {
        public CopyUtil()
        {
        }
        public static void CloneObject<T>(object sourceObject, ref T destObject)
        {
            String emptyString = string.Empty;

            if (sourceObject == null || destObject == null)
                return;

            Type sourceType = sourceObject.GetType();
            Type targetType = destObject.GetType();

            foreach (PropertyInfo currentProperty in sourceType.GetProperties())
            {
                PropertyInfo targetObj = targetType.GetProperty(currentProperty.Name);

                if (currentProperty.PropertyType.IsClass && !currentProperty.PropertyType.IsInstanceOfType(emptyString))
                {
                    if (targetObj != null)
                    {
                        object sourceSub = currentProperty.GetValue(sourceObject, null);
                        object destSub = targetObj.GetValue(destObject, null);
                        CloneObject(sourceSub, ref destSub);
                    }
                }
                else
                {
                    if (targetObj == null)
                        continue;
                    targetObj.SetValue(destObject, currentProperty.GetValue(sourceObject, null), null);
                }
            }
        }

        public static void CopyObject<T>(object sourceObject, ref T destObject)
        {

            try
            {

                //	If either the source, or destination is null, return
                if (sourceObject == null || destObject == null)
                    return;

                //	Get the type of each object
                Type sourceType = sourceObject.GetType();
                Type targetType = destObject.GetType();

                //	Loop through the source properties
                foreach (PropertyInfo p in sourceType.GetProperties())
                {
                    //	Get the matching property in the destination object
                    PropertyInfo targetObj = targetType.GetProperty(p.Name);

                    //	If there is none, skip
                    if (targetObj == null)
                        continue;

                    targetObj.SetValue(destObject, p.GetValue(sourceObject, null), null);
                }
            }
            catch (Exception exp)
            {

                throw exp;
            }

        }
    }
}
