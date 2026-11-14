namespace Orders.Api.Contracts;

public sealed record CreateOrderRequest(string? Customer, List<CreateOrderLineRequest>? Lines);

public sealed record CreateOrderLineRequest(string? Sku, int Quantity, decimal UnitPrice);
