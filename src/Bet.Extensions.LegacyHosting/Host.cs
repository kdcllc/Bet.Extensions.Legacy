using System;
using System.IO;

using Microsoft.Extensions.Configuration;

namespace Bet.Extensions.LegacyHosting
{
    public static class Host
    {
        public static HostBuilder CreateDefaultBuilder(string contentRoot = "", string legacyFileName = "App.config")
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
