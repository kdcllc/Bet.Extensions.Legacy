using System;
using System.Net.Http;
using System.Web;
using System.Web.Http;

using Bet.AspNet.DependencyInjection.Owin;
using Bet.AspNet.DependencyInjection.Owin.Internal;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Owin
{
    public static class OwinExtensions
    {
        public static IAppBuilder UseServiceProvider(this IAppBuilder app)
        {
            return app.UseServiceProvider(HttpRuntime.WebObjectActivator);
        }

        public static IAppBuilder UseServiceProvider(this IAppBuilder app, IServiceProvider provider)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            app.SetServiceProvider(provider);

            return app.Use(new Func<AppFunc, AppFunc>(nextApp => new ContainerMiddleware(nextApp, app).Invoke));
        }

        public static IAppBuilder UseWebApi2(this IAppBuilder app)
        {
            var config = new HttpConfiguration();

            // Web API configuration and services
            // https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/configuring-aspnet-web-api
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.UseDataContractJsonSerializer = false;
            jsonFormatter.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            jsonFormatter.SerializerSettings.Formatting = Formatting.None;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            return app.UseWebApi2(config);
        }

        public static IAppBuilder UseWebApi2(this IAppBuilder app, HttpConfiguration configuration)
        {
            var provider = app.GetServiceProvider();
            configuration.DependencyResolver = new OwinDependencyResolverWebApiAdapter(provider);
            HttpServer httpServer = new OwinDependencyScopeHttpServerAdapter(configuration);
            return app.UseWebApi(httpServer);
        }

        public static IAppBuilder UseWebApi2(
            this IAppBuilder app,
            HttpConfiguration configuration,
            HttpMessageHandler dispatcher)
        {
            var provider = app.GetServiceProvider();
            configuration.DependencyResolver = new OwinDependencyResolverWebApiAdapter(provider);
            HttpServer httpServer = new OwinDependencyScopeHttpServerAdapter(configuration, dispatcher);
            return app.UseWebApi(httpServer);
        }
    }
}
