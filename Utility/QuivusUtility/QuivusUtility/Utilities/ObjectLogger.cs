using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Quivus.Utility.Logger;

namespace QuivusUtility.Utilities
{
    public class ObjectLogger
    {
        public static void LogObject(object sourceObject)
        {
            try
            {
                String emptyString = string.Empty;

                if (sourceObject == null)
                    return;

                System.Type sourceType = sourceObject.GetType();


                foreach (PropertyInfo currentProperty in sourceType.GetProperties())
                {
                    //PropertyInfo targetObj = targetType.GetProperty(currentProperty.Name);

                    if (currentProperty.PropertyType.IsClass && !currentProperty.PropertyType.IsInstanceOfType(emptyString))
                    {
                        object sourceSub = currentProperty.GetValue(sourceObject, null);
                        if (!currentProperty.PropertyType.IsGenericType)
                            LogObject(sourceSub);
                    }
                    else
                    {
                        QuivusLogger.Info("currentProperty :: " + currentProperty + ":: " + currentProperty.GetValue(sourceObject, null));

                    }
                }
            }
            catch
            { 
            
            }
        } // End Method 

    }
}
