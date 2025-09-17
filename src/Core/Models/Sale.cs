namespace Core.Models;

public class Sale
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public required Organization Organization { get; set; }
    public Guid CampaignId { get; set; }
    public required Campaign Campaign { get; set; }
    public Guid MemberId { get; set; } // vendedor
    public required Member Member { get; set; }
    public Guid? ContributorId { get; set; }
    public Contributor? Contributor { get; set; }
    public required string ContributorFirstName { get; set; }
    public required string ContributorLastName { get; set; }
    public required string ContributorEmail { get; set; }
    public required string ContributorWhatsAppNumber { get; set; }
    public required decimal TotalAmount { get; set; } = 0;
    public required string Currency { get; set; } = "BRL";
    public required string PaymentStatus { get; set; }
    public DateTimeOffset? PaymentAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; }
    public List<VoucherInstance> VoucherInstances { get; set; } = [];
}
