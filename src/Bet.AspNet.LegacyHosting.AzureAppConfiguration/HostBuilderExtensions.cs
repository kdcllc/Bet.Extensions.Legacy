using System;

using Bet.AspNet.LegacyHosting.AzureAppConfiguration.Owin;
using Bet.Extensions.AzureAppConfiguration;
using Bet.Extensions.AzureAppConfiguration.Options;
using Bet.Extensions.LegacyHosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration.FeatureManagement;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Bet.AspNet.LegacyHosting
{
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Adds Azure App Configuration support for ASP.NET WebForm, MVC4 or WebApi2 application.
        /// Configuration section for <see cref="AzureAppConfigConnectOptions"/> is set to <see cref="Sections.AzureAppConfig"/>.
        /// It requires <see cref="Owin"/>  to be configured with Web Application.
        /// </summary>
        /// <param name="builder">The hosting builder.</param>
        /// <param name="appConfigiSectionName">The configuration section to load from Azure App Configuration storage.</param>
        /// <param name="appConfigRefreshSectionName">The configuration section to watch for Azure App Configuration reload.</param>
        /// <param name="throwExceptionOnStoreNotFound"></param>
        /// <param name="configureConnect"></param>
        /// <returns></returns>
        public static IHostBuilder UseAzureAppConfiguration(
            this IHostBuilder builder,
            string appConfigiSectionName,
            string appConfigRefreshSectionName,
            bool throwExceptionOnStoreNotFound = false,
            Action<AzureAppConfigConnectOptions, FiltersOptions, FeatureFlagOptions>? configureConnect = default)
        {
            builder.ConfigureAppConfiguration((context, configBuilder) =>
            {
                var currentEnv = context.HostingEnvironment.EnvironmentName;

                configBuilder.AddAzureAppConfiguration(
                    currentEnv,
                    (connect, filters, features) =>
                    {
                        filters.Sections.Add(appConfigiSectionName);
                        filters.RefresSections.Add(appConfigRefreshSectionName);
                        features.Label = currentEnv;

                        configureConnect?.Invoke(connect, filters, features);
                    },
                    throwExceptionOnStoreNotFound: throwExceptionOnStoreNotFound);
            });

            builder.ConfigureServices((context, services) =>
            {
                services.TryAddTransient<AzureAppConfigurationRefreshMiddleware>();
            });

            return builder;
        }
    }
}
