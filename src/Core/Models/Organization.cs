using System.Text.Json;

namespace Core.Models;

public record Document (string Number, string Type);

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
