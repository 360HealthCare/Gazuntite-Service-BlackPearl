using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusModel.Security;
using System.Data;
using QuivusSecurity.DAO.Constant;
using QuivusSecurity.Logger.Logger;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using QuivusSecurity.DAO.Convertor;
using QuivusSecurity.DAO.CustomException;

namespace QuivusSecurity.DAO.DAO
{
    public class SecurityDAO
    {
        public List<AccessRightDTO> LoadRoleAccessRight(int userTypeId, int clinicBranchId)
        {
            List<AccessRightDTO> list = null;

            IDataReader objIReader = null;

            string sProc = DAOConstant.LOAD_ROLE_APPMODULE_ACCESSRIGHT_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<AccessRightDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB

                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@userTypeId", DbType.Int32, userTypeId);
                db.AddInParameter(command, "@clinicBranchId", DbType.Int32, clinicBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(AccessRightDTO));
                }
            }
            catch
            {
                QuivusLogger.Error("Exception in SecurityDAO method LoadRoleAccessRight...");
                throw;
            }

            return list;
        }



        public List<AppModuleDTO> LoadAppModules()
        {
            List<AppModuleDTO> list = null;

            IDataReader objIReader = null;

            string sProc = DAOConstant.LOAD_APPMODULES_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<AppModuleDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB

                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name



                using (objIReader = db.ExecuteReader(command))
                {
                    GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(AppModuleDTO));
                }
            }
            catch
            {
                QuivusLogger.Error("Exception in SecurityDAO method LoadAppModules...");
                throw;
            }

            return list;
        }



        /// <summary>
        /// if userId is 0 then it will update role access right otherwise it will
        /// update user access right
        /// </summary>
        /// <param name="roleARResponceDTO"></param>
        /// <param name="userTypeId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="userId"></param>
        public int ManageRoleOrUserAccessRight(int userTypeIdSeq, int userTypeAppModuleAccessRight, bool isViewAble, bool isEditable, int clinicBranchId, int userId)
        {
            int UpdateResult = 0;
            string sProc = "";

            if (userId == 0)
                sProc = DAOConstant.MANAGE_ROLE_APPMODULE_ACCESSRIGHT_SP;
            else
                sProc = DAOConstant.MANAGE_USER_APPMODULE_ACCESSRIGHT_SP;

            try
            {

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name


                if (userTypeIdSeq > 0 && userTypeAppModuleAccessRight > 0 && clinicBranchId > 0)
                {

                    db.AddInParameter(command, "@userTypeIdSeq", DbType.Int32, userTypeIdSeq);
                    db.AddInParameter(command, "@userTypeAppModuleAccessRight", DbType.Int32, userTypeAppModuleAccessRight);
                    db.AddInParameter(command, "@isViewAble", DbType.Boolean, isViewAble);
                    db.AddInParameter(command, "@isEditable", DbType.Boolean, isEditable);
                    db.AddInParameter(command, "@clinicBranchId", DbType.Int32, clinicBranchId);

                    if(sProc == DAOConstant.MANAGE_USER_APPMODULE_ACCESSRIGHT_SP)
                        db.AddInParameter(command, "@userId", DbType.Int32, userId);

                }


                if (db.ExecuteNonQuery(command) == 0)
                {
                    UpdateResult = 0;
                }
                else
                {
                    UpdateResult = 1;
                }

            }
            catch (Exception exp)
            {
                QuivusLogger.Error("Exception in SecurityDAO method ManageRoleAccessRight...");
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }





        public List<AccessRightDTO> LoadUserAccessRight(int userId, int userTypeId, int clinicBranchId)
        {
            List<AccessRightDTO> list = null;

            IDataReader objIReader = null;

            string sProc = DAOConstant.LOAD_USER_APPMODULE_ACCESSRIGHT_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<AccessRightDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB

                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@userTypeId", DbType.Int32, userTypeId);
                db.AddInParameter(command, "@clinicBranchId", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@userId", DbType.Int32, userId);

                using (objIReader = db.ExecuteReader(command))
                {
                    GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(AccessRightDTO));
                }
            }
            catch
            {
                QuivusLogger.Error("Exception in SecurityDAO method LoadUserAccessRight...");
                throw;
            }

            return list;
        }



   

        public bool InsertUserTypeApModuleAccessRight(int appModuleIdSeq, int userTypeId, int clinicBranchId, bool isViewAble, bool isEditable)
        {
            bool inserted = false;

           

            string sProc = DAOConstant.INSERT_USERTYPE_APPMODULEACCESSRIGHT_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

               

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB

                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@userTypeIdSeq", DbType.Int32, userTypeId);
                db.AddInParameter(command, "@appModuleIdSeq", DbType.Int32, appModuleIdSeq);
                db.AddInParameter(command, "@isViewAble", DbType.Boolean, isViewAble);
                db.AddInParameter(command, "@isEditable", DbType.Boolean, isEditable);
                db.AddInParameter(command, "@clinicBranchId", DbType.Int32, clinicBranchId);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(DAOConstant.RECORD_NOT_INSERTED_ERROR_MSG, DAOConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    inserted = true;
                }
            }
            catch
            {
                QuivusLogger.Error("Exception in SecurityDAO method InsertUserTypeApModuleAccessRight...");
                throw;
            }

            return inserted;
        }


       
        public bool InsertUserApModuleAccessRight(int userId, int userTypeAppModuleIdSeq, int userTypeId, int clinicBranchId, bool isViewAble, bool isEditable)
        {
            bool inserted = false;

            //586, 2, 356, 1,1,7

            string sProc = DAOConstant.INSERT_USER_ACCESS_RIGHT_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB

                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@userIdSeq", DbType.Int32, userId);
                db.AddInParameter(command, "@userTypeIdSeq", DbType.Int32, userTypeId);
                db.AddInParameter(command, "@userTypeAppModuleIdSeq", DbType.Int32, userTypeAppModuleIdSeq);
                db.AddInParameter(command, "@isViewAble", DbType.Boolean, isViewAble);
                db.AddInParameter(command, "@isEditable", DbType.Boolean, isEditable);
                db.AddInParameter(command, "@clinicBranchId", DbType.Int32, clinicBranchId);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(DAOConstant.RECORD_NOT_INSERTED_ERROR_MSG, DAOConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    inserted = true;
                }
            }
            catch
            {
                QuivusLogger.Error("Exception in SecurityDAO method InsertUserApModuleAccessRight...");
                throw;
            }

            return inserted;
        }
    }
}

