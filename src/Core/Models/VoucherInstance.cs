using NanoidDotNet;
namespace Core.Models;

public static class VoucherCode
{
    private const string Alphabet = "abcdefghijkmnpqrstuvwxyz@23456789";
    public static string Generate(int len = 8)
    {
        return Nanoid.Generate(Alphabet, len);
    }
}
public class VoucherInstance
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Code { get; set; } = VoucherCode.Generate();
    public string Status { get; set; } = "Emitido";
    //status: [ Emitido | Resgatado ]

    //auditoria
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? Canceled { get; set; }
    public DateTime? RedeemedAt { get; set; }

    //relações
    public Guid VoucherTemplateId { get; set; }
    public required VoucherTemplate VoucherTemplate { get; set; }
    public Guid SaleId { get; set; }
    public required Sale Sale { get; set; }
}

