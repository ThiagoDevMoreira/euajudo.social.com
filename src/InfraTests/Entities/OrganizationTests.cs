using System.Text.Json;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Infra;
using Core.Models;
using InfraTests.DB;

namespace InfraTests.Entities
{
    [Collection("InfraTests")]
    public class OrganizationTests
    {
        private readonly PostgresTestDatabaseFixture _fixture;

        public OrganizationTests(PostgresTestDatabaseFixture fixture)
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
        public async Task Deve_Persistir_Organization_Com_Documento()
        {
            await using var ctx = CreateContext();

            var org = new Organization
            {
                Id = Guid.NewGuid(),
                Name = "ONG Teste",
                Description = "ONG Teste, criada pela estrutura de testes de infra estrutura automatizados.",
                Email = "ong1@test.local",
                WhatsAppNumber = "5511999999999",
                Document = new Document("12345678901", "CNPJ"),
                Settings = JsonDocument.Parse("{\"foo\":\"bar\"}")
            };

            ctx.Orgs.Add(org);
            await ctx.SaveChangesAsync();

            var fetched = await ctx.Orgs.FirstOrDefaultAsync(o => o.Email == "ong1@test.local");

            fetched.Should().NotBeNull();
            fetched!.Document.Number.Should().Be("12345678901");
            fetched.Document.Type.Should().Be("CNPJ");
        }

        [Fact]
        public async Task Nao_Deve_Permitir_Email_Duplicado()
        {
            await using var ctx = CreateContext();

            var org1 = new Organization
            {
                Id = Guid.NewGuid(),
                Name = "ONG A",
                Description = "ONG A (teste de duplicação de email), criada pela estrutura de testes de infra estrutura automatizados.",
                Email = "duplicado@test.local",
                WhatsAppNumber = "5511000000000",
                Document = new Document("111", "CNPJ"),
                Settings = JsonDocument.Parse("{}")
            };

            var org2 = new Organization
            {
                Id = Guid.NewGuid(),
                Name = "ONG B",
                Description = "ONG B (teste de duplicação de email), criada pela estrutura de testes de infra estrutura automatizados.",
                Email = "duplicado@test.local", // mesmo email
                WhatsAppNumber = "5522000000000",
                Document = new Document("222", "CNPJ"),
                Settings = JsonDocument.Parse("{}")
            };

            ctx.Orgs.Add(org1);
            await ctx.SaveChangesAsync();

            ctx.Orgs.Add(org2);
            var act = async () => await ctx.SaveChangesAsync();

            await act.Should().ThrowAsync<DbUpdateException>();
        }
    }
}
