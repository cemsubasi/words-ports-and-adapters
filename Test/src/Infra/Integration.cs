using Domain.Category.Port;
using Infra.Category.Adapter;
using Infra.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Test.Integrations;

public class Integration {
    private static readonly Lazy<ServiceProvider> _provider = new Lazy<ServiceProvider>(BuildServiceProvider);
    private static IConfigurationRoot _configuration;

    public static ServiceProvider Provider => _provider.Value;

    private static ServiceProvider BuildServiceProvider() {
        var services = new ServiceCollection();

        _configuration = new ConfigurationBuilder()
            .AddJsonFile("testconfig.json")
            .Build();

        services.Configure<DBOptions>(_configuration.GetSection("MainDb"));
        services.AddScoped<CategoryPort, CategoryAdapter>();

        return services.BuildServiceProvider();
    }

    public static IConfiguration Configuration => _configuration;
}