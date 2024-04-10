using Infra.Configurations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace Infra.Health;

public class MySqlHealthCheck(IOptions<DBOptions> options) : IHealthCheck {
    private readonly IOptions<DBOptions> options = options;

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default) {
        try {
            await using var connection = new MySqlConnection(this.options.Value.MainDb);
            await connection.OpenAsync(cancellationToken);
            return HealthCheckResult.Healthy();
        } catch (Exception e) {
            return HealthCheckResult.Unhealthy(e.Message);
        }
    }
}