using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;

namespace EprescriptionService
{
    /// <summary>
    /// The ErrorBehaviorAttribute exists as a mechanism to register an error handler with a service. 
    /// This attribute takes a single type parameter. That type should implement the IErrorHandler 
    /// interface and should have a public, empty constructor. The attribute then instantiates an 
    /// instance of that error handler type and installs it into the service. It does this by 
    /// implementing the IServiceBehavior interface and then using the ApplyDispatchBehavior 
    /// method to add instances of the error handler to the service.
    /// 
    /// To use this class specify the attribute on your service class.
    /// </summary>
    public class ErrorBehaviorAttribute : Attribute, IServiceBehavior
    {
        private Type typeErrorHandler_m;

        public ErrorBehaviorAttribute(Type typeErrorHandler)
        {
            this.typeErrorHandler_m = typeErrorHandler;
        }

        public ErrorBehaviorAttribute(Type typeErrorHandler, string strUrl, bool bHandled)
            : this(typeErrorHandler)
        {
            this.strUrl_m = strUrl;
            this.bHandled_m = bHandled;
        }

        public virtual void Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
        {
            return;
        }

        public virtual void AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
        {
            return;
        }

        protected virtual IErrorHandler CreateTypeHandler()
        {
            IErrorHandler typeErrorHandler;

            try
            {
                typeErrorHandler = (IErrorHandler)Activator.CreateInstance(this.typeErrorHandler_m, this.strUrl_m, bHandled_m);
            }
            catch (MissingMethodException e)
            {
                throw new ArgumentException("The ErrorHandler type specified in the ErrorBehaviorAttribute constructor must have a public constructor with string parameter and bool parameter.", e);
            }
            catch (InvalidCastException e)
            {
                throw new ArgumentException("The ErrorHandler type specified in the ErrorBehaviorAttribute constructor must implement System.ServiceModel.Dispatcher.IErrorHandler.", e);
            }

            return typeErrorHandler;
        }

        public virtual void ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
        {
            IErrorHandler typeErrorHandler = this.CreateTypeHandler();

            foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher channelDispatcher = channelDispatcherBase as ChannelDispatcher;
                channelDispatcher.ErrorHandlers.Add(typeErrorHandler);
            }
        }

        private string strUrl_m;
        /// <summary>
        /// Specifies a url of the service, displayed in the error log
        /// </summary>
        public string Url
        {
            get
            {
                return this.strUrl_m;
            }
        }

        private bool bHandled_m;
        /// <summary>
        /// Determines if the ServiceErrorHandler will consider the exception handled
        /// </summary>
        public bool Handled
        {
            get
            {
                return this.bHandled_m;
            }
        }
    }
}
