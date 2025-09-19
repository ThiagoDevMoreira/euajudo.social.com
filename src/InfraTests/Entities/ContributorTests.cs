using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Infra;
using Core.Models;
using InfraTests.DB;
using InfraTests.Collections;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace InfraTests.Entities

{
    [Collection("InfraTests")]
    public class ContributorTest
    {
        private readonly PostgresTestDatabaseFixture _fixture;

        public ContributorTest(PostgresTestDatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        private EuAjudoDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<EuAjudoDbContext>()
                .UseNpgsql(_fixture.ConnectionString)
                .Options;
            return new EuAjudoDbContext(options);
        }
        [Fact]
        public async Task Deve_Persistir_Contributor()
        {
            await using var ctx = CreateContext();
            var contributor = new Contributor
            {

            };
        }


    }
}
