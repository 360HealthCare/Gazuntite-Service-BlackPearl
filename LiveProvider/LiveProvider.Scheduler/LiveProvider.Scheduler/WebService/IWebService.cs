using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LiveProvider.Model.Response;

namespace LiveProvider.Scheduler.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWebService" in both code and config file together.
    [ServiceContract]
    public interface IWebService
    {
        [OperationContract]        
        AppointmentTypeDTOResponse GetAllAppointmentTypes();

        [OperationContract]        
        AppointmentRequestStatusTypeDTOResponse GetAllAppointmentRequestStatusTypes();

        [OperationContract]        
        AppointmentCurrentStatusTypeDTOResponse GetAllAppointmentCurrentStatusTypeDTOs();
    }
}
