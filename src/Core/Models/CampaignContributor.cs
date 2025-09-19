namespace Core.Models;

public class CampaignContributor
{
    //chave composta
    public Guid CampaignId { get; set; }
    public required Campaign Campaign { get; set; }

    public Guid ContributorId { get; set; }
    public required Contributor Contributor { get; set; }

    //dados adicionais

}