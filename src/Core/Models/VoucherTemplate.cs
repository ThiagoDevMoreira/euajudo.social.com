using System.Text.Json;

namespace Core.Models;

public class VoucherTemplate
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Category { set; get; }
    public string? Subtype { get; set; }
    public required JsonDocument Content { get; set; }
    public required int? SalesLimit { get; set; } // null = "sem Limite"
    public required int SalesCount { get; set; } = 0;
    public required decimal Price { get; set; } = 0;
    public required string Currency { get; set; } = "BRL";
    public string? CheckoutSite { get; set; }
    public bool IsActive { get; set; } = true;

    //auditoria
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    //relações
    public Guid OrganizationId { set; get; }
    public required Organization Organization { get; set; }
    public Guid CampaignId { set; get; }
    public required Campaign Campaign { get; set; }
    public List<VoucherInstance> VoucherInstances { get; set; } = [];
}
