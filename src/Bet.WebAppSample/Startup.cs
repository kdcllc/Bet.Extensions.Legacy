
using Owin;

namespace Bet.WebAppSample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseServiceProvider()
               .UseWebApi2();

            app.UseAzureAppConfiguration();
        }
    }
}
