namespace Core.Models;

public class OrganizationContributor
{
    public Guid OrganizationId { get; set; }
    public required Organization Organization { get; set; }
    public Guid ContributorId { get; set; }
    public required Contributor Contributor { get; set; }
    public DateTime? LastContributeAt { get; set; }
    public decimal? ContributeSum { get; set; }
}

