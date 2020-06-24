# Bet.AspNet.DependencyInjection.Legacy

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](https://raw.githubusercontent.com/kdcllc/Bet.Extensions.Legacy/master/LICENSE)
[![Build status](https://ci.appveyor.com/api/projects/status/fib71kajo91ygfrp?svg=true)](https://ci.appveyor.com/project/kdcllc/bet-extensions-legacy)
[![NuGet](https://img.shields.io/nuget/v/Bet.AspNet.DependencyInjection.Legacy.svg)](https://www.nuget.org/packages?q=Bet.AspNet.DependencyInjection.Legacy)
![Nuget](https://img.shields.io/nuget/dt/Bet.AspNet.DependencyInjection.Legacy)
[![feedz.io](https://img.shields.io/badge/endpoint.svg?url=https://f.feedz.io/kdcllc/kdcllc/shield/Bet.AspNet.DependencyInjection.Legacy/latest)](https://f.feedz.io/kdcllc/kdcllc/packages/Bet.AspNet.DependencyInjection.Legacy/latest/download)

The goal of this library is to support gradual migration of the Asp.Net WebForms/ MVC5 or WebApi2 projects to AspNetCore.

## Install

```csharp
    dotnet add package Bet.AspNet.DependencyInjection.Legacy
```

## Usage within Asp.Net MVC5, WebApi2 or WebForms

```csharp
        void Application_Start(object sender, EventArgs e)
        {
            var builder = WebHost.CreateDefaultBuilder<Global>()
                                .ConfigureServices((context, services) =>
                                {
                                    services.AddOptions<AppOptions>()
                                    .Configure<IConfiguration>((options, config) =>
                                    {
                                        config.Bind("AppSettings:AppOptions", options);
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
```