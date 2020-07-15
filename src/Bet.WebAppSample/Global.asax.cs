using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Bet.AspNet.LegacyHosting;
using Bet.WebAppSample.Options;
using Bet.WebAppSample.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;

namespace Bet.WebAppSample
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            var builder = WebHost.CreateDefaultBuilder<Global>()
                                .UseAzureAppConfiguration(
                                 "WebApp:AppOptions*",
                                 "WebApp:AppOptions:Flag",
                                 configureAzureAppConfigOptions: options =>
                                 {
                                     options.UseFeatureFlags(flags =>
                                     {
                                         flags.CacheExpirationTime = TimeSpan.FromSeconds(1);
                                     });
                                 })
                                .ConfigureServices((context, services) =>
                                {
                                    services.AddOptions<AppOptions>()
                                    .Configure<IConfiguration>((options, config) =>
                                    {
                                        config.Bind("AppOptions", options);
                                    });

                                    services.AddFeatureManagement();

                                    // register our service here
                                    services.AddTransient<FeedService>();
                                })
                                .Build();

            // Configure DI for Mvc5 and WebApi2 Controllers
            builder.ConfigureMvcDependencyResolver();

            // Configure DI for WebForms
            builder.ConfigureWebFormsResolver();

            var logger = builder.Services.GetService<ILoggerFactory>().CreateLogger(nameof(WebApiConfig));

            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
