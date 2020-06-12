using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bet.Extensions.LegacyHosting
{
    public class LegacyHostBuilder
    {
        private List<Action<IConfigurationBuilder>> _configureHostConfigActions = new List<Action<IConfigurationBuilder>>();

        private List<Action<HostBuilderContext, IConfigurationBuilder>> _configureAppConfigActions = new List<Action<HostBuilderContext, IConfigurationBuilder>>();

        private List<Action<HostBuilderContext, IServiceCollection>> _configureServicesActions = new List<Action<HostBuilderContext, IServiceCollection>>();

        private readonly HostBuilderContext _hostBuilderContext = new HostBuilderContext();

        private HostingEnvironment _hostingEnvironment;

        private IConfiguration _hostConfiguration;

        private IServiceProvider _appServices;

        private bool _hostBuilt;

        /// <summary>
        /// Set up the configuration for the builder itself. This will be used to initialize the <see cref="IHostEnvironment"/>
        /// for use later in the build process. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="IConfigurationBuilder"/> that will be used
        /// to construct the <see cref="IConfiguration"/> for the host.</param>
        /// <returns>The same instance of the <see cref="LegacyHostBuilder"/> for chaining.</returns>
        public LegacyHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
        {
            _configureHostConfigActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return this;
        }

        /// <summary>
        /// Sets up the configuration for the remainder of the build process and application. This can be called multiple times and
        /// the results will be additive. The results will be available at <see cref="HostBuilderContext.Configuration"/> for
        /// subsequent operations, as well as in <see cref="HostBuilderContext"/>.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="IConfigurationBuilder"/> that will be used
        /// to construct the <see cref="IConfiguration"/> for the host.</param>
        /// <returns>The same instance of the <see cref="LegacyHostBuilder"/> for chaining.</returns>
        public LegacyHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            _configureAppConfigActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return this;
        }

        /// <summary>
        /// Adds services to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="IConfigurationBuilder"/> that will be used
        /// to construct the <see cref="IConfiguration"/> for the host.</param>
        /// <returns>The same instance of the <see cref="LegacyHostBuilder"/> for chaining.</returns>
        public LegacyHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            _configureServicesActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return this;
        }

        /// <summary>
        /// Run the given actions to initialize the host. This can only be called once.
        /// </summary>
        public LegacyHostContext Build()
        {
            if (_hostBuilt)
            {
                throw new InvalidOperationException("Build can only be called once.");
            }

            _hostBuilt = true;

            BuildHostConfiguration();
            CreateHostingEnvironment();
            BuildAppConfiguration();
            CreateServiceProvider();

            return _appServices.GetRequiredService<LegacyHostContext>();
        }

        private void BuildHostConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(); // Make sure there's some default storage since there are no default providers

            foreach (var buildAction in _configureHostConfigActions)
            {
                buildAction(configBuilder);
            }

            _hostConfiguration = configBuilder.Build();
        }

        private void CreateHostingEnvironment()
        {
            var hostingEnvironment = new HostingEnvironment()
            {
                ApplicationName = _hostConfiguration[HostDefaults.ApplicationKey],
                EnvironmentName = _hostConfiguration[HostDefaults.EnvironmentKey] ?? Environments.Production,
                ContentRootPath = ResolveContentRootPath(_hostConfiguration[HostDefaults.ContentRootKey], AppContext.BaseDirectory),
            };

            if (string.IsNullOrEmpty(hostingEnvironment.ApplicationName))
            {
                // Note GetEntryAssembly returns null for the net4x console test runner.
                hostingEnvironment.ApplicationName = Assembly.GetEntryAssembly()?.GetName().Name;

                if (string.IsNullOrEmpty(hostingEnvironment.ApplicationName))
                {
                    hostingEnvironment.ApplicationName = Assembly.GetCallingAssembly()?.GetName().Name;
                }
            }

            _hostBuilderContext.HostingEnvironment = hostingEnvironment;
        }

        private void BuildAppConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(_hostBuilderContext.HostingEnvironment.ContentRootPath)
                .AddConfiguration(_hostConfiguration, shouldDisposeConfiguration: true);

            foreach (var buildAction in _configureAppConfigActions)
            {
                buildAction(_hostBuilderContext, configBuilder);
            }

            _hostBuilderContext.Configuration = configBuilder.Build();
        }

        private void CreateServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IHostEnvironment>(_hostBuilderContext.HostingEnvironment);
            services.AddSingleton(_hostBuilderContext);

            services.AddSingleton<LegacyHostContext>();

            // register configuration as factory to make it dispose with the service provider
            services.AddSingleton(_ => _hostBuilderContext.Configuration);
            services.AddSingleton(_hostBuilderContext.Configuration);
            services.AddOptions();

            foreach (var configureServicesAction in _configureServicesActions)
            {
                configureServicesAction(_hostBuilderContext, services);
            }

            _appServices = services.BuildServiceProvider();

            if (_appServices == null)
            {
                throw new InvalidOperationException($"The IServiceProviderFactory returned a null IServiceProvider.");
            }

            // resolve configuration explicitly once to mark it as resolved within the
            // service provider, ensuring it will be properly disposed with the provider
            _ = _appServices.GetService<IConfiguration>();
        }

        private string ResolveContentRootPath(string contentRootPath, string basePath)
        {
            if (string.IsNullOrEmpty(contentRootPath))
            {
                return basePath;
            }

            if (Path.IsPathRooted(contentRootPath))
            {
                return contentRootPath;
            }

            return Path.Combine(Path.GetFullPath(basePath), contentRootPath);
        }
    }
}
