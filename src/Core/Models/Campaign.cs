namespace Core.Models;

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
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
