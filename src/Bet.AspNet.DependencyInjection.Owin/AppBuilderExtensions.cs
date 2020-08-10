using System;

using Bet.AspNet.DependencyInjection.Owin;

namespace Owin
{
    public static class AppBuilderExtensions
    {
        public static void SetServiceProvider(this IAppBuilder app, IServiceProvider container)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.Properties[Constants.OwinApplicationContainerKey] = container ?? throw new ArgumentNullException(nameof(container));
        }

        public static IServiceProvider GetServiceProvider(this IAppBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.Properties[Constants.OwinApplicationContainerKey] as IServiceProvider;
        }
    }
}
