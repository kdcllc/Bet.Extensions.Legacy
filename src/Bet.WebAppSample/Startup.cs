using Owin;

namespace Bet.WebAppSample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseAzureAppConfiguration();
        }
    }
}
