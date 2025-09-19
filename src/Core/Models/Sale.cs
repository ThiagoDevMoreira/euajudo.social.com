namespace Core.Models;

public class Sale
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string PaymentStatus { get; set; } = "Pendente";
    //status [ Pendente | Pagamento parcial | Pago | Emitido por Cortesia ]
    public required decimal TotalAmount { get; set; }
    public required decimal PaymentReceived { get; set; }
    public required string Currency { get; set; } = "BRL";
    public required string PaymentMethod { get; set; }
    public string? Notes { get; set; }

    //auditoria
    public DateTime? PaymentAt { get; set; } //preenche apenas quando estiver o valor quitado.
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    //relações
    public Guid OrganizationId { get; set; }
    public required Organization Organization { get; set; }
    public Guid MemberId { get; set; } // vendedor
    public required Member Member { get; set; }
    public Guid CampaignId { get; set; }
    public required Campaign Campaign { get; set; }
    public Guid? ContributorId { get; set; }
    public required Contributor Contributor { get; set; }
    public List<VoucherInstance> VoucherInstances { get; set; } = [];
}
