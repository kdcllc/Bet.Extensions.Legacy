using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using Bet.AspNet.DependencyInjection.Legacy;
using Bet.Extensions.LegacyHosting;

namespace Bet.AspNet.LegacyHosting
{
    public static class HostContextExtensions
    {
        /// <summary>
        /// Configure Asp.Net Mvc4 Dependency Injection.
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHost ConfigureMvcDependencyResolver(this IHost host)
        {
            var resolver = new ServiceScopeResolver(host.Services);

            // Set MVC Resolver
            DependencyResolver.SetResolver(resolver);

            // Set WebApi Resolver
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            ServiceScopeModule.SetServiceProvider(host.Services);

            return host;
        }

        /// <summary>
        /// Configure Asp.Net WebForms Dependency Injection.
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHost ConfigureWebFormsResolver(this IHost host)
        {
            HttpRuntime.WebObjectActivator = new AspNetWebFormsDependencyResolver(host.Services);
            return host;
        }
    }
}
