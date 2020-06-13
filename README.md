# Bet.Extensions.LegacyHosting

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](https://raw.githubusercontent.com/kdcllc/Bet.Extensions.Legacy/master/LICENSE)

[![Build status](https://ci.appveyor.com/api/projects/status/fib71kajo91ygfrp?svg=true)](https://ci.appveyor.com/project/kdcllc/bet-extensions-legacy)

[![NuGet](https://img.shields.io/nuget/v/Bet.Extensions.LegacyHosting.svg)](https://www.nuget.org/packages?q=Bet.Extensions.LegacyHosting)
![Nuget](https://img.shields.io/nuget/dt/Bet.Extensions.LegacyHosting)

[![feedz.io](https://img.shields.io/badge/endpoint.svg?url=https://f.feedz.io/kdcllc/bet-extensions-resilience/shield/Bet.Extensions.LegacyHosting/latest)](https://f.feedz.io/kdcllc/kdcllc/packages/Bet.Extensions.LegacyHosting/latest/download)

The goal of this project is to provide a gradual migration path for Asp.Net WebForms, MVC, WebApi extension to use DotNetCore Dependency Injection (DI)

## Projects

- [`Bet.AspNet.LegacyHosting`](./src/Bet.AspNet.LegacyHosting/) - Adding `Microsoft.Extensions.DependecyInjection` to existing Asp.Net Web Applications.
- [`Bet.AspNet.DependencyInjection.Legacy`](./src/Bet.AspNet.DependencyInjection.Legacy/) - Legacy Support for `Microsoft.Extensions.DependecyInjection`.
- [`Bet.Extensions.LegacyHosting`](./src/Bet.AspNet.LegacyHosting/) -Adding `Microsoft.Extensions.DependecyInjection` to Console Applications.
- [`Bet.Extensions.Options.Legacy`](./src/Bet.Extensions.Options.Legacy/) - Adding support for `Web.config` or `App.config`.

## Sample Asp.Net WebForms/MVC5/WebApi2 application

- [`Bet.WebAppSample`](./Bet.WebAppSample/) - Utilized all of the libraries example.

## Manual

```bash
    dotnet build Bet.Extensions.Legacy.Nuget.sln -c Release
    dotnet pack Bet.Extensions.Legacy.Nuget.sln -c Release -o pack
```