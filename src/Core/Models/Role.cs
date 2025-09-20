namespace Core.Models;

public class Role
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Name { get; set; }
    public required string Description { get; set; }

    //relações
    public List<OrganizationMember> OrganizationMembers { get; set; } = [];
}
