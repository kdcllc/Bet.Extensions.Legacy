using System;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;

namespace Bet.Extensions.LegacyHosting
{
    public static class HostingHostBuilderExtensions
    {
        /// <summary>
        /// Specify name for the application.
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        public static IHostBuilder UseApplicationName(this IHostBuilder hostBuilder, string applicationName)
        {
            return hostBuilder.ConfigureHostConfiguration(configBuilder =>
            {
                configBuilder.AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string, string>(
                        HostDefaults.ApplicationKey,
                        applicationName ?? throw new ArgumentNullException(nameof(applicationName)))
                });
            });
        }

        /// <summary>
        /// Specify the environment to be used by the host.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostBuilder"/> to configure.</param>
        /// <param name="environment">The environment to host the application in.</param>
        /// <returns>The <see cref="HostBuilder"/>.</returns>
        public static IHostBuilder UseEnvironment(this IHostBuilder hostBuilder, string environment)
        {
            return hostBuilder.ConfigureHostConfiguration(configBuilder =>
            {
                configBuilder.AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string, string>(
                        HostDefaults.EnvironmentKey,
                        environment ?? throw new ArgumentNullException(nameof(environment)))
                });
            });
        }

        /// <summary>
        /// Specify the content root directory to be used by the host.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostBuilder"/> to configure.</param>
        /// <param name="contentRoot">Path to root directory of the application.</param>
        /// <returns>The <see cref="HostBuilder"/>.</returns>
        public static IHostBuilder UseContentRoot(this IHostBuilder hostBuilder, string contentRoot)
        {
            return hostBuilder.ConfigureHostConfiguration(configBuilder =>
            {
                configBuilder.AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string, string>(
                        HostDefaults.ContentRootKey,
                        contentRoot ?? throw new ArgumentNullException(nameof(contentRoot)))
                });
            });
        }
    }
}
