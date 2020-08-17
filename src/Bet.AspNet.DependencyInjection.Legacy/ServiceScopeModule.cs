using System;
using System.Web;

using Microsoft.Extensions.DependencyInjection;

namespace Bet.AspNet.DependencyInjection.Legacy
{
    public sealed class ServiceScopeModule : IHttpModule
    {
        private static IServiceProvider _serviceProvider;

        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnContextBeginRequest;
            context.EndRequest += OnContextEndRequest;
        }

        private static void OnContextBeginRequest(
            object sender,
            EventArgs e)
        {
            var context = sender.ToHttpContext();

            context.Items[Constants.ServiceScopeType] = _serviceProvider.CreateScope();
        }

        private static void OnContextEndRequest(
            object sender,
            EventArgs e)
        {
            var context = sender.ToHttpContext();

            if (context.Items[Constants.ServiceScopeType] is IServiceScope scope)
            {
                scope.Dispose();
            }
        }
    }
}
