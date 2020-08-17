using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http.Dependencies;

using Microsoft.Extensions.DependencyInjection;

namespace Bet.AspNet.DependencyInjection.Legacy
{
    public class ServiceScopeResolver : System.Web.Mvc.IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceScopeResolver(IServiceProvider provider)
        {
            _serviceProvider = provider;
        }

        public IDependencyScope BeginScope()
        {
            var scope = GetScope();
            return new ServiceScopeResolver(scope.ServiceProvider);
        }

        public void Dispose()
        {
        }

        public object GetService(Type serviceType)
        {
            var scope = GetScope();

            return scope.ServiceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var scope = GetScope();

            return scope.ServiceProvider.GetServices(serviceType);
        }

        private IServiceScope GetScope()
        {
            return HttpContext.Current.GetServiceScope() ?? _serviceProvider.CreateScope();
        }
    }
}
