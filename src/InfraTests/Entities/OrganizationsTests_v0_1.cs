// using Microsoft.EntityFrameworkCore;
// using Core.Models;
// using Infra;
// using InfraTests.DB;

// namespace InfraTests.Entities;

// [Collection("InfraTests")]
// public class OrganizationTests : IClassFixture<PostgresTestDatabaseFixture>
// {
//     private readonly EuAjudoDbContext _db;

//     public OrganizationTests(PostgresTestDatabaseFixture fixture)
//     {
//         _db = fixture.CreateContext();
//     }

//     // Deve persistir quando todos os campos (inclusive opcionais) estiverem preenchidos
//     [Fact(DisplayName = "Deve persistir Organization válido")]
//     public async Task Should_Persist_When_Valid()
//     {
//         var org = EntitiesMockFactory.Create<Organization>();

//         //popula campos e relacionamentos opcionais

//         org.Website = "https//enderecodosite.com.br";
//         org.UpdatedAt = DateTime.MinValue;
//         org.DeletedAt = DateTime.MinValue;

//         var campaign = EntitiesMockFactory.Create<Campaign>();
//         campaign.Organization = org;
//         campaign.OrganizationId = org.Id;

//         var sale = EntitiesMockFactory.Create<Sale>();
//         sale.Organization = org;
//         sale.OrganizationId = org.Id;

//         var orgMember = EntitiesMockFactory.Create<OrganizationMember>();
//         orgMember.Organization = org;
//         orgMember.OrganizationId = org.Id;

//         var orgContributor = EntitiesMockFactory.Create<OrganizationContributor>();
//         orgContributor.Organization = org;
//         orgContributor.OrganizationId = org.Id;

//         org.Campaigns.Add(campaign);
//         org.Sales.Add(sale);
//         org.OrganizationMembers.Add(orgMember);
//         org.OrganizationContributors.Add(orgContributor);

//         _db.Organization.Add(org);
//         await _db.SaveChangesAsync();

//         var saved = await _db.Organization.FindAsync(org.Id);
//         Assert.NotNull(saved);
//         Assert.Equal("ONG Exemplo", saved!.Description);
//     }

//     // ✅ Deve persistir quando campos opcionais estiverem vazios/nulos
//     [Fact(DisplayName = "Deve persistir Organization com campos opcionais nulos/vazios")]
//     public async Task Should_Persist_When_Optional_Fields_AreNullOrEmpty()
//     {
//         var org = EntitiesMockFactory.Create<Organization>();
//         _db.Organization.Add(org);

//         await _db.SaveChangesAsync();
//         var saved = await _db.Organization.FindAsync(org.Id);
//         Assert.NotNull(saved);
//     }

//     // ✅ Deve persistir quando relacionamentos opcionais estiverem vazios
//     [Fact(DisplayName = "Deve persistir Organization com relacionamentos opcionais vazios")]
//     public async Task Should_Persist_When_Optional_Relationships_AreEmpty()
//     {
//         var org = EntitiesMockFactory.Create<Organization>();
//         org.Campaigns.Clear();
//         org.Sales.Clear();
//         org.OrganizationMembers.Clear();
//         org.OrganizationContributors.Clear();

//         _db.Organization.Add(org);
//         await _db.SaveChangesAsync();

//         var saved = await _db.Organization
//             .Include(o => o.Campaigns)
//             .Include(o => o.Sales)
//             .Include(o => o.OrganizationMembers)
//             .Include(o => o.OrganizationContributors)
//             .FirstOrDefaultAsync(o => o.Id == org.Id);

//         Assert.NotNull(saved);
//         Assert.Empty(saved!.Campaigns);
//         Assert.Empty(saved.Sales);
//         Assert.Empty(saved.OrganizationMembers);
//         Assert.Empty(saved.OrganizationContributors);
//     }

//     // ❌ Deve falhar quando campos obrigatórios forem nulos/vazios
//     [Theory(DisplayName = "Deve falhar ao persistir Organization sem campos obrigatórios")]
//     [InlineData("Id")]
//     [InlineData("Description")]
//     [InlineData("Country")]
//     [InlineData("State")]
//     [InlineData("City")]
//     [InlineData("Email")]
//     [InlineData("WhatsAppNumber")]
//     [InlineData("Settings")]
//     [InlineData("CreatedAt")]
    
//     public async Task Should_Fail_When_Required_Field_IsMissing(string propertyName)
//     {
//         var org = EntitiesMockFactory.Create<Organization>();
//         if (propertyName == "CreatedAt")
//         {
//             typeof(Organization).GetProperty(propertyName)?.SetValue(org, DateTime.MinValue);
//         }

//         typeof(Organization).GetProperty(propertyName)?.SetValue(org, null);

//         _db.Organization.Add(org);

//         await Assert.ThrowsAsync<DbUpdateException>(() => _db.SaveChangesAsync());
//     }

//     // ❌ Deve falhar quando Document for inválido
//     [Fact(DisplayName = "Deve falhar ao persistir Organization sem Document")]
//     public async Task Should_Fail_When_Document_IsNull()
//     {
//         var org = EntitiesMockFactory.Create<Organization>();
//         org.Document = null!;

//         _db.Organization.Add(org);

//         await Assert.ThrowsAsync<DbUpdateException>(() => _db.SaveChangesAsync());
//     }

//     // ❌ Deve falhar quando Settings for inválido
//     [Fact(DisplayName = "Deve falhar ao persistir Organization sem Settings")]
//     public async Task Should_Fail_When_Settings_IsNull()
//     {
//         var org = EntitiesMockFactory.Create<Organization>();
//         org.Settings = null!;
//         _db.Organization.Add(org);

//         await Assert.ThrowsAsync<DbUpdateException>(() => _db.SaveChangesAsync());
//     }
// }
