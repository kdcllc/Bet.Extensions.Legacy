using System.Web;

using Microsoft.Extensions.DependencyInjection;

namespace Bet.AspNet.DependencyInjection.Legacy
{
    public static class HttpContextExtensions
    {
        public static IServiceScope GetServiceScope(this HttpContext context)
        {
            return context?.Items[Constants.ServiceScopeType] as IServiceScope;
        }

        public static IServiceScope GetWebFormsServiceScope(this HttpContext context)
        {
            return context?.Items[Constants.ServiceScopeWebForms] as IServiceScope;
        }
    }
}
