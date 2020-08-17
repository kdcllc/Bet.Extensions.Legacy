using System;

using Microsoft.Extensions.DependencyInjection;

namespace Bet.AspNet.DependencyInjection.Legacy
{
    public static class Constants
    {
        public static readonly string ServiceScopeWebForms = "webforms.di";

        public static readonly Type ServiceScopeType = typeof(IServiceScope);
        public static readonly Type ServiceScopeModuleType = typeof(ServiceScopeModule);
    }
}
