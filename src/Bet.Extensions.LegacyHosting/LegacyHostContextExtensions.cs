using System.Web.Http;
using System.Web.Mvc;

namespace Bet.Extensions.LegacyHosting
{
    public static class LegacyHostContextExtensions
    {
        public static LegacyHostContext AddDependencyResolver(this LegacyHostContext legacyHostContext)
        {
            // Set MVC Resolver
            DependencyResolver.SetResolver(legacyHostContext.MvcDependecyResolver);

            GlobalConfiguration.Configuration.DependencyResolver = legacyHostContext.MvcDependecyResolver;

            return legacyHostContext;
        }
    }
}
