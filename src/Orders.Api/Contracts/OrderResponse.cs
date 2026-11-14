using Orders.Domain;

namespace Orders.Api.Contracts;

public sealed record OrderLineResponse(string Sku, int Quantity, decimal UnitPrice, decimal Total);

public sealed record OrderResponse(
    Guid Id,
    string Customer,
    string Status,
    DateTimeOffset CreatedAtUtc,
    decimal Total,
    IReadOnlyList<OrderLineResponse> Lines)
{
    public static OrderResponse From(Order order) => new(
        order.Id,
        order.Customer,
        order.Status.ToString(),
        order.CreatedAtUtc,
        order.Total,
        order.Lines.Select(line =>
            new OrderLineResponse(line.Sku, line.Quantity, line.UnitPrice, line.Total)).ToArray());
}
