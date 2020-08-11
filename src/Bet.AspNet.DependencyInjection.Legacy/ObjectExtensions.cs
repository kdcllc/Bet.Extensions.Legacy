using System.Web;

namespace Bet.AspNet.DependencyInjection.Legacy
{
    public static class ObjectExtensions
    {
        public static HttpContext ToHttpContext(this object obj)
        {
            return ((HttpApplication)obj).Context;
        }
    }
}
