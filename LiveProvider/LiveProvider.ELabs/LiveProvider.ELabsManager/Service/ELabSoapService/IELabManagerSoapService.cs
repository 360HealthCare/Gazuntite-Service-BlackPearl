using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LiveProvider.Utility.Fault;
using LiveProvider.Model.TypeDTOs.ELabs.QuickInput;
using LiveProvider.Model.TypeDTOs;
using LiveProvider.Model.QuickInput;

namespace LiveProvider.ELabsManager.Service.ELabSoapService
{
    /// <summary>
    /// Author Sahibzada Sami uddin
    /// Date 13 Oct 2011
    /// This Service will manage labout operation
    /// </summary>
    [ServiceContract]
    public interface IELabManagerSoapService
    {

        /// <summary>
        /// Author Sahibzada Sami uddin 
        /// Date 13 Oct 2011
        /// This Method Will be used to load lab nature Types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        List<LabNatureTypeDTO> LoadLabNatureTypes();

        /// <summary>
        /// Author Sahibzada Sami uddin 
        /// Date 13 Oct 2011
        /// This Method Will be used to load lab priority Types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        List<LabTestPriorityTypeDTO> LoadLabTestPriorityTypes();

        /// <summary>
        /// Author Sahibzada Sami uddin 
        /// Date 13 Oct 2011
        /// This Method Will be used to load lab specimen Types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        List<LabTestSpecimenTypeDTO> LoadLabTestSpecimenTypes();

        /// <summary>
        /// Author Sahibzada Sami uddin 
        /// Date 13 Oct 2011
        /// This Method Will be used to load lab  Types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        List<LabTestTypeDTO> LoadLabTestTypes();

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load lab  Types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        List<LabTypeDTO> LoadLabTypes();
        /// <summary>
        /// Author Sahibzada Sami uddin 
        /// Date 13 Oct 2011
        /// This Method Will be used to load lab test order method Types 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        List<LabTestOrderMethodTypeDTO> LoadLabTestOrderMethodTypes();

        /// <summary>
        /// Author Sahibzada Sami uddin
        /// Date 13 Oct 2011
        /// This Method Will be used to load ICD Code by String 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        List<ICDCodeDTO> LoadICDCodeByName(string queryString);

        /// <summary>
        /// Author Sahibzada Sami uddin 
        /// Date 22 March 2011
        /// This Method Will be used to load lab LIONIC Test Types
        /// /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        List<LIONICLabTestTypeDTO> LoadLIONICLabTestType(int LabtestTypeID, string LabTestCodeOrName);

        /// <summary>
        /// Author Sahibzada Sami uddin 
        /// Date 13 Oct 2011
        /// This Method Will be used to Insert/Update Patient Labs.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        int ManagePatientLabs(LabDTO LabObject);

         /// <summary>
        /// Author Sahibzada Sami uddin 
        /// Date 17 Oct 2011
        /// This Method Will be used to load Patient Labs by visit.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        List<LabDTO> LoadPatientLabsByVisit(int VisitId);

        /// <summary>
        /// Author Sahibzada Sami uddin 
        /// Date 17 Oct 2011
        /// This Method Will be used to load Patient Labs by patient.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        List<LabDTO> LoadPatientLabsByPatientId(int PatientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected);

        /// <summary>
        /// Author Sami
        /// Date 17 Oct 2011
        /// This Method Will be used to load Patient Labs by patrint Id
        /// </summary>
        /// <returns></returns>         
        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        List<LabDTO> LoadPatientLabsAllByPatientId(int PatientId);

        /// <summary>
        /// Author Sami
        /// Date 17 Oct 2011
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        List<FollowupLabsDTO> LoadPatientLabTestsForFollowupByPatient(int PatientId);
    }
}
