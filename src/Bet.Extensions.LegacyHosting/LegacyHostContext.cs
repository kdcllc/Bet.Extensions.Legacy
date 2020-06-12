using System;
using System.Web.Mvc;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bet.Extensions.LegacyHosting
{
    public class LegacyHostContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LegacyHostContext"/> class.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="hostBuilderContext"></param>
        public LegacyHostContext(IServiceProvider serviceProvider, HostBuilderContext hostBuilderContext)
        {
            ServiceProvider = serviceProvider;

            MvcDependecyResolver = new MvcDependecyResolver(ServiceProvider);

            HostEnvironment = hostBuilderContext.HostingEnvironment;

            Configuration = hostBuilderContext.Configuration;
        }

        /// <summary>
        /// The <see cref="IServiceProvider"/> containing the build DI services.
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// Resolved Mvc Based Container.
        /// </summary>
        public MvcDependecyResolver MvcDependecyResolver { get; set; }

        public IHostEnvironment HostEnvironment { get; set; }

        public IConfiguration Configuration { get; set; }
    }
}
