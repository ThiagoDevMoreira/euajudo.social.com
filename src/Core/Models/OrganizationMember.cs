namespace Core.Models;

public class OrganizationMember
{
    //chave composta única OrganizationId + MemberId
    public Guid OrganizationId { get; set; }
    public required Organization Organization { get; set; }
    public Guid MemberId { get; set; }
    public required Member Member { get; set; }

    //auditoria
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }

    //relções
    public Guid RoleId { get; set; }
    public required Role Role { get; set; }
}
