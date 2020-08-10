using System;
using System.Collections.Generic;
using System.Security;
using System.Web.Http.Dependencies;

namespace Bet.AspNet.DependencyInjection.Owin.Internal
{
    internal class OwinDependencyResolverWebApiAdapter : IDependencyResolver
    {
        private readonly IServiceProvider _appContainer;

        public OwinDependencyResolverWebApiAdapter(IServiceProvider appContainer)
        {
            _appContainer = appContainer;
        }

        [SecuritySafeCritical]
        public IDependencyScope BeginScope()
        {
            var scope = _appContainer.GetService(typeof(IServiceProvider)) as IServiceProvider;
            return new OwinDependencyScopeWebApiAdapter(scope);
        }

        [SecuritySafeCritical]
        public object GetService(Type serviceType)
        {
            return _appContainer.GetService(serviceType);
        }

        [SecuritySafeCritical]
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _appContainer.GetService(typeof(IEnumerable<>).MakeGenericType(serviceType)) as IEnumerable<object>;
        }

        [SecuritySafeCritical]
        public void Dispose()
        {
            // TODO: Figure out if the IOwinDependencyResolver instance needs to be
            // disposed by this instance.
        }
    }
}
