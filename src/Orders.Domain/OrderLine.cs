namespace Orders.Domain;

public sealed record OrderLine
{
    private OrderLine(string sku, int quantity, decimal unitPrice)
    {
        Sku = sku;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public string Sku { get; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }
    public decimal Total => Quantity * UnitPrice;

    public static OrderLine Create(string? sku, int quantity, decimal unitPrice)
    {
        if (string.IsNullOrWhiteSpace(sku) || sku.Trim().Length > 64)
        {
            throw new DomainRuleException("SKU must be between 1 and 64 characters.");
        }

        if (quantity <= 0)
        {
            throw new DomainRuleException("Quantity must be positive.");
        }

        if (unitPrice < 0)
        {
            throw new DomainRuleException("Unit price cannot be negative.");
        }

        return new OrderLine(sku.Trim().ToUpperInvariant(), quantity, unitPrice);
    }
}
