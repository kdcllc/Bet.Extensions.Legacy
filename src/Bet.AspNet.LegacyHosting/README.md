﻿# Bet.AspNet.LegacyHosting

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](https://raw.githubusercontent.com/kdcllc/Bet.Extensions.Legacy/master/LICENSE)

[![Build status](https://ci.appveyor.com/api/projects/status/fib71kajo91ygfrp?svg=true)](https://ci.appveyor.com/project/kdcllc/bet-extensions-legacy)

[![NuGet](https://img.shields.io/nuget/v/Bet.AspNet.LegacyHosting.svg)](https://www.nuget.org/packages?q=Bet.AspNet.LegacyHosting)
![Nuget](https://img.shields.io/nuget/dt/Bet.AspNet.LegacyHosting)

[![feedz.io](https://img.shields.io/badge/endpoint.svg?url=https://f.feedz.io/kdcllc/bet-extensions-resilience/shield/Bet.AspNet.LegacyHosting/latest)](https://f.feedz.io/kdcllc/kdcllc/packages/Bet.AspNet.LegacyHosting/latest/download)


## Required DotNetCore libraries to be installed on Asp.Net Web Application Project

- Microsoft.Extensions.Configuration
- Microsoft.Extensions.Configuration.FileExtensions
- Microsoft.Extensions.DependencyInjection
- Microsoft.Extensions.Logging
- Bet.Extensions.LegacyHosting

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

This IHost can also be utilized inside of `Owin`.