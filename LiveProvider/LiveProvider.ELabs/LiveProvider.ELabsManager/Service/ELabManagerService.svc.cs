using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LiveProvider.Model.Test;
using System.Xml.Serialization;
using LiveProvider.Model.Response.LabManagement;
using LiveProvider.Model.ELab;
using LiveProvider.Model.User.Patient;
using LiveProvider.Model.User.Provider;
using LiveProvider.Utility.Logger;
using ELabManager.Business.BL.ELabManager;
using LiveProvider.Utility.CustomExceptions;
using LiveProvider.ELabsManager.Util;
using ELabManager.Business.BL.ELabManager.Impl;
using LiveProvider.Utility.Constant.ProjectConstant;
using System.Xml;
using ELabManager.Business.Common;

namespace LiveProvider.ELabsManager.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ELabsManagerService" in code, svc and config file together.
    public class ELabManagerService : IElabManagerService
    {
        IELabManager eLabManager = null;
        ELabStaticDataManager staticDataManager = null;
        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 13 Feb 2012
        /// This method will load all lab order on the bases of clinic branch ids
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public LabPaggingResponseDTO LoadOrderByClinicBranch(string clinicBranchID, string pageNo)
        {
            LiveProviderLogger.Info("LoadOrderByClinicBranch");
            LabPaggingResponseDTO response = new LabPaggingResponseDTO();
            try
            {
                if (eLabManager == null)
                {
                    eLabManager = new ELabManagerImpl();
                }
                response = eLabManager.LoadOrderByClinicBranch(int.Parse(clinicBranchID), int.Parse(pageNo));
                response.Response = FaultHandler.HandleFault(ELabContant.SUCESS_MSG, false);
            }
            catch (InvalidArgumentException exp)
            {
                response.Response = FaultHandler.HandleFault(exp.Message, true);
            }
            catch (Exception exp)
            {
                response.Response = FaultHandler.HandleFault(exp.Message, true);
            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 20 Feb 2012
        /// This method will return search fields for order and result management on the bases of parameters.
        /// </summary>
        /// <param name="IsLabOrder"></param>
        /// <param name="IsLabResult"></param>
        /// <returns></returns>
        public StaticDataResponse LoadStaticData()
        {
            StaticDataResponse response = null;
            try
            {
                if (eLabManager == null)
                {
                    eLabManager = new ELabManagerImpl();
                }
                response = eLabManager.LoadStaticData();
                response.Response = FaultHandler.HandleFault(ELabContant.SUCESS_MSG, false);
            }
            catch(Exception exp)
            {
                response.Response = FaultHandler.HandleFault(exp.Message, true);
            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 13 Feb 2012
        /// This method will load all lab results on the bases of clinic branch ids
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public ResultPagingResponse LoadResultsByClinicBranch(string clinicBranchID, string pageNo)
        {
            LiveProviderLogger.Info("LoadResultByClinicBranch");
            ResultPagingResponse response = new ResultPagingResponse();
            try
            {
                if (eLabManager == null)
                {
                    eLabManager = new ELabManagerImpl();
                }
                response = eLabManager.LoadResultsByClinicBranch(int.Parse(clinicBranchID), int.Parse(pageNo));
                response.Response = FaultHandler.HandleFault(ELabContant.SUCESS_MSG, false);
            }
            catch (InvalidArgumentException exp)
            {
                response.Response = FaultHandler.HandleFault(exp.Message, true);
            }
            catch (Exception exp)
            {
                response.Response = FaultHandler.HandleFault(exp.Message, true);
            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 21 Feb 2012
        /// This method will process lab order
        /// </summary>
        /// <param name="orderNumbers"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public ServiceResponse ProcessLabOrder(List<string> orderNumbers, string date)
        {
            LiveProviderLogger.Info("ProcessLabOrder");
            ServiceResponse response = null;
            try
            {
                if (eLabManager == null)
                {
                    eLabManager = new ELabManagerImpl();
                }
                if (eLabManager.ProcessLabOrder(orderNumbers, date))
                {
                    response = FaultHandler.HandleFault(ELabContant.LABORDER_PROCESS_SUCESS_MSG, false);
                }
            }
            catch (Exception exp)
            {
                response = FaultHandler.HandleFault(exp.Message, true);
            }
            return response;
        }

        public List<string> Test()
        {
            List<string> testList = new List<string>();
            testList.Add("abc");
            testList.Add("abc");
            testList.Add("abc");
            testList.Add("abc");
            return testList;
        }

        public LabPaggingResponseDTO SearchLabOrder(string clinicBranchID, string pageNo, string searchText, string fieldID, string providerId, string userTypeId)
        {
            LiveProviderLogger.Info("SearchLabOrder");
            LabPaggingResponseDTO response = new LabPaggingResponseDTO();
            try
            {
                if (eLabManager == null)
                {
                    eLabManager = new ELabManagerImpl();
                }
                response = eLabManager.SearchLabOrder(int.Parse(clinicBranchID), int.Parse(pageNo),searchText,int.Parse(fieldID),int.Parse(providerId),int.Parse(userTypeId));
                response.Response = FaultHandler.HandleFault(ELabContant.SUCESS_MSG, false);
            }
            catch (InvalidArgumentException exp)
            {
                response.Response = FaultHandler.HandleFault(exp.Message, true);
            }
            catch (Exception exp)
            {
                response.Response = FaultHandler.HandleFault(exp.Message, true);
            }
            return response;
        }

        public ResultPagingResponse SearchLabResult(string clinicBranchID, string pageNo, string searchText, string fieldID, string providerId, string userTypeId)
        {
            LiveProviderLogger.Info("SearchLabResult");
            ResultPagingResponse response = new ResultPagingResponse();
            try
            {
                if (eLabManager == null)
                {
                    eLabManager = new ELabManagerImpl();
                }
                response = eLabManager.SearchLabResult(int.Parse(clinicBranchID), int.Parse(pageNo), searchText, int.Parse(fieldID), int.Parse(providerId), int.Parse(userTypeId));
                response.Response = FaultHandler.HandleFault(ELabContant.SUCESS_MSG, false);
            }
            catch (InvalidArgumentException exp)
            {
                response.Response = FaultHandler.HandleFault(exp.Message, true);
            }
            catch (Exception exp)
            {
                response.Response = FaultHandler.HandleFault(exp.Message, true);
            }
            return response;
        }


        public LabPaggingResponseDTO FilterlabOrders(string clinicBranchID, string pageNo, string searchText)
        {
            LiveProviderLogger.Info("FilterlabOrders");
            LabPaggingResponseDTO response = new LabPaggingResponseDTO();
            try
            {
                if (eLabManager == null)
                {
                    eLabManager = new ELabManagerImpl();
                }
                response = eLabManager.FilterPatientLabOrders(int.Parse(clinicBranchID), int.Parse(pageNo), searchText);
                response.Response = FaultHandler.HandleFault(ELabContant.SUCESS_MSG, false);
            }
            catch (InvalidArgumentException exp)
            {
                response.Response = FaultHandler.HandleFault(exp.Message, true);
            }
            catch (Exception exp)
            {
                response.Response = FaultHandler.HandleFault(exp.Message, true);
            }
            return response;
        }

        public ResultPagingResponse FilterPatientLabResults(string clinicBranchId, string pageNo, string searchString)
        {
            LiveProviderLogger.Info("FilterlabOrders");
            ResultPagingResponse response = new ResultPagingResponse();
            try
            {
                if (eLabManager == null)
                {
                    eLabManager = new ELabManagerImpl();
                }
                response = eLabManager.FilterPatientLabResults(int.Parse(clinicBranchId), int.Parse(pageNo), searchString);
                response.Response = FaultHandler.HandleFault(ELabContant.SUCESS_MSG, false);
            }
            catch (InvalidArgumentException exp)
            {
                response.Response = FaultHandler.HandleFault(exp.Message, true);
            }
            catch (Exception exp)
            {
                response.Response = FaultHandler.HandleFault(exp.Message, true);
            }
            return response;
        }

        public ServiceResponse ProcessLabOrderByCriteria(string searchString)
        {
            LiveProviderLogger.Info("ProcessLabOrderByCriteria");
            ServiceResponse response = null;
            try
            {
                if (eLabManager == null)
                {
                    eLabManager = new ELabManagerImpl();
                }
                if (eLabManager.ProcessLabOrderByCriteria(searchString))
                {
                    response = FaultHandler.HandleFault(ELabContant.LABORDER_PROCESS_SUCESS_MSG, false);
                }
            }
            catch (Exception exp)
            {
                response = FaultHandler.HandleFault(exp.Message, true);
            }
            return response;
        }

        public ServiceResponse ManageLabOrder(XmlDocument order)
        {
            LiveProviderLogger.Info("ManageLabOrder");
            ServiceResponse response = null;
            try
            {
                if (eLabManager == null)
                {
                    eLabManager = new ELabManagerImpl();
                }
                if (eLabManager.ManageLabOrder(order))
                {
                    response = FaultHandler.HandleFault(ELabContant.LABORDER_PROCESS_SUCESS_MSG, false);
                }
            }
            catch (Exception exp)
            {
                response = FaultHandler.HandleFault(exp.Message, true);
            }
            return response;
        }
        public TestTypeResponse LoadTestTypeByName(string searchText)
        {
            LiveProviderLogger.Info("LoadTestTypeByName");
            TestTypeResponse response = null;
            try
            {
                if (staticDataManager == null)
                {
                    staticDataManager = new ELabStaticDataManager();
                }
                response = new TestTypeResponse();
                response.TestList = staticDataManager.LoadLIONICLabTestType(ELabContant.ALL_TEST_TYPE_ID, searchText);
            }
            catch (Exception exp)
            {
                response.Response = FaultHandler.HandleFault(exp.Message, true);
            }
            return response;
        }
    }
}
