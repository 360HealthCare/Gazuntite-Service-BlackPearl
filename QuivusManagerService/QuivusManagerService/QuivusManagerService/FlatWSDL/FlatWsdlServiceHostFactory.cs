using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.ServiceModel.Activation;
using System.ServiceModel;

namespace QuivusManagerService.FlatWSDL
{
    public sealed class FlatWsdlServiceHostFactory : ServiceHostFactory
    {
        public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
        {
            return base.CreateServiceHost(constructorString, baseAddresses);
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new FlatWsdlServiceHost(serviceType, baseAddresses);
        }
    }
}
