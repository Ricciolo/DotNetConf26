namespace Orders.Domain;

public sealed class Order
{
    private readonly List<OrderLine> _lines;

    private Order(Guid id, string customer, IEnumerable<OrderLine> lines, DateTimeOffset createdAtUtc)
    {
        Id = id;
        Customer = customer;
        _lines = [.. lines];
        CreatedAtUtc = createdAtUtc;
        Status = OrderStatus.Submitted;
    }

    public Guid Id { get; }
    public string Customer { get; }
    public DateTimeOffset CreatedAtUtc { get; }
    public OrderStatus Status { get; private set; }
    public IReadOnlyList<OrderLine> Lines => _lines.AsReadOnly();
    public decimal Total => _lines.Sum(line => line.Total);

    public static Order Create(string? customer, IReadOnlyCollection<OrderLine>? lines, DateTimeOffset createdAtUtc)
    {
        if (string.IsNullOrWhiteSpace(customer) || customer.Trim().Length > 120)
        {
            throw new DomainRuleException("Il nome del cliente deve contenere da 1 a 120 caratteri.");
        }

        if (lines is null || lines.Count is < 1 or > 100)
        {
            throw new DomainRuleException("Un ordine deve contenere da 1 a 100 righe.");
        }

        if (lines.GroupBy(line => line.Sku, StringComparer.OrdinalIgnoreCase).Any(group => group.Count() > 1))
        {
            throw new DomainRuleException("Ogni codice articolo (SKU) può comparire una sola volta nell'ordine.");
        }

        return new Order(Guid.NewGuid(), customer.Trim(), lines, createdAtUtc.ToUniversalTime());
    }
}
