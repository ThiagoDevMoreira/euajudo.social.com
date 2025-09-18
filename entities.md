# Mapa de entidades do projeto:
> Arquivo de referencia para entidades do euajudo.social.br

```c#.net v9.*

using System.Text.Json;
using NanoidDotNet;
namespace Core.Models;

// utilidades:
public record Document (string Number, string Type);

// fim - utilidades

public class Campaign
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public required Organization Organization { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Website { get; set; }
    public string? CheckoutSite { get; set; }
    public string? Status{ get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
public class Contributor
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string Lastname { get; set; }
    public required string Email { get; set; }
    public required string WhatsAppNumber { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public List<OrganizationContributor> OrganizationContributors { get; set; } = [];
    public required List<Campaign> Campaigns { get; set; } = [];
}
public class Member
{
    public Guid Id { get; set; }
    public List<OrganizationMember> OrganizationMembers {get; set;} = [];
    public List<MemberRole> MemberRoles {get; set;} = [];
    public required string FirstName { get; set; }
    public required string Lastname { get; set; }
    public required string Email { get; set; }
    public required string WhatsAppNumber { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
public class MemberRole
{
    public Guid MemberId { get; set; }
    public Guid RoleId { get; set; }
    public Guid OrganizationId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? LastUpdate { get; set; } // formato `<roleAnterior> -> <roleAtual>`
    public DateTime? UpdatedAt { get; set; }
    public required Member Member {get;set;}
    public required Role Role {get;set;}
    public required Organization Organization {get;set;}
}
public class Organization
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public string? Website { get; set; }
    public required string Email { get; set; }
    public required string WhatsAppNumber { get; set; }
    public required Document Document { get; set; }
    public required JsonDocument Settings { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public List<OrganizationMember> OrganizationMembers { get; set; } = [];
    public List<OrganizationContributor> OrganizationContributors { get; set; } = [];
    public List<Campaign> Campaigns { get; set; } = [];
    public List<Sale> Sales { get; set; } = [];
}
public class OrganizationContributor
{
    public Guid OrganizationId { get; set; }
    public Guid ContributorId { get; set; }
    public required Organization Organization { get; set; } 
    public required Contributor Contributor { get; set; }
    public DateTime? LastContributeAt { get; set; }
    public decimal? ContributeSum { get; set; }
}
public class OrganizationMember
{
    public Guid OrganizationId { get; set; }
    public Guid MemberId { get; set; }
    public required Organization Organization { get; set; }
    public required Member Member { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
}
public class Role
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}
public class Sale
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public required Organization Organization { get; set; }
    public Guid CampaignId { get; set; }
    public required Campaign Campaign { get; set; }
    public Guid MemberId { get; set; } // vendedor
    public required Member Member { get; set; }
    public Guid? ContributorId { get; set; }
    public Contributor? Contributor { get; set; }
    public required string ContributorFirstName { get; set; }
    public required string ContributorLastName { get; set; }
    public required string ContributorEmail { get; set; }
    public required string ContributorWhatsAppNumber { get; set; }
    public required decimal TotalAmount { get; set; } = 0;
    public required string Currency { get; set; } = "BRL";
    public required string PaymentStatus { get; set; }
    public DateTime? PaymentAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public List<VoucherInstance> VoucherInstances { get; set; } = [];
}
public static class VoucherCodeGenerator
{
    private const string Alphabet = "abcdefghijkmpqrstuvwxz23456789";
    public static string Generate(int len = 8)
    {
        return Nanoid.Generate(Alphabet, len);
    }
}
public class VoucherInstance
{
    public Guid Id { get; set; }
    public Guid VoucherTemplateId { get; set; }
    public required VoucherTemplate VoucherTemplate { get; set; }
    public required string Code { get; set; } = VoucherCodeGenerator.Generate();
    public string? Status { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? RedeemedAt { get; set; }
    public Guid? SaleId { get; set; }
    public Sale? Sale { get; set; }
    public DateTime? CanceledAt { get; set; }
}
public class VoucherTemplate
{
    public Guid Id { get; set; }
    public Guid OrganizationId { set; get; }
    public required Organization Organization { get; set; }
    public Guid CampaignId { set; get; }
    public required Campaign Campaign { get; set; }
    public required string Category { set; get; } = "unique";
    public string? Subtype { get; set; }
    public required JsonDocument Content { get; set; }
    public int? SalesLimit { get; set; } // null = "sem Limite"
    public required int SalesCount { get; set; } = 0;
    public required decimal Price { get; set; } = 0;
    public required string Currency { get; set; } = "BRL";
    public string? CheckoutSite { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public List<VoucherInstance> VoucherInstances { get; set; } = [];
}
```
