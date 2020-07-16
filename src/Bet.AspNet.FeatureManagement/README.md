# Bet.AspNet.FeatureManagement

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](https://raw.githubusercontent.com/kdcllc/Bet.Extensions.Legacy/master/LICENSE)
[![Build status](https://ci.appveyor.com/api/projects/status/fib71kajo91ygfrp?svg=true)](https://ci.appveyor.com/project/kdcllc/bet-extensions-legacy)
[![NuGet](https://img.shields.io/nuget/v/Bet.AspNet.FeatureManagement.svg)](https://www.nuget.org/packages?q=Bet.AspNet.FeatureManagement)
![Nuget](https://img.shields.io/nuget/dt/Bet.AspNet.FeatureManagement)
[![feedz.io](https://img.shields.io/badge/endpoint.svg?url=https://f.feedz.io/kdcllc/kdcllc/shield/Bet.AspNet.FeatureManagement/latest)](https://f.feedz.io/kdcllc/kdcllc/packages/Bet.AspNet.FeatureManagement/latest/download)

*Note: Pre-release packages are distributed via [feedz.io](https://f.feedz.io/kdcllc/kdcllc/nuget/index.json).*

This library was design to support ASP.NET WebForms, MVC4 and WebApi legacy projects for Microsoft.FeatureManagement.

## Install

```csharp
    dotnet add package Bet.AspNet.FeatureManagement
```

## Usage

1. Create Feature Configuration in the source

```xml
<appSettings>
    <!-- inline feature config -->
    <add key="FeatureManagement:Alpha" value="false" />
    <add key="FeatureManagement:Beta" value="true" />
  </appSettings>
```

2. WebApi2 Controller usage

```csharp
public class ValuesController : ApiController
    {
        private readonly OptionsService _optionsService;
        private readonly IOptionsSnapshot<AppOptions> _options;

        public ValuesController(OptionsService optionsService, IOptionsSnapshot<AppOptions> options)
        {
            _optionsService = optionsService;
            _options = options;
        }

        // GET: api/Values
        [ApiFeatureGate(FeatureReleaseFlags.Alpha)]
        public IEnumerable<string> Get()
        {
            return new string[] { _options.Value.Message, _optionsService.GetValue() };
        }
    }
```

2. MVC4 controller usage

```csharp
     public class FeatureManagementController : Controller
    {
        // GET: FeatureManagement
        [FeatureGate(RequirementType.All, FeatureReleaseFlags.Beta, FeatureReleaseFlags.Alpha)]
        public ActionResult Index()
        {
            return View();
        }
    }
```