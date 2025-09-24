using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Core.Models;

public static class EntitiesMockFactory
{
    public static T Create<T>() where T : class
    {
        if (typeof(T) == typeof(Organization)) return (CreateOrganization() as T)!;
        if (typeof(T) == typeof(Member)) return (CreateMember() as T)!;
        if (typeof(T) == typeof(Role)) return (CreateRole() as T)!;
        if (typeof(T) == typeof(Campaign)) return (CreateCampaign() as T)!;
        if (typeof(T) == typeof(VoucherTemplate)) return (CreateVoucherTemplate() as T)!;
        if (typeof(T) == typeof(VoucherInstance)) return (CreateVoucherInstance() as T)!;
        if (typeof(T) == typeof(Contributor)) return (CreateContributor() as T)!;
        if (typeof(T) == typeof(OrganizationMember)) return (CreateOrganizationMember() as T)!;
        if (typeof(T) == typeof(OrganizationContributor)) return (CreateOrganizationContributor() as T)!;
        if (typeof(T) == typeof(CampaignContributor)) return (CreateCampaignContributor() as T)!;
        if (typeof(T) == typeof(CampaignMember)) return (CreateCampaignMember() as T)!;
        if (typeof(T) == typeof(Sale)) return (CreateSale() as T)!;

        throw new NotSupportedException($"Factory not implemented for {typeof(T).Name}");
    }

    private static Organization CreateOrganization()
    {
                return new Organization
                {
                    Description = "ONG Exemplo",
                    Country = "Brazil",
                    State = "SP",
                    City = "São Paulo",
                    Email = "org@example.com",
                    WhatsAppNumber = "+5511999999999",
                    Document = new Document("12345678901234", "CNPJ"),
                    Settings = JsonDocument.Parse("{\"theme\":\"default\"}"),
                    IsActive = true,
                };
    }

    private static Member CreateMember()
    {
        return new Member
        {
            FirstName = "Maria",
            LastName = "Silva",
            Email = "maria@example.com",
            WhatsAppNumber = "+5511988888888",
            IsActive = true
        };
    }

    private static Role CreateRole()
    {
        return new Role
        {
            Name = "Admin",
            Description = "Administrator role"
        };
    }

    private static Campaign CreateCampaign()
    {
        var org = CreateOrganization();
        return new Campaign
        {
            Name = "Campanha Solidária",
            Description = "Venda de pizzas para arrecadação",
            Status = "Rascunho",
            Organization = org,
            OrganizationId = org.Id
        };
    }

    private static VoucherTemplate CreateVoucherTemplate()
    {
        var org = CreateOrganization();
        var campaign = CreateCampaign();
        return new VoucherTemplate
        {
            Category = "Alimentação",
            Subtype = "Pizza Calabresa",
            Content = JsonDocument.Parse("{\"item\":\"Pizza Calabresa\"}"),
            SalesLimit = 100,
            SalesCount = 0,
            Price = 30,
            Currency = "BRL",
            IsActive = true,
            Organization = org,
            OrganizationId = org.Id,
            Campaign = campaign,
            CampaignId = campaign.Id
        };
    }

    private static VoucherInstance CreateVoucherInstance()
    {
        var sale = CreateSale();
        var template = CreateVoucherTemplate();
        return new VoucherInstance
        {
            VoucherTemplate = template,
            VoucherTemplateId = template.Id,
            Sale = sale,
            SaleId = sale.Id,
            Status = "Emitido"
        };
    }

    private static Contributor CreateContributor()
    {
        return new Contributor
        {
            FirstName = "João",
            Lastname = "Pereira",
            Email = "joao@example.com",
            WhatsAppNumber = "+5511977777777",
            IsActive = true
        };
    }

    private static OrganizationMember CreateOrganizationMember()
    {
        var org = CreateOrganization();
        var member = CreateMember();
        var role = CreateRole();

        return new OrganizationMember
        {
            Organization = org,
            OrganizationId = org.Id,
            Member = member,
            MemberId = member.Id,
            Role = role,
            RoleId = role.Id
        };
    }

    private static OrganizationContributor CreateOrganizationContributor()
    {
        var org = CreateOrganization();
        var contributor = CreateContributor();
        return new OrganizationContributor
        {
            Organization = org,
            OrganizationId = org.Id,
            Contributor = contributor,
            ContributorId = contributor.Id,
            ContributeSum = 200,
            LastContributeAt = DateTime.UtcNow
        };
    }

    private static CampaignContributor CreateCampaignContributor()
    {
        var campaign = CreateCampaign();
        var contributor = CreateContributor();
        return new CampaignContributor
        {
            Campaign = campaign,
            CampaignId = campaign.Id,
            Contributor = contributor,
            ContributorId = contributor.Id
        };
    }

    private static CampaignMember CreateCampaignMember()
    {
        var campaign = CreateCampaign();
        var member = CreateMember();
        return new CampaignMember
        {
            Campaign = campaign,
            CampaignId = campaign.Id,
            Member = member,
            MemberId = member.Id
        };
    }

    private static Sale CreateSale()
    {
        var org = CreateOrganization();
        var campaign = CreateCampaign();
        var member = CreateMember();
        var contributor = CreateContributor();

        return new Sale
        {
            Organization = org,
            OrganizationId = org.Id,
            Campaign = campaign,
            CampaignId = campaign.Id,
            Member = member,
            MemberId = member.Id,
            Contributor = contributor,
            ContributorId = contributor.Id,
            PaymentMethod = "Pix",
            TotalAmount = 50,
            PaymentReceived = 50,
            PaymentStatus = "Pago",
            Currency = "BRL"
        };
    }
}
