namespace Core.Models;

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
