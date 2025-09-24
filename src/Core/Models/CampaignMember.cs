namespace Core.Models;

public class CampaignMember
{
    public Guid CampaignId { get; set; }
    public required Campaign Campaign { get; set; }

    public Guid MemberId { get; set; }
    public required Member Member { get; set; }

    public string CampaignRole {get; set; } = "defaut"; // or "Coordinator"
}
