using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Xml.Serialization;

namespace LiveProvider.Utility.Utilities
{
    public class XmlUtil
    {
        public static object ConvertFormXmlToObject(XmlNode node, object targetObj)
        {
            if (node.Attributes.Count == 0)
                return targetObj;
           

            foreach (XmlAttribute attr in node.Attributes)
            {
                string propertyName = attr.Name;
                string propertyValue = attr.Value;
                PropertyInfo pi = null;
                //PropertyInfo pi = targetObj.GetType().GetProperty(propertyName);
                PropertyInfo[] objProperties = targetObj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                MemberInfo[] mem = targetObj.GetType().GetMembers();
                foreach(PropertyInfo currentPI in objProperties)
                {
                    Attribute aTargetAttribute = Attribute.GetCustomAttribute(currentPI, typeof(XmlElementAttribute));
                    if (((XmlElementAttribute)aTargetAttribute).ElementName.ToUpper() == propertyName.ToUpper())
                    {
                        pi = currentPI;
                        break;
                    }
                   
                }

                if (pi != null)
                {
                    if (pi.PropertyType.Equals(typeof(DateTime)))
                    {
                        string objValue = propertyValue;
                        objValue = objValue.Replace("T", " ");
                        DateTime TempDT = DateTime.Parse(objValue);

                        pi.SetValue(targetObj, Convert.ChangeType(TempDT, pi.PropertyType), null);
                    }
                    else
                        pi.SetValue(targetObj, Convert.ChangeType(propertyValue, pi.PropertyType), null);
                }

                else // search if such field exists
                {
                    FieldInfo fi = targetObj.GetType().GetField(propertyName);

                    if (fi != null)
                        fi.SetValue(targetObj, Convert.ChangeType(propertyValue, fi.FieldType));
                }
            }

            return targetObj;
        }
    }
}
