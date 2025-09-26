using Xunit;
using Core.Models;
using Core.Models.MockFactory;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace InfraTests.Entities;

public class OrganizationTests
{
    [Fact(DisplayName = "Deve validar Organization válido")]
    public void Should_Validate_When_Valid()
    {
        var org = EntitiesMockFactory.Create<Organization>();

        // Act + Assert: não deve lançar exceção
        org.Validate();
    }

    [Fact(DisplayName = "Deve validar Organization com campos opcionais nulos/vazios")]
    public void Should_Validate_When_Optional_Fields_AreNullOrEmpty()
    {
        var org = EntitiesMockFactory.Create<Organization>();
        org.Website = null;
        org.UpdatedAt = null;
        org.DeletedAt = null;

        // Act + Assert: não deve lançar exceção
        org.Validate();
    }

    [Fact(DisplayName = "Deve validar Organization com relacionamentos opcionais vazios")]
    public void Should_Validate_When_Optional_Relationships_AreEmpty()
    {
        var org = EntitiesMockFactory.Create<Organization>();
        org.Campaigns.Clear();
        org.Sales.Clear();
        org.OrganizationMembers.Clear();
        org.OrganizationContributors.Clear();

        // Act + Assert: não deve lançar exceção
        org.Validate();
    }

    [Theory(DisplayName = "Deve falhar ao validar Organization sem campos obrigatórios")]
    [InlineData("Description")]
    [InlineData("Country")]
    [InlineData("State")]
    [InlineData("City")]
    [InlineData("Email")]
    [InlineData("WhatsAppNumber")]
    public void Should_Fail_When_Required_Field_IsMissing(string propertyName)
    {
        var org = EntitiesMockFactory.Create<Organization>();

        // força null no campo alvo
        typeof(Organization).GetProperty(propertyName)?.SetValue(org, null);

        Assert.Throws<ValidationException>(() => org.Validate());
    }

    [Fact(DisplayName = "Deve falhar ao validar Organization com Id vazio")]
    public void Should_Fail_When_Id_IsEmpty()
    {
        var org = EntitiesMockFactory.Create<Organization>();
        org.Id = Guid.Empty;

        Assert.Throws<ValidationException>(() => org.Validate());
    }

    [Fact(DisplayName = "Deve falhar ao validar Organization com Document inválido")]
    public void Should_Fail_When_Document_IsInvalid()
    {
        var org = EntitiesMockFactory.Create<Organization>();
        org.Document = new Document("", ""); // inválido

        Assert.Throws<ValidationException>(() => org.Validate());
    }

    [Fact(DisplayName = "Deve falhar ao validar Organization com Settings nulo")]
    public void Should_Fail_When_Settings_IsNull()
    {
        var org = EntitiesMockFactory.Create<Organization>();
        org.Settings = null!;

        Assert.Throws<ValidationException>(() => org.Validate());
    }

    [Fact(DisplayName = "Deve falhar ao validar Organization com Settings indefinido")]
    public void Should_Fail_When_Settings_IsUndefined()
    {
        var org = EntitiesMockFactory.Create<Organization>();
        org.Settings = JsonDocument.Parse("null"); // inválido para Settings

        Assert.Throws<ValidationException>(() => org.Validate());
    }

    [Fact(DisplayName = "Deve falhar ao validar Organization com CreatedAt = DateTime.MinValue")]
    public void Should_Fail_When_CreatedAt_IsMinValue()
    {
        var org = EntitiesMockFactory.Create<Organization>();
        typeof(Organization).GetProperty(nameof(Organization.CreatedAt))!
            .SetValue(org, DateTime.MinValue);

        Assert.Throws<ValidationException>(() => org.Validate());
    }
}
