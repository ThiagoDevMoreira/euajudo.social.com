namespace Core.Models;

public class MemberRole
{
    public Guid MemberId { get; set; }
    public Guid RoleId { get; set; }
    public Guid OrganizationId { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public string? LastUpdate { get; set; } // formato `<roleAnterior> -> <roleAtual>`
    public DateTimeOffset? UpdatedAt { get; set; }
    public required Member Member {get;set;}
    public required Role Role {get;set;}
    public required Organization Organization {get;set;}
}