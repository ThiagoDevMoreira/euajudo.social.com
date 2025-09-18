namespace Core.Models;

public class OrganizationMember
{
    public Guid OrganizationId { get; set; }
    public Guid MemberId { get; set; }
    public required Organization Organization { get; set; }
    public required Member Member { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
}
