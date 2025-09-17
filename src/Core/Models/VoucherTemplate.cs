using System.Text.Json;

namespace Core.Models;

public class VoucherTemplate
{
    public Guid Id { get; set; }
    public Guid OrganizationId { set; get; }
    public required Organization Organization { get; set; }
    public Guid CampaignId { set; get; }
    public required Campaign Campaign { get; set; }
    public required string Category { set; get; } = "unique";
    public string? Subtype { get; set; }
    public required JsonDocument Content { get; set; }
    public int? SalesLimit { get; set; } // null = "sem Limite"
    public required int SalesCount { get; set; } = 0;
    public required decimal Price { get; set; } = 0;
    public required string Currency { get; set; } = "BRL";
    public string? CheckoutSite { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public List<VoucherInstance> VoucherInstances { get; set; } = [];
}
