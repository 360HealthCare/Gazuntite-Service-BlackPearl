using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quivus.Utility.Factory
{
    public class DAOFactory
    {
        public static string USER_DAO = "UserManagerService.DAO.UserDAO";

        static Object obj = null;
        private DAOFactory()
        {

        }

        /// <summary>
        /// Author Mansoor
        /// Modified by Ismail
        /// Remove String Parameter For Class Name
        /// New Parameter of Type to create/get Instance
        /// Modified Date 14 jan 2011
        /// </summary>
        /// <param name="ClassType"></param>
        /// <returns></returns>
        public static Object GetDataAccessObject(Type ClassType)
        {
            try
            {
                if (obj != null)
                {
                    // string classNameOBJ = obj.GetType().Namespace + "." + obj.GetType().Name;
                    if (!obj.GetType().Equals(ClassType))
                    {
                        // Type t = Type.GetType(className);
                        obj = Activator.CreateInstance(ClassType);
                    }

                }
                else
                {

                    // Type t = Type.GetType(className);
                    obj = Activator.CreateInstance(ClassType);
                }
                return obj;
            }
            catch (Exception exp)
            { throw exp; }
        }
    }
}