using Microsoft.Extensions.Configuration;

namespace Bet.Extensions.LegacyHosting
{
    /// <summary>
    /// Context containing the common services on the <see cref="IHostBuilder" />. Some properties may be null until set by the <see cref="IHostBuilder" />.
    /// </summary>
    public class HostBuilderContext
    {
        /// <summary>
        /// The <see cref="IHostEnvironment" /> initialized by the <see cref="IHostBuilder" />.
        /// </summary>
        public IHostEnvironment HostingEnvironment { get; set; }

        /// <summary>
        /// The <see cref="IConfiguration" /> containing the merged configuration of the application and the <see cref="IHostBuilder" />.
        /// </summary>
        public IConfiguration Configuration { get; set; }
    }
}
