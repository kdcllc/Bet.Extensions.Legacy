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
        /// Creates Asp.Net Default registrations for Mvc5 and WebApi2 controllers.
        /// </summary>
        /// <typeparam name="TStart"></typeparam>
        /// <param name="contentRoot"></param>
        /// <param name="legacyFileName"></param>
        /// <returns></returns>
        public static HostBuilder CreateDefaultBuilder<TStart>(
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

            builder.ConfigureServices((context, services) =>
            {
                services.AddControllersAsServices<TStart>();
            });

            return builder;
        }

        /// <summary>
        /// Creates Asp.Net Hosting project.
        /// </summary>
        /// <param name="contentRoot"></param>
        /// <param name="legacyFileName"></param>
        /// <returns></returns>
        public static HostBuilder CreateDefaultBuilder(
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
