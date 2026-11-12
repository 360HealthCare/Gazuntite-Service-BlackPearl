using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.EprescriptionService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.E_PRescription
{
   [DataContract]
    public  class NotificationResponse : ServiceResponse
    {
        [DataMember]
       public List<NotificationsInfoDTO> ResponseData { get; set; }
        
    }
}
