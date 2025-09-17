namespace Core.Models;

public class OrganizationMember
{
    public Guid OrganizationId { get; set; }
    public Guid MemberId { get; set; }
    public required Organization Organization { get; set; }
    public required Member Member { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? DeletedAt { get; set; }
}