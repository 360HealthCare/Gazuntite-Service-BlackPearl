using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Model.TypeDTOs.ELabs.QuickInput;
using LiveProvider.Utility.Logger;
using ELabManager.DataAccess.ELabManager;
using LiveProvider.Utility.Factory;
using LiveProvider.Model.TypeDTOs;

namespace ELabManager.Business.Common
{
    public class ELabStaticDataManager
    {
        ELabManagerDAO eLabDAO = null;

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab nature Types 
        /// </summary>
        /// <returns></returns>
        public List<LabNatureTypeDTO> LoadLabNatureTypes()
        {
            List<LabNatureTypeDTO> list = null;
            try
            {
                LiveProviderLogger.Info("Loading LoadLabNatureTypes");

                if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }

                list = eLabDAO.LoadLabNatureTypes();
            }
            catch
            {
                throw;
            }
            return list;
        }

        public List<LabTestPriorityTypeDTO> LoadLabTestPriorityTypes()
        {
            List<LabTestPriorityTypeDTO> list = null;
            try
            {
                LiveProviderLogger.Info("Loading LoadLabTestPriorityTypes");

                if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }

                list = eLabDAO.LoadLabTestPriorityTypes();
            }
            catch
            {
                throw;
            }
            return list;
        }

        public List<LabTestSpecimenTypeDTO> LoadLabTestSpecimenTypes()
        {
            List<LabTestSpecimenTypeDTO> list = null;
            try
            {
                LiveProviderLogger.Info("Loading LoadLabTestSpecimenTypes");

                if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }

                list = eLabDAO.LoadLabTestSpecimenTypes();
            }
            catch
            {
                throw;
            }
            return list;
        }

        public List<LabTestTypeDTO> LoadLabTestTypes()
        {
            List<LabTestTypeDTO> list = null;
            try
            {
                LiveProviderLogger.Info("Loading LoadLabTestTypes");
                if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }

                list = eLabDAO.LoadLabTestTypes();
            }
            catch
            {
                throw;
            }
            return list;
        }

        public List<LabTypeDTO> LoadLabTypes()
        {
            List<LabTypeDTO> list = null;
            try
            {
                LiveProviderLogger.Info("Loading LoadLabTypes");

                if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }

                list = eLabDAO.LoadLabTypes();
            }
            catch
            {
                throw;
            }
            return list;
        }

        public List<LabTestOrderMethodTypeDTO> LoadLabTestOrderMethodTypes()
        {
            List<LabTestOrderMethodTypeDTO> list = null;
            try
            {
                LiveProviderLogger.Info("Loading LoadLabTestOrderMethodTypes");

                if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }

                list = eLabDAO.LoadLabTestOrderMethodTypes();
            }
            catch
            {
                throw;
            }
            return list;
        }

        public List<ICDCodeDTO> LoadICDCodeByName(string queryString)
        {
            List<ICDCodeDTO> list = null;
            try
            {
                LiveProviderLogger.Info("Loading LoadICDCodeByName");

                if(eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }

                list = eLabDAO.LoadICDCodeByName(queryString);
            }
            catch
            {
                throw;
            }
            return list;
        }

        public List<LIONICLabTestTypeDTO> LoadLIONICLabTestType(int LabtestTypeID, string LabTestCodeOrName)
        {
            List<LIONICLabTestTypeDTO> TypeList = null;
            try
            {
                LiveProviderLogger.Info("Loading LoadLIONICLabTestType");

                if (eLabDAO == null)
                {
                    eLabDAO = (ELabManagerDAO)DAOFactory.GetDataAccessObject(typeof(ELabManagerDAO));
                }

                TypeList = eLabDAO.LoadLIONICLabTestType(LabtestTypeID, LabTestCodeOrName);
                //    TypeList = visitDAO.LoadLIONICLabTestType();
            }
            catch
            {
                throw;
            }
            return TypeList;
        }
    }
}
