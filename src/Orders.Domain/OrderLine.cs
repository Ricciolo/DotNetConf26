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
            throw new DomainRuleException("Il codice articolo (SKU) deve contenere da 1 a 64 caratteri.");
        }

        if (quantity <= 0)
        {
            throw new DomainRuleException("La quantità deve essere maggiore di zero.");
        }

        if (unitPrice < 0)
        {
            throw new DomainRuleException("Il prezzo unitario non può essere negativo.");
        }

        return new OrderLine(sku.Trim().ToUpperInvariant(), quantity, unitPrice);
    }
}
