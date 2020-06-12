using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bet.Extensions.LegacyHosting;

namespace Bet.WebAppSample.Services
{
    public class FeedService
    {
        private readonly IHostEnvironment _hostEnvironment;

        public FeedService(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public string GetValue()
        {
            return _hostEnvironment.EnvironmentName;
        }
    }
}
