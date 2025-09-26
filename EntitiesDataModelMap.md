using System.Text.Json;
using NanoidDotNet;
namespace Core.Models;
public class Campaign
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Website { get; set; }
    public string? CheckoutSite { get; set; }
    public string Status { get; set; } = "Rascunho";
    
    // auditoria
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    // relações
    public Guid OrganizationId { get; set; }
    public required Organization Organization { get; set; }
    public List<Sale> Sales { get; set; } = [];
    public List<VoucherTemplate> VoucherTemplates { get; set; } = [];
    public List<CampaignMember> CampaignMembers { get; set; } = [];
    public List<CampaignContributor> CampaignContributors { get; set; } = [];
}
public class CampaignContributor
{
    //chave composta
    public Guid CampaignId { get; set; }
    public required Campaign Campaign { get; set; }

    public Guid ContributorId { get; set; }
    public required Contributor Contributor { get; set; }
}
public class CampaignMember
{
    public Guid CampaignId { get; set; }
    public required Campaign Campaign { get; set; }

    public Guid MemberId { get; set; }
    public required Member Member { get; set; }
}
public class Contributor
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Email { get; set; }
    public required string WhatsAppNumber { get; set; }
    public bool IsActive { get; set; } = true;

    //auditoria
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    //relações
    public List<Sale> Sales { get; set; } = [];
    public List<OrganizationContributor> OrganizationContributors { get; set; } = [];
    public List<CampaignContributor> CampaignContributors { get; set; } = [];
}
public class Member
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string WhatsAppNumber { get; set; }
    public bool IsActive { get; set; } = true;

    //auditoria
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    //relações
    public List<Sale> Sales { get; set; } = [];
    public List<OrganizationMember> OrganizationMembers { get; set; } = [];
    public List<CampaignMember> CampaignMembers { get; set; } = [];
}
public record Document (string Number, string Type);
public class Organization
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Description { get; set; }
    public required string Country { get; set; }
    public required string State { get; set; }
    public required string City { get; set; }
    public string? Website { get; set; }
    public required string Email { get; set; }
    public required string WhatsAppNumber { get; set; }
    public required Document Document { get; set; }
    public required JsonDocument Settings { get; set; }

    //auditoria
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    //relações
    public List<Campaign> Campaigns { get; set; } = [];
    public List<Sale> Sales { get; set; } = [];
    public List<OrganizationMember> OrganizationMembers { get; set; } = [];
    public List<OrganizationContributor> organizationContributors { get; set; } = [];
}
public class OrganizationContributor
{
    public Guid OrganizationId { get; set; }
    public required Organization Organization { get; set; }
    public Guid ContributorId { get; set; }
    public required Contributor Contributor { get; set; }
    public DateTime? LastContributeAt { get; set; }
    public decimal? ContributeSum { get; set; }
}
public class OrganizationMember
{
    //chave composta única OrganizationId + MemberId
    public Guid OrganizationId { get; set; }
    public required Organization Organization { get; set; }
    public Guid MemberId { get; set; }
    public required Member Member { get; set; }

    //auditoria
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }

    //relções
    public Guid RoleId { get; set; }
    public required Role Role { get; set; }
}
public class Role
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Name { get; set; }
    public required string Description { get; set; }

    //relações
    public List<OrganizationMember> OrganizationMembers { get; set; } = [];
}
public class Sale
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string PaymentStatus { get; set; } = "Pendente";
    //status [ Pendente | Pagamento parcial | Pago | Emitido por Cortesia ]
    public required decimal TotalAmount { get; set; }
    public required decimal PaymentReceived { get; set; }
    public required string Currency { get; set; } = "BRL";
    public required string PaymentMethod { get; set; }
    public string? Notes { get; set; }

    //auditoria
    public DateTime? PaymentAt { get; set; } //preenche apenas quando estiver o valor quitado.
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    //relações
    public Guid OrganizationId { get; set; }
    public required Organization Organization { get; set; }
    public Guid MemberId { get; set; } // vendedor
    public required Member Member { get; set; }
    public Guid CampaignId { get; set; }
    public required Campaign Campaign { get; set; }
    public Guid? ContributorId { get; set; }
    public required Contributor Contributor { get; set; }
    public List<VoucherInstance> VoucherInstances { get; set; } = [];
}
public static class VoucherCode
{
    private const string Alphabet = "abcdefghijkmnpqrstuvwxyz@23456789";
    public static string Generate(int len = 8)
    {
        return Nanoid.Generate(Alphabet, len);
    }
}
public class VoucherInstance
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Code { get; set; } = VoucherCode.Generate();
    public string Status { get; set; } = "Emitido";
    //status: [ Emitido | Resgatado ]

    //auditoria
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? Canceled { get; set; }
    public DateTime? RedeemedAt { get; set; }

    //relações
    public Guid VoucherTemplateId { get; set; }
    public required VoucherTemplate VoucherTemplate { get; set; }
    public Guid SaleId { get; set; }
    public required Sale Sale { get; set; }
}
public class VoucherTemplate
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Category { set; get; }
    public string? Subtype { get; set; }
    public required JsonDocument Content { get; set; }
    public required int? SalesLimit { get; set; } // null = "sem Limite"
    public required int SalesCount { get; set; } = 0;
    public required decimal Price { get; set; } = 0;
    public required string Currency { get; set; } = "BRL";
    public string? CheckoutSite { get; set; }
    public bool IsActive { get; set; } = true;

    //auditoria
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    //relações
    public Guid OrganizationId { set; get; }
    public required Organization Organization { get; set; }
    public Guid CampaignId { set; get; }
    public required Campaign Campaign { get; set; }
    public List<VoucherInstance> VoucherInstances { get; set; } = [];
}
