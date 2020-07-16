# Bet.AspNet.LegacyHosting

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](https://raw.githubusercontent.com/kdcllc/Bet.Extensions.Legacy/master/LICENSE)
[![Build status](https://ci.appveyor.com/api/projects/status/fib71kajo91ygfrp?svg=true)](https://ci.appveyor.com/project/kdcllc/bet-extensions-legacy)
[![NuGet](https://img.shields.io/nuget/v/Bet.AspNet.LegacyHosting.svg)](https://www.nuget.org/packages?q=Bet.AspNet.LegacyHosting)
![Nuget](https://img.shields.io/nuget/dt/Bet.AspNet.LegacyHosting)
[![feedz.io](https://img.shields.io/badge/endpoint.svg?url=https://f.feedz.io/kdcllc/kdcllc/shield/Bet.AspNet.LegacyHosting/latest)](https://f.feedz.io/kdcllc/kdcllc/packages/Bet.AspNet.LegacyHosting/latest/download)

*Note: Pre-release packages are distributed via [feedz.io](https://f.feedz.io/kdcllc/kdcllc/nuget/index.json).*

This library provides with a way to register `Microsoft.Extensions.DependecyInjection` library into the older version of Asp.Net Application in preparation for the migration.

There are many enterprise and small businesses that run their websites from Asp.Net framework and haven't made the transition to AspNetCore. This library attempts to provide with ability for gradual migration of the functionality.

## Install

```csharp
    dotnet add package Bet.AspNet.LegacyHosting
```

## Usage

In `Global.asax.cs` add the following:
```csharp
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
```

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