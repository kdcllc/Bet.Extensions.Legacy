using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace Microsoft.Extensions.DependencyInjection
{
    public class MvcDependecyResolver : System.Web.Mvc.IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver
    {
        private IServiceProvider _provider;

        public MvcDependecyResolver(IServiceProvider serviceProvider)
        {
            _provider = serviceProvider;
        }

        public IDependencyScope BeginScope()
        {
            return new MvcDependecyResolver(_provider.CreateScope().ServiceProvider);
        }

        public void Dispose()
        {
        }

        public object GetService(Type serviceType)
        {
            return _provider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _provider.GetServices(serviceType);
        }
    }
}
