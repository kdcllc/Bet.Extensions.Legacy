using System;
using System.IO;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bet.Extensions.LegacyHosting
{
    public static class LegacyHost
    {
        public static LegacyHostBuilder CreateDefaultBuilder<TStart>(string contentRoot = "", string legacyFileName = "Web.config")
        {
            contentRoot = string.IsNullOrEmpty(contentRoot) ? AppContext.BaseDirectory : contentRoot;

            var builder = new LegacyHostBuilder();
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
    }
}
