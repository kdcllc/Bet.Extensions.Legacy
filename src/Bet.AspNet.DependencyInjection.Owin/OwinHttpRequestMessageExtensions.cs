using System.Web.Http.Dependencies;

using Bet.AspNet.DependencyInjection.Owin.Internal;

using Owin;

namespace System.Net.Http
{
    internal static class OwinHttpRequestMessageExtensions
    {
        internal static IDependencyScope GetOwinDependencyScope(this HttpRequestMessage request)
        {
            var requestContainer = request.GetOwinContext().Environment.GetRequestProvider();
            return new OwinDependencyScopeWebApiAdapter(requestContainer);
        }
    }
}
