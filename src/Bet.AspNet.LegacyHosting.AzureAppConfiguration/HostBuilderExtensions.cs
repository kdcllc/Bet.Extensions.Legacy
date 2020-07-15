﻿using System;
using System.Collections.Generic;
using System.IO;

using Azure.Identity;

using Bet.AspNet.LegacyHosting.AzureAppConfiguration;
using Bet.AspNet.LegacyHosting.AzureAppConfiguration.Owin;
using Bet.Extensions.LegacyHosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;

namespace Bet.AspNet.LegacyHosting
{
    public static class HostBuilderExtensions
    {
        private static readonly string AppConfig = nameof(AppConfig);

        private static readonly Dictionary<string, string> Environments = new Dictionary<string, string>
        {
            { "Development", "dev" },
            { "Staging", "qa" },
            { "Production", "prod" }
        };

        public static IHostBuilder UseAzureAppConfiguration(
            this IHostBuilder builder,
            string appConfigiSectionName,
            string appConfigRefreshSectionName,
            Action<AzureAppConfigurationOptions>? configureAzureAppConfigOptions = default,
            Action<AppConfigurationConnectOptions, IConfiguration>? configureConnect = default)
        {
            builder.ConfigureAppConfiguration((context, configBuilder) =>
            {
                configBuilder.AddAzureAppConfiguration(options =>
                {
                    // create connection options
                    var connectOptions = new AppConfigurationConnectOptions();
                    context.Configuration.Bind(AppConfig, connectOptions);
                    configureConnect?.Invoke(connectOptions, context.Configuration);

                    if (!string.IsNullOrEmpty(connectOptions.ConnectionString))
                    {
                        options.Connect(connectOptions.ConnectionString);
                    }
                    else if (connectOptions.Endpoint != null
                        && string.IsNullOrEmpty(connectOptions.ConnectionString))
                    {
                        var credentials = new DefaultAzureCredential();
                        options.Connect(connectOptions.Endpoint, credentials);
                    }

                    options.ConfigureClientOptions(clientOptions => clientOptions.Retry.MaxRetries = 5);

                    // Load configuration values with no label, which means all of the configurations that are not specific to
                    // Environment
                    options.Select(appConfigiSectionName);

                    // Override with any configuration values specific to current hosting env
                    options.Select(appConfigiSectionName, Environments[context.HostingEnvironment.EnvironmentName]);

                    options.ConfigureRefresh(refresh =>
                    {
                        refresh
                            .Register(appConfigRefreshSectionName, refreshAll: true)
                            .Register(appConfigRefreshSectionName, Environments[context.HostingEnvironment.EnvironmentName], refreshAll: true)
                            .SetCacheExpiration(TimeSpan.FromSeconds(1));
                    });

                    configureAzureAppConfigOptions?.Invoke(options);
                });
            });

            builder.ConfigureServices((context, services) =>
            {
                services.AddTransient<AzureAppConfigurationRefreshMiddleware>();
            });

            return builder;
        }
    }
}
