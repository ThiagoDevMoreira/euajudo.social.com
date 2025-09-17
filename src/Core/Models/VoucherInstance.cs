using NanoidDotNet;
namespace Core.Models;

public static class VoucherCodeGenerator
{
    private const string Alphabet = "abcdefghijkmpqrstuvwxz23456789";
    public static string Generate(int len = 8)
    {
        return Nanoid.Generate(Alphabet, len);
    }
}
public class VoucherInstance
{
    public Guid Id { get; set; }
    public Guid VoucherTemplateId { get; set; }
    public required VoucherTemplate VoucherTemplate { get; set; }
    public required string Code { get; set; } = VoucherCodeGenerator.Generate();
    public string? Status { get; set; }
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.Now;
    public DateTimeOffset? RedeemedAt { get; set; }
    public Guid? SaleId { get; set; }
    public Sale? Sale { get; set; }
    public DateTimeOffset? CanceledAt { get; set; }
}