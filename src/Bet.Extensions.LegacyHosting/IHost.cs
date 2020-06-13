using System;

namespace Bet.Extensions.LegacyHosting
{
    public interface IHost
    {
        /// <summary>
        /// The programs configured services.
        /// </summary>
        IServiceProvider Services { get; }
    }
}
