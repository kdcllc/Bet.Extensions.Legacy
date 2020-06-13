using System;

namespace Bet.Extensions.LegacyHosting.Internal
{
    public class Host : IHost
    {
        public Host(IServiceProvider services)
        {
            Services = services;
        }

        public IServiceProvider Services { get; }
    }
}
