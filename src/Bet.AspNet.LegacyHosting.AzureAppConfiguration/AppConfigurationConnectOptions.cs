using System;

namespace Bet.AspNet.LegacyHosting.AzureAppConfiguration
{
    public class AppConfigurationConnectOptions
    {
        public string? ConnectionString { get; set; }

        public Uri? Endpoint { get; set; }
    }
}
