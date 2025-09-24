using Microsoft.EntityFrameworkCore;
using Core.Models;

namespace Infra;

public class EuAjudoDbContext : DbContext
{
    public EuAjudoDbContext(DbContextOptions<EuAjudoDbContext> options) : base(options) { }
    
    public DbSet<Campaign> Campaign { get; set; }
    public DbSet<CampaignContributor> CampaignContributor { get; set; }
    public DbSet<CampaignMember> CampaignMember { get; set; }
    public DbSet<Contributor> Contributor { get; set; }
    public DbSet<Member> Member { get; set; }
    public DbSet<Organization> Organization { get; set; }
    public DbSet<OrganizationContributor> OrganizationContributor { get; set; }
    public DbSet<OrganizationMember> OrganizationMember { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Sale> Sale { get; set; }
    public DbSet<VoucherInstance> VoucherInstance { get; set; }
    public DbSet<VoucherTemplate> VoucherTemplate { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // consistência de armazenameto de dadas sempre em utc
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(
                        new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<DateTime, DateTime>(
                            v => v, // já esperamos DateTime em UTC
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                    );
                }
            }
        }

        // chaves compostas únicas
        modelBuilder.Entity<OrganizationMember>()
            .HasKey(om => new { om.OrganizationId, om.MemberId });

        modelBuilder.Entity<OrganizationContributor>()
            .HasKey(oc => new { oc.OrganizationId, oc.ContributorId });

        modelBuilder.Entity<CampaignMember>()
            .HasKey(cm => new { cm.CampaignId, cm.MemberId });

        modelBuilder.Entity<CampaignContributor>()
            .HasKey(cc => new { cc.CampaignId, cc.ContributorId });

        //relações A <1:1> B <1:1> A
        //relações A <1:1> B <1:N> A
        modelBuilder.Entity<OrganizationMember>()
            .HasOne(om => om.Role)
            .WithMany(r => r.OrganizationMembers)
            .HasForeignKey(om => om.RoleId)
            .IsRequired();

        modelBuilder.Entity<Campaign>()
            .HasOne(c => c.Organization)
            .WithMany(o => o.Campaigns)
            .HasForeignKey(c => c.OrganizationId)
            .IsRequired();

        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Organization)
            .WithMany(o => o.Sales)
            .HasForeignKey(s => s.OrganizationId)
            .IsRequired();

        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Member)
            .WithMany(m => m.Sales)
            .HasForeignKey(s => s.MemberId)
            .IsRequired();

        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Campaign)
            .WithMany(c => c.Sales)
            .HasForeignKey(s => s.CampaignId)
            .IsRequired();

        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Contributor)
            .WithMany(c => c.Sales)
            .HasForeignKey(s => s.ContributorId)
            .IsRequired();

        modelBuilder.Entity<VoucherInstance>()
            .HasOne(vi => vi.VoucherTemplate)
            .WithMany(vt => vt.VoucherInstances)
            .HasForeignKey(vi => vi.VoucherTemplateId)
            .IsRequired();

        modelBuilder.Entity<VoucherInstance>()
            .HasOne(vi => vi.Sale)
            .WithMany(s => s.VoucherInstances)
            .HasForeignKey(vi => vi.SaleId)
            .IsRequired(false); // atenção: relação opcional

        modelBuilder.Entity<VoucherTemplate>()
            .HasOne(vt => vt.Organization)
            .WithMany()
            .HasForeignKey(vt => vt.OrganizationId)
            .IsRequired();

        modelBuilder.Entity<VoucherTemplate>()
            .HasOne(vt => vt.Campaign)
            .WithMany(c => c.VoucherTemplates)
            .HasForeignKey(vt => vt.CampaignId)
            .IsRequired();

        modelBuilder.Entity<Organization>()
            .OwnsOne(o => o.Document, d =>
            {
                d.Property(p => p.Number).HasColumnName("DocumentNumber");
                d.Property(p => p.Type).HasColumnName("DocumentType");
            });
    }
}
