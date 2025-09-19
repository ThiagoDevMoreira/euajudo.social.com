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
