using System.Configuration;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// Legacy support for App.config and Web.config.
    /// </summary>
    public class LegacyConfigurationProvider : ConfigurationProvider, IConfigurationSource
    {
        private readonly System.Configuration.Configuration _legacyConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="LegacyConfigurationProvider"/> class.
        /// </summary>
        /// <param name="path"></param>
        public LegacyConfigurationProvider(string path)
        {
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = path };
            _legacyConfiguration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }

        public override void Load()
        {
            foreach (ConnectionStringSettings connectionString in _legacyConfiguration.ConnectionStrings.ConnectionStrings)
            {
                var key = $"ConnectionStrings:{connectionString.Name}";

                if (Data.ContainsKey(key))
                {
                    Data.Remove(key);
                }

                Data.Add(key, connectionString.ConnectionString);
            }

            foreach (var settingKey in _legacyConfiguration.AppSettings.Settings.AllKeys)
            {
                var key = $"AppSettings:{settingKey}";

                if (Data.ContainsKey(settingKey))
                {
                    Data.Remove(settingKey);
                }

                Data.Add(settingKey, _legacyConfiguration.AppSettings.Settings[settingKey].Value);
            }
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return this;
        }
    }
}
