using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestClient.ServiceReference;
using System.ServiceModel;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference.UserManagerServiceClient client = new UserManagerServiceClient();

            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback =
             ((sender, certificate, chain, sslPolicyErrors) => true);

                client.ClientCredentials.UserName.UserName = "john@quivus.com";
                client.ClientCredentials.UserName.Password = "quivus123";

                string res=client.Test();
            }
            catch (Exception ex)
            {
                ShowFaultContract(ex);
                ShowExceptionDetails(ex);
            }

            Console.ReadLine();
        }

        /// <summary>
        /// method to get the core info from the exception
        /// </summary>
        /// <param name="ex"></param>
        private static void ShowFaultContract(Exception ex)
        {
            var faultContract = ex as FaultException<UserManagerServiceFault>;
            if (faultContract != null)
            {
                UserManagerServiceFault userManagerServiceFault = faultContract.Detail;
                Console.WriteLine("Fault contract detail: ");
                Console.WriteLine("Code: {0}", userManagerServiceFault.Code);
                Console.WriteLine("Fault ID: {0}", userManagerServiceFault.FaultID);
                Console.WriteLine("Message: {0}", userManagerServiceFault.FaultMessage);

            }
        }

        /// <summary>
        /// method to get the exception info
        /// </summary>
        /// <param name="ex"></param>
        private static void ShowExceptionDetails(Exception ex)
        {
            Console.WriteLine("Exception type {0} was thrown.", ex.GetType().ToString());
            Console.WriteLine("Message: '{0}'", ex.Message);
            Console.WriteLine("Source: '{0}'", ex.Source);
            if (null == ex.InnerException)
            {
                Console.WriteLine("No Inner Exception");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Inner Exception: {0}", ex.InnerException.ToString());
            }
            Console.WriteLine();
        }
    }
}
