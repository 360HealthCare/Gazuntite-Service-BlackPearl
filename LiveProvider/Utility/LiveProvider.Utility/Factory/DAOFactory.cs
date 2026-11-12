using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveProvider.Utility.Factory
{
    /// <summary>
    /// Author Muhammad Ismail
    /// Date 04 July 2011
    /// Factory class will be used to created DAO instances 
    /// </summary>
    public class DAOFactory
    {
        static Object obj = null;
        private DAOFactory()
        {

        }

        /// <summary>
        /// Author Ismail
        /// Remove String Parameter For Class Name
        /// New Parameter of Type to create/get Instance
        /// Modified Date 04 july 2011
        /// </summary>
        /// <param name="ClassType"></param>
        /// <returns></returns>
        public static Object GetDataAccessObject(Type ClassType)
        {
            try
            {
                if (obj != null)
                {
                    if (!obj.GetType().Equals(ClassType))
                    {
                        obj = Activator.CreateInstance(ClassType);
                    }
                }
                else
                {
                    obj = Activator.CreateInstance(ClassType);
                }
                return obj;
            }
            catch (Exception exp)
            { throw exp; }
        }
    }
}
