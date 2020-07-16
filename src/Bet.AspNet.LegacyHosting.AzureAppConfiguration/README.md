# Bet.AspNet.LegacyHosting.AzureAppConfiguration

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](https://raw.githubusercontent.com/kdcllc/Bet.Extensions.Legacy/master/LICENSE)
[![Build status](https://ci.appveyor.com/api/projects/status/fib71kajo91ygfrp?svg=true)](https://ci.appveyor.com/project/kdcllc/bet-extensions-legacy)
[![NuGet](https://img.shields.io/nuget/v/Bet.AspNet.LegacyHosting.AzureAppConfiguration.svg)](https://www.nuget.org/packages?q=Bet.AspNet.LegacyHosting.AzureAppConfiguration)
![Nuget](https://img.shields.io/nuget/dt/Bet.AspNet.LegacyHosting.AzureAppConfiguration)
[![feedz.io](https://img.shields.io/badge/endpoint.svg?url=https://f.feedz.io/kdcllc/kdcllc/shield/Bet.AspNet.LegacyHosting.AzureAppConfiguration/latest)](https://f.feedz.io/kdcllc/kdcllc/packages/Bet.AspNet.LegacyHosting.AzureAppConfiguration/latest/download)

*Note: Pre-release packages are distributed via [feedz.io](https://f.feedz.io/kdcllc/kdcllc/nuget/index.json).*

This library was design to support ASP.NET WebForms, MVC4 and WebApi legacy projects with latest Azure App Configuration Provider.

## Install

```csharp
    dotnet add package Bet.AspNet.LegacyHosting.AzureAppConfiguration
```

## Usage

For best practices Microsoft Managed Identity must be used for Endpoint to Azure App Configuration which in turn doesn't work with local development for this reason, please refer to usage of [`AppAuthentication`](https://github.com/kdcllc/AppAuthentication) dotnet cli tool.

**NOTE: this tool is only used on the local development machine. In Azure Cloud MSI Identity is provided thru Environment variables.**

```bash
    # install
    dotnet tool install --global appauthentication

    # run this first before opening vs.net or vscode; this it will create proper environments for you.
    dotnet tool install --global appauthentication
```
