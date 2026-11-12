using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



namespace QuivusManager.Buisness.Utility
{
    /// <summary>
    /// Helper calss to copy object propeties
    /// but not neted objects
    /// </summary>
    public class CopyUtilities
    {

     
        /// <summary>
        /// Copy an object to destination object, only matching fields will be copied
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceObject">An object with matching fields of the destination object</param>
        /// <param name="destObject">Destination object, must already be created</param>
        /// 

        public static T DeepClone<T>(T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }

        public static void CloneObject<T>(object sourceObject, ref T destObject)
        {
            String emptyString = string.Empty;

            if (sourceObject == null || destObject == null)
                return;

            Type sourceType = sourceObject.GetType();
                Type targetType = destObject.GetType();
            
            PropertyInfo[] propertyInfo =     sourceType.GetProperties();

            foreach (PropertyInfo currentProperty in sourceType.GetProperties())
            {
              
                PropertyInfo targetObj = targetType.GetProperty(currentProperty.Name);

                if (currentProperty.PropertyType.IsClass && !currentProperty.PropertyType.IsInstanceOfType(emptyString))
                {
                    if (targetObj != null)
                    {
                        object sourceSub = currentProperty.GetValue(sourceObject, null);
                        object destSub = targetObj.GetValue(destObject, null);
                        if (sourceSub != null && destSub != null)
                        CloneObject(sourceSub, ref destSub);
                    }
                }
                else
                {
                    if (targetObj == null || sourceObject == null)
                        continue;
                    Object value = currentProperty.GetValue(sourceObject, null);
                    targetObj.SetValue(destObject, value, null);
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

                         }

        }


        public static void CloneObjectList<T, V, X>(object sourceObject, ref T destObject, V srcListObj, X destListObj)
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

                        if (currentProperty.PropertyType.IsGenericType)
                        {
                            Type[] gst = targetObj.PropertyType.GetGenericArguments();

                            object sourceList = currentProperty.GetValue(sourceObject, null);
                            object jo = Activator.CreateInstance(gst[0]);
                            List<V> srclst = (List<V>)sourceList;
                            object destinationList = targetObj.GetValue(destObject, null);
                            List<X> destLst = (List<X>)destinationList;

                            foreach (object src in srclst)
                            {
                                X obj = (X)Activator.CreateInstance(destListObj.GetType());
                                CloneObjectList(src, ref obj, srcListObj, destListObj);
                                destLst.Add(obj);
                            }
                        }
                        else
                        {
                            object sourceSub = currentProperty.GetValue(sourceObject, null);
                            object destSub = targetObj.GetValue(destObject, null);
                            CloneObjectList(sourceSub, ref destSub, srcListObj, destListObj);
                        }
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


        public static void CloneObjectIgnoreList<T>(object sourceObject, ref T destObject)
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

                        if (currentProperty.PropertyType.IsGenericType)
                        {
                            // Ignore List & Dictonary Types
                            // Do Not Convert 
                        }
                        else
                        {
                            object sourceSub = currentProperty.GetValue(sourceObject, null);
                            object destSub = targetObj.GetValue(destObject, null);
                            CloneObjectIgnoreList(sourceSub, ref destSub);
                        }
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


      


    }
}
