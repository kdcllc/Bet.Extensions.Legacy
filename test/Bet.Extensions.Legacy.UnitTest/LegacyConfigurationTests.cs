using Bet.Extensions.Legacy.UnitTest;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Xunit;

namespace Bet.Extensions.DependencyInjection.WebForms.UnitTest
{
    public class LegacyConfigurationTests
    {
        [Fact]
        public void Configure_AppConfig_Successfully()
        {
            // Arrange
            var builder = new ConfigurationBuilder();
            builder.AddLegacyConfig("App.config");

            // Act
            var conifguration = builder.Build();

            // should reload value
            builder.Build();

            Assert.Equal("Custom Connection here", conifguration["ConnectionStrings:DbConnection1"]);
            Assert.Equal("Custom Connection2 here", conifguration["ConnectionStrings:DbConnection2"]);
            Assert.Equal("One", conifguration["SomeKey1"]);
            Assert.Equal("Two", conifguration["SomeKey2"]);
        }

        [Fact]
        public void Configure_AppConfig__2_Successfully()
        {
            // Arrange
            var builder = new ConfigurationBuilder();
            builder.AddLegacyConfig("App.config");

            // Act
            var conifguration = builder.Build();

            // should reload value
            builder.Build();

            Assert.Equal("Custom Connection here", conifguration.GetConnectionString("DbConnection1"));
            Assert.Equal("Custom Connection2 here", conifguration.GetConnectionString("DbConnection2"));
            Assert.Equal("One", conifguration["SomeKey1"]);
            Assert.Equal("Two", conifguration["SomeKey2"]);
        }

        [Fact]
        public void Configure_AppConfig__With_TestOptions_Successfully()
        {
            var services = new ServiceCollection();

            // Arrange
            var builder = new ConfigurationBuilder();
            builder.AddLegacyConfig("App.config");

            services.AddOptions<TestOptions>()
                .Configure<IConfiguration>((options, config) =>
                {
                    config.Bind(nameof(TestOptions), options);
                });

            services.AddSingleton<IConfiguration>(builder.Build());

            var sp = services.BuildServiceProvider();

            var options = sp.GetRequiredService<IOptions<TestOptions>>().Value;

            Assert.Equal(45678, options.Id);
            Assert.Equal(3, options.ArrayValue.Length);
        }

        [Fact]
        public void Configure_appsettings__With_TestOptions_Successfully()
        {
            var services = new ServiceCollection();

            // Arrange
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");

            services.AddOptions<TestOptions>()
                .Configure<IConfiguration>((options, config) =>
                {
                    config.Bind(nameof(TestOptions), options);
                });

            services.AddSingleton<IConfiguration>(builder.Build());

            var sp = services.BuildServiceProvider();

            var options = sp.GetRequiredService<IOptions<TestOptions>>().Value;

            Assert.Equal(45678, options.Id);
            Assert.Equal(3, options.ArrayValue.Length);
        }
    }
}
