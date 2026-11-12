using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuivusSecurity.DAO.DAO.Factory
{
    public class DAOFactory
    {
        static Object obj = null;

        private DAOFactory()
        {

        }


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
