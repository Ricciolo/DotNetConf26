using Orders.Domain;

namespace Orders.Application;

public sealed class OrderService(IOrderRepository repository, TimeProvider timeProvider)
{
    public async Task<Order> CreateAsync(
        CreateOrderCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);
        OrderLine[] lines = command.Lines?
            .Select(line => OrderLine.Create(line.Sku, line.Quantity, line.UnitPrice))
            .ToArray() ?? [];

        Order order = Order.Create(command.Customer, lines, timeProvider.GetUtcNow());
        await repository.AddAsync(order, cancellationToken);
        return order;
    }

    public Task<Order?> FindAsync(Guid id, CancellationToken cancellationToken = default)
        => repository.FindAsync(id, cancellationToken);

    public Task<IReadOnlyList<Order>> ListAsync(CancellationToken cancellationToken = default)
        => repository.ListAsync(cancellationToken);
}
