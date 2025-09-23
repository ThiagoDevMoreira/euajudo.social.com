
using Core.Models;
using Infra;
using InfraTests.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace InfraTests.Entities;

[Collection("InfraTests")]
public class MemberTests : IClassFixture<PostgresTestDatabaseFixture>
{
    private readonly EuAjudoDbContext _db;

    public MemberTests(PostgresTestDatabaseFixture fixture)
    {
        _db = fixture.CreateContext();
    }

    [Fact(DisplayName = "Deve persistir Member válido")]
    public async Task Should_Persist_Member_When_Valid()
    {
        var member = EntitiesMockFactory.Create<Member>();
        _db.Member.Add(member);
        await _db.SaveChangesAsync();

        var saved = await _db.Member.FindAsync(member.Id);
        Assert.NotNull(saved);
        Assert.Equal("Maria", saved!.FirstName);
    }

    [Theory(DisplayName = "Deve falhar ao persistir Member sem os campos obrigatórios")]
    [InlineData("FirstName")]
    [InlineData("LastName")]
    [InlineData("Email")]
    [InlineData("WhatsAppNumber")]
    public async Task Should_Fail_When_Required_Field_IsMissing(string propertyName)
    {
        var member = EntitiesMockFactory.Create<Member>();
        var prop = typeof(Member).GetProperty(propertyName);
        prop?.SetValue(member, null);

        _db.Member.Add(member);
        await Assert.ThrowsAsync<DbUpdateException>(() => _db.SaveChangesAsync());
    }

    [Fact(DisplayName = "Não deve falhar sem campos opcionais")]
    public async Task Should_NoFail_When_Optional_Fields_AreMissing()
    {
        var member = EntitiesMockFactory.Create<Member>();
        member.UpdatedAt = null;
        member.DeletedAt = null;

        _db.Member.Add(member);
        await _db.SaveChangesAsync();

        var saved = await _db.Member.FindAsync(member.Id);
        Assert.NotNull(saved);
    }

    [Fact(DisplayName = "Member deve aceitar Tabelas Relacionadas")]
    public async Task Should_Persist_With_Relation_Tables()
    {
        var member = EntitiesMockFactory.Create<Member>();

        var sale = EntitiesMockFactory.Create<Sale>();
        var organizationMember = EntitiesMockFactory.Create<OrganizationMember>();
        var campaignMember = EntitiesMockFactory.Create<CampaignMember>();

        sale.Member = member;
        sale.MemberId = member.Id;

        organizationMember.Member = member;
        organizationMember.MemberId = member.Id;

        campaignMember.Member = member;
        campaignMember.MemberId = member.Id;

        member.Sales.Add(sale);
        member.OrganizationMembers.Add(organizationMember);
        member.CampaignMembers.Add(campaignMember);

        _db.Member.Add(member);
        await _db.SaveChangesAsync();

        var saved = await _db.Member
            .Include(ms => ms.Sales)
            .Include(mo => mo.OrganizationMembers)
            .Include(mc => mc.CampaignMembers)
            .FirstOrDefaultAsync(ms => ms.Id == member.Id);

        Assert.NotNull(saved);
        Assert.Single(saved!.Sales);
        Assert.Single(saved!.OrganizationMembers);
        Assert.Single(saved!.CampaignMembers);
        Assert.Equal(member.Id, saved.Sales.First().MemberId);
        Assert.Equal(member.Id, saved.OrganizationMembers.First().MemberId);
        Assert.Equal(member.Id, saved.CampaignMembers.First().MemberId);
    }
}
