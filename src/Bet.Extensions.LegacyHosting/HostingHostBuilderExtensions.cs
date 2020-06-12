using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Extensions.Configuration;

namespace Bet.Extensions.LegacyHosting
{
    public static class HostingHostBuilderExtensions
    {

        /// <summary>
        /// Specify the environment to be used by the host.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="LegacyHostBuilder"/> to configure.</param>
        /// <param name="environment">The environment to host the application in.</param>
        /// <returns>The <see cref="LegacyHostBuilder"/>.</returns>
        public static LegacyHostBuilder UseEnvironment(this LegacyHostBuilder hostBuilder, string environment)
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
        /// <param name="hostBuilder">The <see cref="LegacyHostBuilder"/> to configure.</param>
        /// <param name="contentRoot">Path to root directory of the application.</param>
        /// <returns>The <see cref="LegacyHostBuilder"/>.</returns>
        public static LegacyHostBuilder UseContentRoot(this LegacyHostBuilder hostBuilder, string contentRoot)
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
