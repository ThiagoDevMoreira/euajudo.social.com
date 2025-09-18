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
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}