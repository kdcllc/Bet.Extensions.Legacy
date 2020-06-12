using Microsoft.Extensions.Configuration;

namespace Bet.Extensions.LegacyHosting
{
    /// <summary>
    /// Context containing the common services on the <see cref="LegacyHostBuilder" />. Some properties may be null until set by the <see cref="LegacyHostBuilder" />.
    /// </summary>
    public class HostBuilderContext
    {
        /// <summary>
        /// The <see cref="IHostEnvironment" /> initialized by the <see cref="LegacyHostBuilder" />.
        /// </summary>
        public IHostEnvironment HostingEnvironment { get; set; }

        /// <summary>
        /// The <see cref="IConfiguration" /> containing the merged configuration of the application and the <see cref="LegacyHostBuilder" />.
        /// </summary>
        public IConfiguration Configuration { get; set; }
    }
}
