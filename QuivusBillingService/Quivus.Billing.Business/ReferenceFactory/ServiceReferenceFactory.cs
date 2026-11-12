using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusBilling.IntegratedServices.UserManagerService;
using System.ServiceModel;
using QuivusBilling.IntegratedServices.VisitService;

namespace Quivus.Billing.Business.ReferenceFactory
{
    public class ServiceReferenceFactory
    {
        public static volatile object ReferenceObj = null;

        private static object _syncRoot = new object();
        private static object _syncExistingRoot = new object();

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/04/2012
        ///  This Method  used To get Service Reference 
        /// </summary>
        /// <param name="ClassType"></param>
        /// <returns></returns>
        public static Object GetServiceReference(Type ClassType)
        {
            try
            {
                if (ReferenceObj != null)
                {
                    if (!ReferenceObj.GetType().Equals(ClassType))
                    {
                        lock (_syncExistingRoot)
                        {

                            ReferenceObj = Activator.CreateInstance(ClassType);
                        }
                    }
                    else
                    {
                        VerfiyCommunicationState(ClassType); // Verify Object Should Not Be in Faultes State
                    }
                }
                else
                {
                    lock (_syncRoot)
                    {
                        if (ReferenceObj == null)
                            ReferenceObj = Activator.CreateInstance(ClassType);
                    }
                }
                return ReferenceObj;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/04/2012
        /// This Method Will be used To Verfiy Service Reference Object Should Not be in Faulted State If So Create New 
        /// </summary>
        /// <param name="ClassType"></param>
        private static void VerfiyCommunicationState(Type ClassType)
        {
            if (ReferenceObj != null && ReferenceObj.GetType().Equals(typeof(UserManagerServiceClient)))
            {
                UserManagerServiceClient obj = (UserManagerServiceClient)ReferenceObj;
                if (obj != null && obj.State == CommunicationState.Faulted)
                {
                    ReferenceObj = Activator.CreateInstance(ClassType);
                }
            }

            //if (ReferenceObj != null && ReferenceObj.GetType().Equals(typeof(AddressBookServiceClient)))
            //{
            //    AddressBookServiceClient obj = (AddressBookServiceClient)ReferenceObj;
            //    if (obj != null && obj.State == CommunicationState.Faulted)
            //    {
            //        ReferenceObj = Activator.CreateInstance(ClassType);
            //    }
            //}

            if (ReferenceObj != null && ReferenceObj.GetType().Equals(typeof(VisitServiceClient)))
            {
                VisitServiceClient obj = (VisitServiceClient)ReferenceObj;
                if (obj != null && obj.State == CommunicationState.Faulted)
                {
                    ReferenceObj = Activator.CreateInstance(ClassType);
                }
            }
        }

    }
}
