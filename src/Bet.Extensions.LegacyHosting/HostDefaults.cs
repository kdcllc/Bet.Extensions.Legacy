namespace Bet.Extensions.LegacyHosting
{
    /// <summary>
    /// Constants for HostBuilder configuration keys.
    /// </summary>
    public static class HostDefaults
    {
        /// <summary>
        /// The configuration key used to set <see cref="IHostEnvironment.ApplicationName"/>.
        /// </summary>
        public static readonly string ApplicationKey = "AppSettings:ApplicationName";

        /// <summary>
        /// The configuration key used to set <see cref="IHostEnvironment.EnvironmentName"/>.
        /// </summary>
        public static readonly string EnvironmentKey = "AppSettings:Environment";

        /// <summary>
        /// The configuration key used to set <see cref="IHostEnvironment.ContentRootPath"/>.
        /// </summary>
        public static readonly string ContentRootKey = "AppSettings:ContentRoot";
    }
}
