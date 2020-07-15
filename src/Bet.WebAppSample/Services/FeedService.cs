using Bet.Extensions.LegacyHosting;
using Bet.WebAppSample.Options;

using Microsoft.Extensions.Options;

namespace Bet.WebAppSample.Services
{
    public class FeedService
    {
        private readonly AppOptions _options;
        private readonly IHostEnvironment _hostEnvironment;

        public FeedService(IOptions<AppOptions> options, IHostEnvironment hostEnvironment)
        {
            _options = options.Value;
            _hostEnvironment = hostEnvironment;
        }

        public string GetValue()
        {
            return $"Environment Value: {_hostEnvironment.EnvironmentName} - Custom Option Value: {_options.TextValue}";
        }
    }
}
