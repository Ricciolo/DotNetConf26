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
            throw new DomainRuleException("Customer must be between 1 and 120 characters.");
        }

        if (lines is null || lines.Count is < 1 or > 100)
        {
            throw new DomainRuleException("An order must contain between 1 and 100 lines.");
        }

        if (lines.GroupBy(line => line.Sku, StringComparer.OrdinalIgnoreCase).Any(group => group.Count() > 1))
        {
            throw new DomainRuleException("Each SKU can appear only once in an order.");
        }

        return new Order(Guid.NewGuid(), customer.Trim(), lines, createdAtUtc.ToUniversalTime());
    }
}
