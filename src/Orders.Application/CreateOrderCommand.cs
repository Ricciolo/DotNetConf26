namespace Orders.Application;

public sealed record CreateOrderLine(string? Sku, int Quantity, decimal UnitPrice);

public sealed record CreateOrderCommand(
    string? Customer,
    IReadOnlyList<CreateOrderLine>? Lines);
