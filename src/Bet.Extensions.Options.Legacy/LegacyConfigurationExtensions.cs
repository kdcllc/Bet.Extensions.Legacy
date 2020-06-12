using System;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// Adds support for Web.config or App.config legacy providers.
    /// </summary>
    public static class LegacyConfigurationExtensions
    {
        /// <summary>
        /// Add legacy support for App.config or Web.config configurations.
        /// </summary>
        /// <param name="builder">The configuration builder.</param>
        /// <param name="path">The actual path to the configuration file.</param>
        /// <returns></returns>
        public static IConfigurationBuilder AddLegacyConfig(
            this IConfigurationBuilder builder,
            string path)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return builder.Add(new LegacyConfigurationProvider(path));
        }
    }
}
