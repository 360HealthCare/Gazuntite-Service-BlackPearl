using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Quivus.Utility;

namespace Visit.DataAccess
{
    public class BaseDAO
    {
        public BaseDAO()
        {
        }

        protected static void SetValueIfNotNull(string PropertyName, int Value, Database db, DbCommand command)
        {
            if (Value > 0)
            {
                db.AddInParameter(command, PropertyName, DbType.Int32, Value);
            }
            else
            {
                db.AddInParameter(command, PropertyName, DbType.Int32, DBNull.Value);
            }
        }

        protected static void SetNullValue(string PropertyName, Database db, DbCommand command)
        {
            db.AddInParameter(command, PropertyName, DbType.Int32, DBNull.Value);
        }

        protected static void SetNullValue(string PropertyName, Database db, DbCommand command, DbType dbType)
        {
            db.AddInParameter(command, PropertyName, dbType, DBNull.Value);
        }

        protected static void SetDateIfNotNull(string PropertyName, DateTime Value, Database db, DbCommand command)
        {
            string InValidDate = "1/1/0001";

            if (!Value.ToShortDateString().Equals(InValidDate))
            {
                db.AddInParameter(command, PropertyName, DbType.DateTime, Value);
            }
            else
            {
                db.AddInParameter(command, PropertyName, DbType.DateTime, DBNull.Value);
            }
        }

        protected static void SetValueIfNotNull(string PropertyName, int Value, Database db, DbCommand command, DbType dbType)
        {
            if (Value > 0)
            {
                db.AddInParameter(command, PropertyName, dbType, Value);
            }
            else
            {
                db.AddInParameter(command, PropertyName, dbType, DBNull.Value);
            }
        }


         protected static void SetDictationIfNotNull(string PropertyName, Guid Value, Database db, DbCommand command)
        {
            if (!Value.Equals(Guid.Empty))
                db.AddInParameter(command, PropertyName, DbType.Guid, Value);
            else
                SetNullValue(PropertyName, db, command, DbType.Guid);
        }

         protected static int GetKeyAsNullIfTemplate(IDataReader objIReader, String PropertyName, int Target, bool IsTemplateVisit)
         {
             Target = 0;
             if (!IsTemplateVisit)
             {
                 if (objIReader[PropertyName] != DBNull.Value)
                 {
                     Target = int.Parse(objIReader[PropertyName].ToString());
                 }
             }
             return Target;
         }

         protected static Guid GetDictationGUIDAsNullIfTemplate(IDataReader objIReader, String PropertyName, Guid Target, bool IsTemplateVisit)
         {
             Target = Guid.Empty;

             if (!IsTemplateVisit)
             {
                 if (objIReader[PropertyName] != DBNull.Value)
                 {
                     Target = Guid.Parse(objIReader[PropertyName].ToString());
                 }
             }
             return Target;
         }

         protected static DateTime GetDateAsNullIfTemplate(IDataReader objIReader, String PropertyName, DateTime Target, bool IsTemplateVisit)
         {
             Target = DateTime.MinValue;

             if (!IsTemplateVisit)
             {
                 if (objIReader[PropertyName] != DBNull.Value)
                 {
                     Target = DateTime.Parse(objIReader[PropertyName].ToString());
                 }
             }
             return Target;
         }
    }
}
