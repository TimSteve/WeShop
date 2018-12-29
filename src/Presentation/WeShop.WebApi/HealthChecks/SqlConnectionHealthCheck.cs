using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace UCenter.WebApi.HealthChecks
{
      public abstract class DbConnectionHealthCheck : IHealthCheck
    {
        protected DbConnectionHealthCheck(string name, string connectionString)
            : this(name, connectionString, testQuery: null)
        {
        }

        protected DbConnectionHealthCheck(string name, string connectionString, string testQuery)
        {
            Name = name ?? throw new System.ArgumentNullException(nameof(name));
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            TestQuery = testQuery;
        }

        public string Name { get; }
        protected string ConnectionString { get; }
        protected string TestQuery { get; }

        protected abstract NpgsqlConnection CreateConnection(string connectionString);

        public async Task<HealthCheckResult> CheckHealthAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var connection = CreateConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);
                    if (TestQuery != null)
                    {
                        var command = connection.CreateCommand();
                        command.CommandText = TestQuery;
                        await command.ExecuteNonQueryAsync(cancellationToken);
                    }
                }
                catch (DbException ex)
                {
                    return HealthCheckResult.Unhealthy(ex);
                }
            }

            return HealthCheckResult.Healthy();
        }
    }
}