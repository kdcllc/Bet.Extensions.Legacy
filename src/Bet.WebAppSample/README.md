# Bet.WebAppSample

The goal of this sample is to demostrate support for Migration path for older projects from .NET Framework 4.7.2 to DotNetCore.

- Support for `Web.Config`
- Support for Mvc5 and WebApi2 Dependecy injection with `Microsoft.Extensions.DependecyInjection`.

## Testing WebApi2 `https://localhost:44326/api/values`

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