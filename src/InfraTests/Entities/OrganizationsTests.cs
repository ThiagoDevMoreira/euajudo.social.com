using Microsoft.EntityFrameworkCore;
using Core.Models;
using Infra;
using InfraTests.DB;

namespace InfraTests.Entities;

[Collection("InfraTests")]
public class OrganizationTests : IClassFixture<PostgresTestDatabaseFixture>
{
    private readonly EuAjudoDbContext _db;
    public OrganizationTests(PostgresTestDatabaseFixture fixture)
    {
        _db = fixture.CreateContext();
    }

    [Fact(DisplayName = "Deve persistir Organization válido")]
    public async Task Should_Persist_Organization_When_Valid()
    {
        var org = EntitiesMockFactory.Create<Organization>();
        _db.Organization.Add(org);
        await _db.SaveChangesAsync();

        var saved = await _db.Organization.FindAsync(org.Id);
        Assert.NotNull(saved);
        Assert.Equal("ONG Exemplo", saved!.Description);
    }

    [Theory(DisplayName = "Deve falhar ao persistir Organization sem campos obrigatórios")]
    [InlineData("Description")]
    [InlineData("State")]
    [InlineData("City")]
    [InlineData("Email")]
    [InlineData("WhatsAppNumber")]
    [InlineData("Document")]
    [InlineData("Settings")]
    public async Task Should_Fail_When_Required_Field_IsMissing(string propertyName)
    {
        var org = EntitiesMockFactory.Create<Organization>();
        var prop = typeof(Organization).GetProperty(propertyName);
        prop?.SetValue(org, null);

        _db.Organization.Add(org);
        await Assert.ThrowsAsync<DbUpdateException>(() => _db.SaveChangesAsync());
    }

    [Fact(DisplayName = "Não deve falhar sem campos opcionais")]
    public async Task Should_NotFail_When_Optional_Fields_AreMissing()
    {
        var org = EntitiesMockFactory.Create<Organization>();
        org.Website = null;
        org.UpdatedAt = null;
        org.DeletedAt = null;

        _db.Organization.Add(org);
        await _db.SaveChangesAsync();

        var saved = await _db.Organization.FindAsync(org.Id);
        Assert.NotNull(saved);
    }

    [Fact(DisplayName = "Organization deve aceitar Campaigns relacionados")]
    public async Task Should_Persist_With_Campaigns()
    {
        var org = EntitiesMockFactory.Create<Organization>();
        var campaign = EntitiesMockFactory.Create<Campaign>();
        campaign.Organization = org;
        campaign.OrganizationId = org.Id;

        org.Campaigns.Add(campaign);

        _db.Organization.Add(org);
        await _db.SaveChangesAsync();

        var saved = await _db.Organization
            .Include(o => o.Campaigns)
            .FirstOrDefaultAsync(o => o.Id == org.Id);

        Assert.NotNull(saved);
        Assert.Single(saved!.Campaigns);
        Assert.Equal("Campanha Solidária", saved.Campaigns.First().Name);
    }
}
