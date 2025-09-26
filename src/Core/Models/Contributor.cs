namespace Core.Models;

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
