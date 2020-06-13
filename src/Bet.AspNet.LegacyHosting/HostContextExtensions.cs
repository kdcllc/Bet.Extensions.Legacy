using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using Bet.AspNet.DependencyInjection.Legacy;
using Bet.Extensions.LegacyHosting;

using Microsoft.Extensions.DependencyInjection;

namespace Bet.AspNet.LegacyHosting
{
    public static class HostContextExtensions
    {
        /// <summary>
        /// Configure Asp.Net Mvc5 and WebApi2 Dependency Injection.
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHost ConfigureMvcDependencyResolver(this IHost host)
        {
            var resolver = new MvcDependecyResolver(host.Services);

            // Set MVC Resolver
            DependencyResolver.SetResolver(resolver);

            GlobalConfiguration.Configuration.DependencyResolver = resolver;

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
