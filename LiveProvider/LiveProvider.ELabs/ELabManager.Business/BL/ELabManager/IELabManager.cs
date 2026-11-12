using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Model.QuickInput;
using LiveProvider.Model.Response.LabManagement;
using System.Xml;

namespace ELabManager.Business.BL.ELabManager
{
    public interface IELabManager
    {

        int ManagePatientLabs(LabDTO LabObject);

        List<LabDTO> LoadPatientLabsByPatientId(int PatientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected);

        List<LabDTO> LoadPatientLabsByVisit(int VisitId, bool IsTemplateVisit);

        List<LabDTO> LoadPatientLabsAllByPatientId(int PatientId);

        List<FollowupLabsDTO> LoadPatientLabTestsForFollowupByPatient(int PatientId);

        LabPaggingResponseDTO LoadOrderByClinicBranch(int clinicBranchID, int pageNo);

        StaticDataResponse LoadStaticData();

        ResultPagingResponse LoadResultsByClinicBranch(int clinicBranchID, int pageNo);

        bool ProcessLabOrder(List<string> orderNumbers, string date);

        LabPaggingResponseDTO SearchLabOrder(int clinicBranchID, int pageNo, string searchText, int fieldID, int providerId, int userTypeId);

        ResultPagingResponse SearchLabResult(int clinicBranchID, int pageNo, string searchText, int fieldID, int providerId, int userTypeId);

        LabPaggingResponseDTO FilterPatientLabOrders(int clinicBranchId, int pageNo, string searchString);

        ResultPagingResponse FilterPatientLabResults(int clinicBranchId, int pageNo, string searchString);

        bool ProcessLabOrderByCriteria(string searchString);

        bool ManageLabOrder(XmlDocument order);
    }
}
