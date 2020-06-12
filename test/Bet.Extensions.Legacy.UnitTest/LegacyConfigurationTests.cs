using Microsoft.Extensions.Configuration;

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

            Assert.Equal("Custom Connection here", conifguration["ConnectionStrings:ConnectionString"]);
            Assert.Equal("Custom Connection2 here", conifguration["ConnectionStrings:ConnectionString2"]);
            Assert.Equal("One", conifguration["AppSettings:SomeKey1"]);
            Assert.Equal("Two", conifguration["AppSettings:SomeKey2"]);
        }
    }
}
