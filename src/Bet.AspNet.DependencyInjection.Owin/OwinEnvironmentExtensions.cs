using System;
using System.Collections.Generic;

using Bet.AspNet.DependencyInjection.Owin;

using Microsoft.Extensions.DependencyInjection;

namespace Owin
{
    public static class OwinEnvironmentExtensions
    {
        public static IDisposable SetRequestProvider(this IDictionary<string, object> environment, IAppBuilder app)
        {
            if (environment == null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            var provider = app.GetServiceProvider();
            if (provider == null)
            {
                throw new InvalidOperationException("There is no application container registered to resolve a request container");
            }

            // Set request container
            environment[Constants.OwinRequestContainerEnvironmentKey] = provider;

            return provider as IDisposable;
        }

        public static IServiceProvider GetRequestProvider(this IDictionary<string, object> environment)
        {
            if (environment == null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            if (!(environment[Constants.OwinRequestContainerEnvironmentKey] is IServiceProvider provider))
            {
                return null;
            }

            return provider;
        }
    }
}
