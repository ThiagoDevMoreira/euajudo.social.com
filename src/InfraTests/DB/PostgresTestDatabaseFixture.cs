// fixture para teste de integração postgre-dbcontect-models
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Infra; // indica onde está o dbcontext em que se baseiam os testes

namespace InfraTests.DB
{
    public class PostgresTestDatabaseFixture : IAsyncLifetime
    {
        public string ConnectionString { get; }
        public string DatabaseName { get; }

        private readonly string _masterConnection;

        public PostgresTestDatabaseFixture()
        {

            _masterConnection = "Host=localhost;Username=postgres;Password=123;Database=postgres";

            DatabaseName = "euajudo_test_" + Guid.NewGuid().ToString("N");

            var builder = new NpgsqlConnectionStringBuilder(_masterConnection)
            {
                Database = DatabaseName
            };

            ConnectionString = builder.ToString();
        }

        public async Task InitializeAsync()
        {
            await using (var conn = new NpgsqlConnection(_masterConnection))
            {
                await conn.OpenAsync();
                await using var cmd = conn.CreateCommand();
                cmd.CommandText = $"CREATE DATABASE \"{DatabaseName}\"";
                await cmd.ExecuteNonQueryAsync();
            }

            var options = new DbContextOptionsBuilder<EuAjudoDbContext>()
                          .UseNpgsql(ConnectionString)
                          .Options;

            await using var context = new EuAjudoDbContext(options);
            await context.Database.MigrateAsync();
        }

        public async Task DisposeAsync()
        {
            await using var conn = new NpgsqlConnection(_masterConnection);
            await conn.OpenAsync();

            await using (var cmd1 = conn.CreateCommand())
            {
                cmd1.CommandText =
                    $"SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE datname = '{DatabaseName}' AND pid <> pg_backend_pid();";
                await cmd1.ExecuteNonQueryAsync();
            }

            await using (var cmd2 = conn.CreateCommand())
            {
                cmd2.CommandText = $"DROP DATABASE IF EXISTS \"{DatabaseName}\";";
                await cmd2.ExecuteNonQueryAsync();
            }
        }
    }
}
