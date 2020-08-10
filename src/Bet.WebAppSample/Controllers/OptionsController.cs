using System.Collections.Generic;
using System.Web.Http;

using Bet.AspNet.FeatureManagement;
using Bet.WebAppSample.Options;
using Bet.WebAppSample.Services;

using Microsoft.Extensions.Options;

namespace Bet.WebAppSample.Controllers
{
    public class OptionsController : ApiController
    {
        private readonly ConfigurationService _optionsService;
        private readonly IOptionsSnapshot<AppOptions> _options;

        public OptionsController(
            ConfigurationService optionsService,
            IOptionsSnapshot<AppOptions> options)
        {
            _optionsService = optionsService;
            _options = options;
        }

        // GET: api/Values
        [ApiFeatureGate(FeatureReleaseFlags.Alpha)]
        public IEnumerable<string> Get()
        {
            return new string[] { $"Message-{_options.Value.Message}", $"Refreshed-{_optionsService.Referesh()}" };
        }
    }
}
