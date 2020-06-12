using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Bet.Extensions.LegacyHosting;
using Bet.WebAppSample.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Bet.WebAppSample
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            var host = LegacyHost

                                // enable injection with Mvc and WebApi Controllers
                                .CreateDefaultBuilder<Global>()
                                .ConfigureServices((context, services) =>
                                {
                                    // register our service here
                                    services.AddTransient<FeedService>();
                                })
                                .Build()

                                // Enable The Dependency Resolver for Mvc and WebApi Controllers
                                .AddDependencyResolver();

            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
