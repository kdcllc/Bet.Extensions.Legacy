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
            var builder = WebHost.CreateDefaultBuilder<Startup>()
                                .ConfigureAppConfiguration((context, configBuilder) =>
                                {
                                    configBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                                })

                                // requires to have configuration for Azure App Configurations
                                .UseAzureAppConfiguration("WebApp:AppOptions*","WebApp:AppOptions:Flag")

                                .ConfigureServices((context, services) =>
                                {
                                    services.AddChangeTokenOptions<AppOptions>("AppOptions", configureAction: (_) => { });
                                    services.AddChangeTokenOptions<AppOptions>("WebApp:AppOptions", configureAction: (_) => { });

                                    services.AddFeatureManagement();

                                    // register our service here
                                    services.AddSingleton<ConfigurationService>();
                                })
                                .Build();

            // Configure DI for Mvc4 and  WebApi2 Controllers
            builder.ConfigureMvcDependencyResolver();

            // Configure DI for WebForms
            builder.ConfigureWebFormsResolver();

            var logger = builder.Services.GetService<ILoggerFactory>().CreateLogger(nameof(Global));

            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
