using System.Web;

using Bet.AspNet.LegacyHosting.AzureAppConfiguration.Owin;

using Microsoft.Extensions.DependencyInjection;

namespace Owin
{
    public static class AzureAppConfigurationAppBuilderExtensions
    {
        public static IAppBuilder UseAzureAppConfiguration(this IAppBuilder app)
        {
            if (app is null)
            {
                throw new System.ArgumentNullException(nameof(app));
            }

            app.Use(async (context, next) =>
            {
                var requestDataMiddleware = HttpRuntime
                    .WebObjectActivator
                    .CreateScope()
                    .ServiceProvider
                    .GetRequiredService<AzureAppConfigurationRefreshMiddleware>();

                await requestDataMiddleware.Invoke(context, next);
            });

            return app;
        }
    }
}
