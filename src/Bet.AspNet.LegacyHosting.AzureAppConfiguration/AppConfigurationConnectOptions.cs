using System;

namespace Bet.AspNet.LegacyHosting.AzureAppConfiguration
{
    public class AppConfigurationConnectOptions
    {
        /// <summary>
        /// The connection string to Azure App Configuration store. If present then it is used.
        /// The default is null.
        /// </summary>
        public string? ConnectionString { get; set; }

        /// <summary>
        /// The Azure App Configuration store Endpoint URI.
        /// It requires utilization of Microsoft Managed Identity setup.
        /// For local development use AppAuthentication dotnetcore cli tool.
        /// <![CDATA[
        ///    # install
        ///    dotnet tool install --global appauthentication
        ///    # run this first before opening vs.net or vscode; this it will create proper environments for you.
        ///    dotnet tool install --global appauthentication
        /// ]]>
        /// </summary>
        public Uri? Endpoint { get; set; }

        /// <summary>
        /// The cache interval for the Options specified.
        /// The default is 1 sec.
        /// </summary>
        public TimeSpan CacheIntervalForOptions { get; set; } = TimeSpan.FromSeconds(1);

        /// <summary>
        /// The cache interval for Features.
        /// The default is 1 sec.
        /// </summary>
        public TimeSpan CacheIntervalForFeatures { get; set; } = TimeSpan.FromSeconds(1);
    }
}
