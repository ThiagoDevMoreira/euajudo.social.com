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
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
