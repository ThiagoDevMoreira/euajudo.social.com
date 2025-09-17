# Mapa de entidades do projeto:
> Arquivo de referencia para consulta rápida

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
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
public class Contributor
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string Lastname { get; set; }
    public required string Email { get; set; }
    public required string WhatsAppNumber { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
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
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
public class MemberRole
{
    public Guid MemberId { get; set; }
    public Guid RoleId { get; set; }
    public Guid OrganizationId { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public string? LastUpdate { get; set; } // formato `<roleAnterior> -> <roleAtual>`
    public DateTimeOffset? UpdatedAt { get; set; }
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
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
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
    public DateTimeOffset? LastContributeAt { get; set; }
    public decimal? ContributeSum { get; set; }
}
public class OrganizationMember
{
    public Guid OrganizationId { get; set; }
    public Guid MemberId { get; set; }
    public required Organization Organization { get; set; }
    public required Member Member { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? DeletedAt { get; set; }
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
    public DateTimeOffset? PaymentAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; }
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
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.Now;
    public DateTimeOffset? RedeemedAt { get; set; }
    public Guid? SaleId { get; set; }
    public Sale? Sale { get; set; }
    public DateTimeOffset? CanceledAt { get; set; }
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
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public List<VoucherInstance> VoucherInstances { get; set; } = [];
}
```

# Referencia do DbContext:
> Arquivo: euajudo\src\Infra\EuAjudoDbContext.cs
```
using Microsoft.EntityFrameworkCore;
using Core.Models;

namespace Infra;

public class EuAjudoDbContext : DbContext
{
    public EuAjudoDbContext(DbContextOptions<EuAjudoDbContext> options) : base(options) { }

    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Contributor> Contributors { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<MemberRole> MemberRoles { get; set; }
    public DbSet<Organization> Orgs { get; set; }
    public DbSet<OrganizationContributor> OrgContributors { get; set; }
    public DbSet<OrganizationMember> OrgMembers { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<VoucherInstance> VoucherInstances { get; set; }
    public DbSet<VoucherTemplate> VoucherTemplates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OrganizationContributor>()
            .HasKey(oc => new { oc.OrganizationId, oc.ContributorId });

        modelBuilder.Entity<OrganizationContributor>()
            .HasOne(oc => oc.Organization)
            .WithMany(o => o.OrganizationContributors)
            .HasForeignKey(oc => oc.OrganizationId);

        modelBuilder.Entity<OrganizationContributor>()
            .HasOne(oc => oc.Contributor)
            .WithMany(c => c.OrganizationContributors)
            .HasForeignKey(oc => oc.ContributorId);

        // OrganizationMember (N:N com payload extra)
        modelBuilder.Entity<OrganizationMember>()
            .HasKey(om => new { om.OrganizationId, om.MemberId });

        modelBuilder.Entity<OrganizationMember>()
            .HasOne(om => om.Organization)
            .WithMany(o => o.OrganizationMembers)
            .HasForeignKey(om => om.OrganizationId);

        modelBuilder.Entity<OrganizationMember>()
            .HasOne(om => om.Member)
            .WithMany(m => m.OrganizationMembers)
            .HasForeignKey(om => om.MemberId);

        // MemberRole (N:N entre Member, Role e Organization)
        modelBuilder.Entity<MemberRole>()
            .HasKey(mr => new { mr.MemberId, mr.RoleId, mr.OrganizationId });

        modelBuilder.Entity<MemberRole>()
            .HasOne(mr => mr.Member)
            .WithMany(m => m.MemberRoles)
            .HasForeignKey(mr => mr.MemberId);

        modelBuilder.Entity<MemberRole>()
            .HasOne(mr => mr.Role)
            .WithMany() 
            .HasForeignKey(mr => mr.RoleId);

        modelBuilder.Entity<MemberRole>()
            .HasOne(mr => mr.Organization)
            .WithMany()
            .HasForeignKey(mr => mr.OrganizationId);
        modelBuilder.Entity<Organization>()
            .HasIndex(o => o.Email)
            .IsUnique();

        modelBuilder.Entity<Contributor>()
            .HasIndex(c => c.Email)
            .IsUnique();

        modelBuilder.Entity<Member>()
            .HasIndex(m => m.Email)
            .IsUnique();

        modelBuilder.Entity<Sale>()
            .HasIndex(s => s.ContributorEmail);
        modelBuilder.Entity<Organization>()
            .OwnsOne(o => o.Document, d =>
            {
                d.Property(p => p.Number).HasColumnName("DocumentNumber");
                d.Property(p => p.Type).HasColumnName("DocumentType");
            });
    }
}
```
