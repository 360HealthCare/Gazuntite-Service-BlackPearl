using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Quivus.Model.Communication;
using Quivus.Model.Image;

namespace QuivusStreamingService.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IStreamingService" in both code and config file together.
    [ServiceContract]
    public interface IStreamingService
    {
        [OperationContract]
        bool PingService();

        [OperationContract]
        int SendEmail(EmailDTO data);

        [OperationContract]
        int InsertUserImage(ImageDTO imageDTO);

        [OperationContract]
        ImageDTO LoadUserImage(int UserID ,int UserTypeID);

        [OperationContract]
        string CallHelloWorld();

        [OperationContract]
        string CallMyName(String MyName);

    }
}
