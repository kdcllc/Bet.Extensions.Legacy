using System.Collections.Generic;
using System.Web.Http;

using Bet.AspNet.FeatureManagement;
using Bet.WebAppSample.Options;
using Bet.WebAppSample.Services;

using Microsoft.Extensions.Options;

namespace Bet.WebAppSample.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly OptionsService _optionsService;
        private readonly IOptionsSnapshot<AppOptions> _options;

        public ValuesController(OptionsService optionsService, IOptionsSnapshot<AppOptions> options)
        {
            _optionsService = optionsService;
            _options = options;
        }

        // GET: api/Values
        [ApiFeatureGate(FeatureReleaseFlags.Alpha)]
        public IEnumerable<string> Get()
        {
            return new string[] { _options.Value.Message, _optionsService.GetValue() };
        }
    }
}
