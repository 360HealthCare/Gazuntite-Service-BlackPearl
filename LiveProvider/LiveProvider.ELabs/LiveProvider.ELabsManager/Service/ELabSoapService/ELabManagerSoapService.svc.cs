using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LiveProvider.Model.TypeDTOs.ELabs.QuickInput;
using LiveProvider.Model.TypeDTOs;
using ELabManager.Business.Common;
using LiveProvider.Utility.Logger;
using LiveProvider.Utility.CustomExceptions;
using LiveProvider.Utility.Utilities;
using LiveProvider.Model.QuickInput;
using ELabManager.Business.BL.ELabManager;
using ELabManager.Business.BL.ELabManager.Impl;
using LiveProvider.Utility.Constant.Errors;

namespace LiveProvider.ELabsManager.Service.ELabSoapService
{
    
    public class ELabManagerSoapService : IELabManagerSoapService
    {
        ELabStaticDataManager objELabSDManager = null;
        IELabManager objELabManager = null;

        public List<LabNatureTypeDTO> LoadLabNatureTypes()
        {
            List<LabNatureTypeDTO> TypeList = null;
            try
            {
                if (objELabSDManager == null)
                {
                    objELabSDManager = new ELabStaticDataManager();
                }
                TypeList = objELabSDManager.LoadLabNatureTypes();
                LiveProviderLogger.Info("LoadLabNatureTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList; 
        }

        public List<LabTestPriorityTypeDTO> LoadLabTestPriorityTypes()
        {
            List<LabTestPriorityTypeDTO> TypeList = null;
            try
            {
                if (objELabSDManager == null)
                {
                    objELabSDManager = new ELabStaticDataManager();
                }
                TypeList = objELabSDManager.LoadLabTestPriorityTypes();
                LiveProviderLogger.Info("LoadLabTestPriorityTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList; 
        }

        public List<LabTestSpecimenTypeDTO> LoadLabTestSpecimenTypes()
        {
            List<LabTestSpecimenTypeDTO> TypeList = null;
            try
            {
                if (objELabSDManager == null)
                {
                    objELabSDManager = new ELabStaticDataManager();
                }
                TypeList = objELabSDManager.LoadLabTestSpecimenTypes();
                LiveProviderLogger.Info("LoadLabTestPriorityTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList;
        }

        public List<LabTestTypeDTO> LoadLabTestTypes()
        {
            List<LabTestTypeDTO> TypeList = null;
            try
            {
                if (objELabSDManager == null)
                {
                    objELabSDManager = new ELabStaticDataManager();
                }
                TypeList = objELabSDManager.LoadLabTestTypes();
                LiveProviderLogger.Info("LoadLabTestTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList; 
        }

        public List<LabTypeDTO> LoadLabTypes()
        {
            List<LabTypeDTO> TypeList = null;
            try
            {
                if (objELabSDManager == null)
                {
                    objELabSDManager = new ELabStaticDataManager();
                }
                TypeList = objELabSDManager.LoadLabTypes();
                LiveProviderLogger.Info("LoadLabTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList; 
        }

        public List<LabTestOrderMethodTypeDTO> LoadLabTestOrderMethodTypes()
        {
            List<LabTestOrderMethodTypeDTO> TypeList = null;
            try
            {
                if (objELabSDManager == null)
                {
                    objELabSDManager = new ELabStaticDataManager();
                }
                TypeList = objELabSDManager.LoadLabTestOrderMethodTypes();
                LiveProviderLogger.Info("LoadLabTestOrderMethodTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList;
        }

        public List<ICDCodeDTO> LoadICDCodeByName(string queryString)
        {
            List<ICDCodeDTO> TypeList = null;
            try
            {
                if (objELabSDManager == null)
                {
                    objELabSDManager = new ELabStaticDataManager();
                }
                TypeList = objELabSDManager.LoadICDCodeByName(queryString);
                LiveProviderLogger.Info("LoadLabTestOrderMethodTypes Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList; 
        }

        public List<LIONICLabTestTypeDTO> LoadLIONICLabTestType(int LabtestTypeID, string LabTestCodeOrName)
        {
            List<LIONICLabTestTypeDTO> TypeList = null;
            try
            {
                if (objELabSDManager == null)
                {
                    objELabSDManager = new ELabStaticDataManager();
                }

                TypeList = objELabSDManager.LoadLIONICLabTestType(LabtestTypeID, LabTestCodeOrName);
                //   TypeList = objVisitSDManager.LoadLIONICLabTestType();
                LiveProviderLogger.Info("LoadLIONICLabTestType Return List ::" + TypeList);
            }
            catch (InvalidArgumentException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return TypeList;
        }

        /// <summary>
        /// Author Sahibzada Sami
        /// Date 17 Oct 2011
        /// This Method Will be used to load Patient Labs by patrint Id
        /// </summary>
        /// <returns></returns>
        public List<LabDTO> LoadPatientLabsByPatientId(int PatientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected)
        {
            List<LabDTO> ReturnList = null;
            try
            {
                if (objELabManager == null)
                {
                    objELabManager = new ELabManagerImpl();
                }

                ReturnList = objELabManager.LoadPatientLabsByPatientId(PatientId, clinicBranchID, stationIDs, isAllSelected);
            }
            catch (InvalidArgumentException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return ReturnList;
        }


        public int ManagePatientLabs(LabDTO LabObject)
        {
            int Result = 0;
            try
            {
                if (objELabManager == null)
                {
                    objELabManager = new ELabManagerImpl();
                }

                Result = objELabManager.ManagePatientLabs(LabObject);

                LiveProviderLogger.Info("ManagePatientVitals Return Result ::" + Result);
            }
            catch (ParameterPopulatorException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.CustomMessage, exp.ErrorCode);
            }
            catch (RecordNotInsertException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Message, exp.Code);
            }
            catch (InvalidArgumentException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                // Compose Genral Error Code For Exception
                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_LIVE_PROVIDER_PREFIX,
                                      ErrorCodes.SECTION_ELAB_MANAGEMENT_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                      ErrorCodes.GENERAL_ERROR_CODE_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
            }
            return Result;
        }

        /// <summary>
        /// Author Sahibzada Sami   
        /// Date 17 Oct 2011
        /// This Method Will be used to load Patient Labs History by visit Id
        /// </summary>
        /// <returns></returns>
        public List<LabDTO> LoadPatientLabsByVisit(int VisitId)
        {
            List<LabDTO> List = null;
            try
            {
                if (objELabManager == null)
                {
                    objELabManager = new ELabManagerImpl();
                }

                List = objELabManager.LoadPatientLabsByVisit(VisitId, false);

                LiveProviderLogger.Info("LoadPatientVitalByVisit Return List ::" + List);
            }
            catch (InvalidArgumentException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return List;
        }

        /// <summary>
        /// Author Sami
        /// Date 17 Oct 2011
        /// This Method Will be used to load Patient Labs by patrint Id
        /// </summary>
        /// <returns></returns>                
        public List<LabDTO> LoadPatientLabsAllByPatientId(int PatientId)
        {
            List<LabDTO> ReturnList = null;
            try
            {
                if (objELabManager == null)
                {
                    objELabManager = new ELabManagerImpl();
                }

                ReturnList = objELabManager.LoadPatientLabsAllByPatientId(PatientId);
            }
            catch (InvalidArgumentException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Sami
        /// Date 19 April 2011
        /// </summary>
        /// <returns></returns>
        public List<FollowupLabsDTO> LoadPatientLabTestsForFollowupByPatient(int PatientId)
        {
            List<FollowupLabsDTO> ReturnList = null;
            try
            {
                if (objELabManager == null)
                {
                    objELabManager = new ELabManagerImpl();
                }

                ReturnList = objELabManager.LoadPatientLabTestsForFollowupByPatient(PatientId);
            }
            catch (InvalidArgumentException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return ReturnList;
        }
    }
}
