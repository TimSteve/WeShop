using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WeShop.WebApi.HealthChecks
{
    public class SqliteHealthCheck : IHealthCheck
    {
        private readonly SqliteConnection CreateConnection;
        protected string _connectionString { get; }

        private static readonly string DefaultTestQuery = "Select 1";

        public SqliteHealthCheck(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);
                    if (DefaultTestQuery != null)
                    {
                        var command = connection.CreateCommand();
                        command.CommandText = DefaultTestQuery;
                        await command.ExecuteNonQueryAsync(cancellationToken);
                    }
                }
                catch (DbException ex)
                {
                    return HealthCheckResult.Unhealthy("Database connection exception.", ex);
                }
            }

            return HealthCheckResult.Healthy();
        }
    }
}