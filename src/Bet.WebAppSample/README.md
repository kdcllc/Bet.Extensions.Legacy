# Bet.WebAppSample

The goal of this sample ASP.NET Web Application project is to demonstrate how to begin use DotNetCore libraries inside of legacy .NET Framework 4.7 projects.

## Demonstrates

- Usage of `Bet.Extensions.Options.Legacy` library to provide support for `Web.Config` configuration as `IConfiguration` source for DotNetCore. **Note:No need to support reload since web application is going to be reloaded once `Web.Config` is updated.

- Usage of for of `DotNetCore` built-in DI library: `Microsoft.Extensions.DependecyInjection` for ASP.NET Mvc4 and WebApi2 Applications. **Note: This examples uses `Owin` to support request/response pipleines.

- Usage of `Bet.AspNet.FeatureManagement` library to support partial functionality that is implemented in `Microsoft.FeatureManagement.AspNetCore`

- Usage of `Bet.AspNet.LegacyHosting.AzureAppConfiguration` library that supports `Microsoft.Extensions.Configuration.AzureAppConfiguration`
    - Usage [`AppAuthentication`](https://github.com/kdcllc/AppAuthentication) dotnet global tool to authenticate if only endpoint is configured, no need to install it if ConnectionString is used to Access `Azure App Configuration`
    - `Azure App Configuration` instance created in Azure Cloud

## Usage

Add the following in `Global.asax.cs` or `OWIN` `Startup.cs`:

```csharp
    var builder = WebHost.CreateDefaultBuilder<Startup>()

                        // requires to have configuration for Azure App Configurations
                        .UseAzureAppConfiguration(
                            "WebApp:AppOptions*",
                            "WebApp:AppOptions:Flag",
                            configureAzureAppConfigOptions:
                            (options, connect, config) =>
                            {
                                options.UseFeatureFlags(flags =>
                                {
                                    flags.CacheExpirationTime = connect.CacheIntervalForFeatures;
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
                            services.AddTransient<OptionsService>();
                        })
                        .Build();

    // Configure DI for Mvc4 and WebApi2 Controllers
    builder.ConfigureMvcDependencyResolver();

    // Configure DI for WebForms
    builder.ConfigureWebFormsResolver();

    var logger = builder.Services.GetService<ILoggerFactory>().CreateLogger(nameof(Global));
```

For Azure App Configuration to work add the following:

[OWIN Startup Class Detection](https://docs.microsoft.com/en-us/aspnet/aspnet/overview/owin-and-katana/owin-startup-class-detection)

```csharp
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseAzureAppConfiguration();
        }
    }
```
