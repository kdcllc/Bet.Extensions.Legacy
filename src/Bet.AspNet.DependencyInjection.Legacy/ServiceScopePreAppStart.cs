using System.Web;

using Bet.AspNet.DependencyInjection.Legacy;

[assembly: PreApplicationStartMethod(typeof(ServiceScopePreAppStart), "Register")]

namespace Bet.AspNet.DependencyInjection.Legacy
{
    public static class ServiceScopePreAppStart
    {
        public static void Register()
        {
            // NOTE: It is not possible to un-register a HttpModule - the actual module registry in `System.Web.dll` can only be added to, not removed from.
            Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(Constants.ServiceScopeModuleType);
        }
    }
}
