using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Infra;
using InfraTests.DB;

namespace InfraTests.DatabaseStructure
{
    [Collection("InfraTests")]
    public class BuildDatabase
    {
        private readonly PostgresTestDatabaseFixture _fixture;

        public BuildDatabase(PostgresTestDatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Deve_ConectarAoBanco_ComSucesso()
        {
            var options = new DbContextOptionsBuilder<EuAjudoDbContext>()
                          .UseNpgsql(_fixture.ConnectionString)
                          .Options;

            await using var ctx = new EuAjudoDbContext(options);

            var podeConectar = await ctx.Database.CanConnectAsync();

            podeConectar.Should().BeTrue();
        }
    }
}
