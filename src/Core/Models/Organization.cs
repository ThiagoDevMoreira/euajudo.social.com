using System.Text.Json;

namespace Core.Models;

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
    public required bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    //relações
    public List<Campaign> Campaigns { get; set; } = [];
    public List<Sale> Sales { get; set; } = [];
    public List<OrganizationMember> OrganizationMembers { get; set; } = [];
    public List<OrganizationContributor> OrganizationContributors { get; set; } = [];
}
