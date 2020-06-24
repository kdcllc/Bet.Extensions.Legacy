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

namespace Bet.WebAppSample
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            var builder = WebHost.CreateDefaultBuilder<Global>()
                                .ConfigureServices((context, services) =>
                                {
                                    services.AddOptions<AppOptions>()
                                    .Configure<IConfiguration>((options, config) =>
                                    {
                                        config.Bind("AppOptions", options);
                                    });

                                    // register our service here
                                    services.AddTransient<FeedService>();
                                })
                                .Build();

            // Configure DI for Mvc5 and WebApi2 Controllers
            builder.ConfigureMvcDependencyResolver();

            // Configure DI for WebForms
            builder.ConfigureWebFormsResolver();

            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
