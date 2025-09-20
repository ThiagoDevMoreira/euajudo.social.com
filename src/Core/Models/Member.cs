namespace Core.Models;

public class Member
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string FirstName { get; set; }
    public required string Lastname { get; set; }
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
