using System;
using System.Web;

using Bet.AspNet.LegacyHosting.AzureAppConfiguration.Owin;

using Microsoft.Extensions.DependencyInjection;

namespace Owin
{
    public static class AzureAppConfigurationAppBuilderExtensions
    {
        /// <summary>
        /// Add support for Azure App Configurations.
        /// </summary>
        /// <param name="app">The <see cref="IAppBuilder"/> app builder.</param>
        /// <returns></returns>
        public static IAppBuilder UseAzureAppConfiguration(this IAppBuilder app)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.Use(async (context, next) =>
            {
                var sp = context.Environment.GetRequestProvider();
                if (sp == null)
                {
                    throw new InvalidOperationException("IServiceProvider wasn't set.");
                }

                var requestDataMiddleware = sp.GetRequiredService<AzureAppConfigurationRefreshMiddleware>();
                await requestDataMiddleware.Invoke(context, next);
            });

            return app;
        }
    }
}
