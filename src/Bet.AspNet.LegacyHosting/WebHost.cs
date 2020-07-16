using System;
using System.IO;

using Bet.Extensions.LegacyHosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bet.AspNet.LegacyHosting
{
    public static class WebHost
    {
        /// <summary>
        /// Creates Asp.Net Default registrations for Mvc4 and WebApi2 controllers.
        /// </summary>
        /// <typeparam name="TStart">Specify Startup file if using OWIN; otherwise use Global.asax.</typeparam>
        /// <param name="contentRoot">The application execution root folder.</param>
        /// <param name="legacyFileName">The name of the legacy file. The default is Web.Config.</param>
        /// <returns></returns>
        public static IHostBuilder CreateDefaultBuilder<TStart>(
            string contentRoot = "",
            string legacyFileName = "Web.config")
        {
            contentRoot = string.IsNullOrEmpty(contentRoot) ? AppContext.BaseDirectory : contentRoot;

            return new HostBuilder()
                .UseContentRoot(contentRoot)
                .ConfigureHostConfiguration(configBuilder =>
                {
                    configBuilder.AddLegacyConfig(Path.Combine(contentRoot, legacyFileName));
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddControllersAsServices<TStart>();
                });
        }

        /// <summary>
        /// Creates Asp.Net Default registrations for Mvc4 and WebApi2 controllers.
        /// </summary>
        /// <param name="contentRoot">The application execution root folder.</param>
        /// <param name="legacyFileName">The name of the legacy file. The default is Web.Config.</param>
        /// <returns></returns>
        public static IHostBuilder CreateDefaultBuilder(
            string contentRoot = "",
            string legacyFileName = "Web.config")
        {
            contentRoot = string.IsNullOrEmpty(contentRoot) ? AppContext.BaseDirectory : contentRoot;

            var builder = new HostBuilder();
            builder.UseContentRoot(contentRoot);
            builder.ConfigureHostConfiguration(configBuilder =>
            {
                configBuilder.AddLegacyConfig(Path.Combine(contentRoot, legacyFileName));
            });

            return builder;
        }
    }
}
