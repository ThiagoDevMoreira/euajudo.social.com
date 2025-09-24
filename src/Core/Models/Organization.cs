using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Utils.ValidationEntityFields;

namespace Core.Models;

public record Document (string Number, string Type);

public class Organization
{
    [Required, Key] public Guid Id { get; set; } = Guid.NewGuid();
    [Required] public required string Description { get; set; }
    [Required] public required string Country { get; set; }
    [Required] public required string State { get; set; }
    [Required] public required string City { get; set; }
    public string? Website { get; set; } = null;
    [Required, EmailAddress] public required string Email { get; set; }
    [Required, Phone] public required string WhatsAppNumber { get; set; }
    [Required] public required Document Document { get; set; }
    [Required] public required JsonDocument Settings { get; set; }

    //auditoria
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = null;
    public DateTime? DeletedAt { get; set; } = null;

    //relações
    public List<Campaign> Campaigns { get; set; } = [];
    public List<Sale> Sales { get; set; } = [];
    public List<OrganizationMember> OrganizationMembers { get; set; } = [];
    public List<OrganizationContributor> OrganizationContributors { get; set; } = [];

    public void Validate()
    {
        this.ValidateGuidField(Id);
        this.ValidateRequiredStringsFields(
            "Description",
            "Country",
            "State",
            "City",
            "Email",
            "WhatsAppNumber"
        );
        this.ValidateDocumentFields(Document.Number, Document.Type);
        this.ValidateRequiredJsonField(Settings);
        this.ValidateAuditField(CreatedAt);
    }
}
