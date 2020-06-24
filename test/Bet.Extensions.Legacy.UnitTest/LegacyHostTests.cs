
using Bet.Extensions.LegacyHosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Xunit;
using Xunit.Abstractions;

namespace Bet.Extensions.Legacy.UnitTest
{
    public class LegacyHostTests
    {
        private readonly ITestOutputHelper _output;

        public LegacyHostTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void LegacyHost_Loaded_Successfully()
        {
            var appName = "TestApp";
            var env = "Development";

            var host = Host.CreateDefaultBuilder()
                            .ConfigureServices((context, services) =>
                            {
                                services.AddLogging(logBuilder =>
                                {
                                    logBuilder.AddXUnit(_output);
                                });

                                services.AddOptions<TestOptions>()
                                         .Configure<IConfiguration>((options, config) =>
                                         {
                                             config.Bind(nameof(TestOptions), options);
                                         });
                            })
                           .UseApplicationName(appName)
                           .Build();

            var sp = host.Services;

            var hostEnv = sp.GetRequiredService<IHostEnvironment>();

            Assert.Equal(env, hostEnv.EnvironmentName);
            Assert.Equal(appName, hostEnv.ApplicationName);

            var options = sp.GetRequiredService<IOptions<TestOptions>>().Value;

            Assert.Equal(45678, options.Id);
            Assert.Equal(3, options.ArrayValue.Length);
        }
    }
}
