using System;
using System.Reflection;
using System.Web;

using Microsoft.Extensions.DependencyInjection;

namespace Bet.AspNet.DependencyInjection.Legacy
{
    public class AspNetWebFormsDependencyResolver : IServiceProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public AspNetWebFormsDependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                IServiceScope lifetimeScope;
                var currentHttpContext = HttpContext.Current;
                if (currentHttpContext != null)
                {
                    lifetimeScope = (IServiceScope)currentHttpContext.Items[typeof(IServiceScope)];
                    if (lifetimeScope == null)
                    {
                        void CleanScope(object sender, EventArgs args)
                        {
                            if (sender is HttpApplication application)
                            {
                                application.RequestCompleted -= CleanScope;
                                lifetimeScope.Dispose();
                            }
                        }

                        lifetimeScope = _serviceProvider.CreateScope();
                        currentHttpContext.Items.Add(typeof(IServiceScope), lifetimeScope);
                        currentHttpContext.ApplicationInstance.RequestCompleted += CleanScope;
                    }
                }
                else
                {
                    lifetimeScope = _serviceProvider.CreateScope();
                }

                return ActivatorUtilities.GetServiceOrCreateInstance(lifetimeScope.ServiceProvider, serviceType);
            }
            catch (InvalidOperationException)
            {
                // No public ctor available, revert to a private/internal one
                try
                {
                    return Activator.CreateInstance(
                        serviceType,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance,
                        null,
                        null,
                        null);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
