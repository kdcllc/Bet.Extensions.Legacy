using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LegacyServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Mvc and WebApi Controllers to DI Container.
        /// The controllers should suffixed with 'Controller'.
        /// </summary>
        /// <typeparam name="T">The type of the Startup file of the assembly.</typeparam>
        /// <param name="services">The DI Services.</param>
        /// <returns></returns>
        public static IServiceCollection AddControllersAsServices<T>(this IServiceCollection services)
        {
            var controllers = typeof(T).Assembly.GetExportedTypes()
                .Where(t => !t.IsAbstract
                    && !t.IsGenericTypeDefinition
                    && (typeof(IController).IsAssignableFrom(t)
                    || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)));

            return services.AddControllersAsServices(controllers);
        }

        /// <summary>
        /// Adds List of Types as Transient.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="controllerTypes"></param>
        /// <returns></returns>
        public static IServiceCollection AddControllersAsServices(
            this IServiceCollection services,
            IEnumerable<Type> controllerTypes)
        {
            foreach (var type in controllerTypes)
            {
                services.AddTransient(type);
            }

            return services;
        }
    }
}
