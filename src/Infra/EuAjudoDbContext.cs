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

        modelBuilder.Entity<MemberRole>()
            .HasKey(mr => new { mr.MemberId, mr.OrganizationId });

        modelBuilder.Entity<MemberRole>()
            .HasOne(mr => mr.Member)
            .WithMany(m => m.MemberRoles)
            .HasForeignKey(mr => mr.MemberId);

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
