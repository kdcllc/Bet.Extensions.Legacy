using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Owin;

namespace Bet.AspNet.LegacyHosting.AzureAppConfiguration.Owin
{
    /// <summary>
    /// Working middleware based on : https://medium.com/@ofirbarak96/custom-middlewares-with-dependency-injection-in-net-framework-b18f5b935e4d.
    /// </summary>
    public class AzureAppConfigurationRefreshMiddleware
    {
        private readonly IConfiguration _configuration;

        public AzureAppConfigurationRefreshMiddleware(IConfiguration configuration)
        {
            _configuration = configuration;

            Refreshers = new List<IConfigurationRefresher>();

            if (!(configuration is IConfigurationRoot configurationRoot))
            {
                throw new InvalidOperationException("Unable to access the Azure App Configuration provider. Please ensure that it has been configured correctly.");
            }

            foreach (var provider in configurationRoot.Providers)
            {
                if (provider is IConfigurationRefresher refresher)
                {
                    Refreshers.Add(refresher);
                }
            }
        }

        public IList<IConfigurationRefresher> Refreshers { get; private set; }

        public async Task Invoke(IOwinContext context, Func<Task> next)
        {
            foreach (var refresher in Refreshers)
            {
                _ = refresher.TryRefreshAsync();
            }

            await next();
        }
    }
}
