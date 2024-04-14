using Microsoft.Extensions.Configuration;

namespace Infra.Test.Integrations.Configuration;
public class ConfigurationProvider {
    private static readonly Lazy<IConfiguration> _configuration = new(BuildConfiguration);

    private static IConfiguration BuildConfiguration() {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("config.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        return configuration;
    }

    public static IConfiguration Instance => _configuration.Value;
}