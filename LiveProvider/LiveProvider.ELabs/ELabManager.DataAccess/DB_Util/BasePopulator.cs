using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using LiveProvider.Utility.Constant.ProjectConstant;

namespace ELabManager.DataAccess.DB_Util
{
    public class BasePopulator
    {
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

        protected static void SetDateIfNotNull(string PropertyName, DateTime DateValue, Database db, DbCommand command)
        {
            //string InValidDate = "1/1/0001";
            //string InValidDate2 = "1/1/1901";

            //if (DateValue.ToShortDateString().Equals(InValidDate) || DateValue.ToShortDateString().Equals(InValidDate2))
            //{
            //    db.AddInParameter(command, PropertyName, DbType.DateTime, DBNull.Value);
            //}
            //else
            //{ 
            //    db.AddInParameter(command, PropertyName, DbType.DateTime, DateValue);               
            //}

            if (DateValue > ELabContant.DATE_MINIMUM)
            {
                db.AddInParameter(command, PropertyName, DbType.DateTime, DateValue);
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
    }
}
