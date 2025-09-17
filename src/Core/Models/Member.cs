namespace Core.Models;

public class Member
{
    public Guid Id { get; set; }
    public List<OrganizationMember> OrganizationMembers {get; set;} = [];
    public List<MemberRole> MemberRoles {get; set;} = [];
    public required string FirstName { get; set; }
    public required string Lastname { get; set; }
    public required string Email { get; set; }
    public required string WhatsAppNumber { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}