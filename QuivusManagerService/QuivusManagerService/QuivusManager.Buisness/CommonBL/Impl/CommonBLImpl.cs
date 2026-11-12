using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.IntegratedServies.ReferenceFactory;
using QuivusManager.IntegratedServies.VisitService;
using QuivusManager.IntegratedServies.AddressBookService;


namespace QuivusManager.Buisness.CommonBL.Impl
{
   public class CommonBLImpl:ICommonBL
    {
        public string PingAllServices()
        {
           
            StringBuilder sb= new StringBuilder();
            sb.Append(PingUserManagerService()).Append(PingVisitService()).Append(PingAddressBookService());
            return sb.ToString();
        }

        public string PingUserManagerService()
        {
            string result = "";
            try
            {
                UserManagerServiceClient UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                UserServiceClient.PingService();
               result=("UserManagerService Successfully Pinged ::");
            }
            catch(Exception exp)
            {
                result=(exp.Message+" :: UserManagerService Failed to Respond ::");
            }
            return result;
        }

        public string PingVisitService()
        {
            string result = "";
            try
            {

                VisitServiceClient clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                clientVisit.PingService();
                result=("VisitService Successfully Pinged ::");
            }
            catch (Exception exp)
            {
                result=(exp.Message+" :: VisitService Failed to Respond ::");
            }
            return result;
        }

        public string PingAddressBookService()
        {
            string result = "";
            try
            {

                AddressBookServiceClient AddressBookClient = (AddressBookServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(AddressBookServiceClient));
                AddressBookClient.PingService();
                result = ("AddressBookService Successfully Pinged ::");
            }
            catch (Exception exp)
            {
                result = (exp.Message+" :: AddressBookService Failed to Respond ::");
            }
            return result;
        }

        //public string PingStreamingService()
        //{
        //    string result = "";
        //    try
        //    {

        //        StreamingServiceClient streamingClient = (StreamingServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(StreamingServiceClient));
        //        streamingClient.PingService();
        //        result = ("StreamingService Successfully Pinged ::");
        //    }
        //    catch (Exception exp)
        //    {
        //        result = (exp.Message + " :: StreamingService Failed to Respond ::");
        //    }
        //    return result;
        //}
    }
}
