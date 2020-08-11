using Owin;

namespace Bet.WebAppSample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // use this in conjunction
            app.UseAzureAppConfiguration();
        }
    }
}
